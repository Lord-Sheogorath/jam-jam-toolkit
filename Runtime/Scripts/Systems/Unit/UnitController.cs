using System;
using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class DefaultUnitSetup : MonoBehaviour
	{
		private UnitController _unit;
		
		public void Awake()
		{
			_unit = GetComponent<UnitController>();
			
			var statSystem = new DefaultStatSystem();
			var inventorySystem = new DefaultInventorySystem();
			var healthSystem = new DefaultHealthSystem(statSystem);
			var targetSystem = new DefaultTargetSystem();
			var actionSystem = new DefaultUnitActionSystem(_unit);
			var combatSystem = new DefaultCombatSystem();
			var signalSystem = new DefaultSignalSystem();
			
			_unit.AddSystem<IStatSystem, DefaultStatSystem>(statSystem);
			_unit.AddSystem<IInventorySystem, DefaultInventorySystem>(inventorySystem);
			_unit.AddSystem<IHealthSystem, DefaultHealthSystem>(healthSystem);
			_unit.AddSystem<ITargetSystem, DefaultTargetSystem>(targetSystem);
			_unit.AddSystem<IUnitActionSystem, DefaultUnitActionSystem>(actionSystem);
			_unit.AddSystem<ICombatSystem, DefaultCombatSystem>(combatSystem);
			_unit.AddSystem<ISignalSystem, DefaultSignalSystem>(signalSystem);
		}
	}
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