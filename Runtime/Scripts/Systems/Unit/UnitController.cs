using System;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class DefaultUnitSetup : MonoBehaviour
	{
		private UnitController _unit;
		
		public void Awake()
		{
			var systems = _unit.Systems;

			var statSystem = new DefaultStatSystem();
			var inventorySystem = new DefaultInventorySystem();
			var healthSystem = new DefaultHealthSystem(statSystem);
			
			systems.Add<IStatSystem, DefaultStatSystem>(statSystem);
			systems.Add<IInventorySystem, DefaultInventorySystem>(inventorySystem);
			systems.Add<IHealthSystem, DefaultHealthSystem>(healthSystem);
		}
	}
	public class UnitController : MonoBehaviour
	{
		[SerializeField]
		private Transform root;
		[SerializeField]
		private Transform visualRoot;

		public IStatSystem StatController => _systems.Get<IStatSystem>();
		public IInventorySystem InventoryController => _systems.Get<IInventorySystem>();
		public IHealthSystem HealthController => _systems.Get<IHealthSystem>();
		
		public TargetController TargetController { get; private set; }
		public CombatController CombatController { get; private set; }
		public ActionController ActionController { get; private set; }

		public SystemRegistry Systems => _systems;
		
		public Transform Root => root;
		public Transform VisualRoot => visualRoot;

		private readonly SystemRegistry _systems = new();

		private void OnValidate()
		{
			if (root == null)
			{
				root = transform.parent ?? transform;
			}
		}

		private void Awake()
		{
			TargetController = GetComponent<TargetController>();
			CombatController = GetComponent<CombatController>();
			ActionController = GetComponent<ActionController>();
		}
	}
}