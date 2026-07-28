using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace LordSheo.JJTK
{
	public abstract class ActionAsset : ScriptableObject
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

	public class ActionController : MonoBehaviour
	{
		public List<ActionAsset> availableActions = new List<ActionAsset>();

		private UnitController _unit;
		private ActionAsset _currentAction;

		private void Start()
		{
			_unit = GetComponentInParent<UnitController>();
		}

		[Button]
		public void SetActiveAction(ActionAsset action)
		{
			if (_currentAction != null)
			{
				_currentAction.OnExit();
				Destroy(_currentAction);
				
				_currentAction = null;
			}
		
			// TO-DO: Check if intended behaviour
			if (action == null)
			{
				return;
			}

			// TO-DO: Check if intended behaviour
			if (action.IsValid(_unit) == false)
			{
				return;
			}
			
			// Create new action asset so it can carry state.
			_currentAction = Instantiate(action);
			_currentAction.OnEnter(_unit);
		}
		
		[Button]
		public void StopActiveAction()
		{
			SetActiveAction(null);
		}
		
		public void Update()
		{
			if (_currentAction != null)
			{
				_currentAction.OnUpdate();
			}
		}
	}
}