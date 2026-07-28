using System;
using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public static class UpdateSystem
	{
		private class Driver : MonoBehaviour
		{
			private void Update()
			{
				Tick(Time.deltaTime);
			}
		}

		private static readonly List<System.Action<float>> _events = new();
		private static readonly List<System.Action<float>> _buffer = new();
		
		private static Driver _driver;
		
		private static void Tick(float deltaTime)
		{
			if (_events.IsNullOrEmpty())
			{
				return;
			}
			
			_buffer.Clear();
			_buffer.AddRange(_events);
			
			foreach (var ev in _buffer)
			{
				try
				{
					ev.Invoke(deltaTime);
				}
				catch (Exception e)
				{
					Debug.LogException(e);
					Debug.LogError("Exception in UpdateSystem", _driver);
				}
			}
		}

		public static void Add(System.Action<float> callback)
		{
			if (_driver == null)
			{
				var obj = new GameObject("[UpdateSystem]");
				GameObject.DontDestroyOnLoad(obj);
				
				_driver = obj.AddComponent<Driver>();
			}
			
#if UNITY_EDITOR || DEVELOPMENT_BUILD
			if (_events.Contains(callback))
			{
				Debug.LogError("UpdateSystem: callback already subscribed.");
			}
#endif
			
			_events.Add(callback);
		}
		public static void Remove(System.Action<float> callback)
		{
			_events.Remove(callback);
		}
	}
}