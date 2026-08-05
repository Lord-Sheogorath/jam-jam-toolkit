using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class PoolOfComponents<T>
		where T : Component
	{
		public class Pool
		{
			public Transform parent;
			public T prefab;

			public Queue<PoolItemInstance> ready = new();
			public List<T> used = new();
		}
		
		public class PoolItemInstance
		{
			public T instance;
			public float timestamp;

			public PoolItemInstance(T instance)
			{
				this.instance = instance;
				this.timestamp = Time.realtimeSinceStartup;
			}
		}

		public string name;
		public float minCullTime = 5f;

		public Action<T> postCreateCallback;
		public Action<T> postDestroyCallback;
		public Action<T> postCullCallback;
		
		private readonly Dictionary<T, Pool> _pools = new();
		/// <summary>
		/// Maps instances to their prefabs.
		/// </summary>
		private readonly Dictionary<T, T> _instances = new();
		
		private static Transform Root
		{
			get
			{
				if (_root == null)
				{
					_root = (GameObject.Find("=== Pool Party ===") ?? new GameObject("=== Pool Party ===")).transform;
				}

				return _root;
			}
		}
		private static Transform _root;

		public PoolOfComponents()
		{
			name = typeof(T).Name;
		}
		
		public IEnumerator StartCull()
		{
			while (Application.isPlaying)
			{
				yield return new WaitForSecondsRealtime(minCullTime);

				var time = Time.realtimeSinceStartup;
					
				foreach (var pool in _pools.Values)
				{
					// NOTE: In case something destroys a pooled object incorrectly
					// then we should just remove it and call it a day.
					for (var index = pool.used.Count - 1; index >= 0; index--)
					{
						var used = pool.used[index];

						if (used == null)
						{
							pool.used.RemoveAt(index);
						}
					}

					if (pool.ready.Count == 0)
					{
						continue;
					}

					while (pool.ready.TryPeek(out var itemInstance) && (time - itemInstance.timestamp) > minCullTime)
					{
						pool.ready.Dequeue();

						_instances.Remove(itemInstance.instance);
						postCullCallback?.Invoke(itemInstance.instance);
						
						GameObject.Destroy(itemInstance.instance.gameObject);
					}
				}
			}
		}
		
		public T Create(T prefab, Vector3 position)
		{
			var pool = GetPool(prefab);
			
			if (pool.ready.TryDequeue(out var itemInstance) == false)
			{
				itemInstance = new PoolItemInstance(GameObject.Instantiate(prefab, pool.parent));
			}

			_instances[itemInstance.instance] = prefab;
            
			pool.used.Add(itemInstance.instance);
			itemInstance.instance.transform.position = position;
			itemInstance.instance.gameObject.SetActive(true);
			postCreateCallback?.Invoke(itemInstance.instance);
            
			return itemInstance.instance;
		}
		
		public void Destroy(T instance)
		{
			if (instance == null)
			{
				return;
			}
			
			var prefab = _instances[instance];
			var pool = GetPool(prefab);
			
			// TO-DO: Fix calling 'Destroy' multiple times

			var itemInstance = new PoolItemInstance(instance);
			pool.ready.Enqueue(itemInstance);
			pool.used.Remove(instance);

			instance.transform.parent = pool.parent;
			instance.gameObject.SetActive(false);
			
			postDestroyCallback?.Invoke(instance);
		}
		
		public Pool GetPool(T prefab)
		{
			if (_pools.TryGetValue(prefab, out var pool) == false)
			{
				_pools[prefab] = pool = new();

				pool.parent = new GameObject($"[{name}] {prefab.name}").transform;
				pool.parent.parent = Root;
				pool.prefab = prefab;
			}

			return pool;
		}
	}
}