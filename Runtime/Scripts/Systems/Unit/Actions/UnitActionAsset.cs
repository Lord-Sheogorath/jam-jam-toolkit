using UnityEngine;

namespace LordSheo.JJTK
{
	public abstract class UnitActionAsset : ScriptableObject,
		IUnitAction
	{
		public string displayName;
		public Sprite icon;
		
		protected UnitController owner;
		
		public virtual void OnEnter(UnitController controller)
		{
			owner = controller;
		}

		public virtual void OnExit()
		{
			
		}
		
		public virtual void OnUpdate()
		{
		}

		public virtual bool IsValid(UnitController controller)
		{
			return true;
		}
	}
}