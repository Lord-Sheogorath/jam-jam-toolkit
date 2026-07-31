using System;
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
}