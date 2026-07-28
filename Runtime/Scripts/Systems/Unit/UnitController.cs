using System;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class UnitController : MonoBehaviour
	{
		[SerializeField]
		private Transform root;
		[SerializeField]
		private Transform visualRoot;
		
		public StatController StatController { get; private set; }
		public HealthController HealthController { get; private set; }
		public TargetController TargetController { get; private set; }
		public CombatController CombatController { get; private set; }
		public ActionController ActionController { get; private set; }
		public InventoryController InventoryController { get; private set; }

		public Transform Root => root;
		public Transform VisualRoot => visualRoot;

		private void OnValidate()
		{
			if (root == null)
			{
				root = transform.parent ?? transform;
			}
		}

		private void Awake()
		{
			StatController = GetComponent<StatController>();
			HealthController = GetComponent<HealthController>();
			TargetController = GetComponent<TargetController>();
			CombatController = GetComponent<CombatController>();
			ActionController = GetComponent<ActionController>();
			InventoryController = GetComponent<InventoryController>();
		}
	}
}