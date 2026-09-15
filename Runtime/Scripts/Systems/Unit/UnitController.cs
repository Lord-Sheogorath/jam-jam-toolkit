using System;
using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class UnitController : MonoBehaviour
	{
		[SerializeField]
		private Transform root;
		[SerializeField]
		private Transform visualRoot;

		public IStatSystem StatSystem => GetSystem<IStatSystem>();
		public IInventorySystem InventorySystem => GetSystem<IInventorySystem>();
		public IHealthSystem HealthSystem => GetSystem<IHealthSystem>();

		public ITargetSystem TargetSystem => GetSystem<ITargetSystem>();
		public ICombatSystem CombatSystem => GetSystem<ICombatSystem>();
		public IUnitActionSystem ActionSystem => GetSystem<IUnitActionSystem>();
		public IAbilitySystem AbilitySystem => GetSystem<IAbilitySystem>();

		public Transform Root => root;
		public Transform VisualRoot => visualRoot;

		private readonly SystemRegistry _systems = new();
		
		private readonly List<ITickable> _tickables = new();
		private readonly List<ITickable> _bufferedTickables = new();

		private void OnValidate()
		{
			if (root == null)
			{
				root = transform.parent ?? transform;
			}
		}

		private void Update()
		{
			var deltaTime = Time.deltaTime;

			Tick(deltaTime);
		}

		private void Tick(float deltaTime)
		{
			if (_tickables.IsNullOrEmpty())
			{
				return;
			}
			
			_bufferedTickables.Clear();
			_bufferedTickables.AddRange(_tickables);

			foreach (var tickable in _bufferedTickables)
			{
				tickable.Tick(deltaTime);
			}
		}

		public void AddSystem<TBase, TImpl>(TImpl system)
			where TBase : ISystem
			where TImpl : TBase
		{
			_systems.Add<TBase, TImpl>(system);

			if (system is ITickable tickable)
			{
				_tickables.Add(tickable);	
			}
		}
		public void RemoveSystem<TBase>()
			where TBase : ISystem
		{
			if (_systems.Contains<TBase>() == false)
			{
				return;
			}
			
			var impl = _systems.Get<TBase>();
			
			_systems.Remove<TBase>();

			if (impl is ITickable tickable)
			{
				_tickables.Remove(tickable);
			}
		}
		public TBase GetSystem<TBase>()
			where TBase : ISystem
		{
			return _systems.Get<TBase>();
		}
	}
}