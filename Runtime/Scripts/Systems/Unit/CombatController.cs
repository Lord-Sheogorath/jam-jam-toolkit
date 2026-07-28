using System;
using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class CombatController : MonoBehaviour
	{
		public enum CombatStateType
		{
			None = 0,
			
			PreCombat = 1,
			ActiveCombat = 2,
		}

		public class CombatState
		{
			public readonly Guid guid;
			
			public CombatStateType type;
			public UnitController target;

			public CombatState()
			{
				guid = Guid.NewGuid();
			}
		}

		public CombatState PreviousState { get; private set; } = new()
		{
			type = CombatStateType.None,
		};

		public CombatState CurrentState { get; private set; } = new()
		{
			type = CombatStateType.None,
		};

		public bool IsPreCombatActive => CurrentState != null
			&& CurrentState.type == CombatStateType.PreCombat;
		public bool WasPreCombatActive => PreviousState != null
			&& PreviousState.type == CombatStateType.PreCombat;
		
		public bool IsCombatActive => CurrentState != null
			&& CurrentState.type == CombatStateType.ActiveCombat;
		public bool WasCombatActive => PreviousState != null
			&& PreviousState.type == CombatStateType.ActiveCombat;
		
		public CombatStateType PrevFrameCombatStateType { get; private set; }

		public UnitController PreviousTarget => PreviousState?.target;
		public UnitController CurrentTarget => CurrentState?.target;

		public event System.Action OnPreCombatStartEvent;
		public event System.Action OnPreCombatStopEvent;
		
		public event System.Action OnCombatStartEvent;
		public event System.Action OnCombatStopEvent;

		private UnitController _unit;

		private void Start()
		{
			_unit = GetComponentInParent<UnitController>();
		}

		public void StartPreCombat(UnitController target)
		{
			if (IsPreCombatActive && target == CurrentTarget)
			{
				return;
			}

			CurrentState = new()
			{
				type = CombatStateType.PreCombat,
				target = target,
			};
			
			OnPreCombatStartEvent?.Invoke();
		}
		public void StopPreCombat()
		{
			if (IsPreCombatActive == false)
			{
				return;
			}
			
			PreviousState = CurrentState;
			CurrentState = new()
			{
				type = CombatStateType.None,
			};
			
			OnPreCombatStopEvent?.Invoke();
		}
		
		public void StartCombat(UnitController target)
		{
			if (IsCombatActive && target == CurrentTarget)
			{
				return;
			}

			CurrentState = new()
			{
				type = CombatStateType.ActiveCombat,
				target = target,
			};
			
			OnCombatStartEvent?.Invoke();
		}

		public void StopCombat()
		{
			if (IsCombatActive == false)
			{
				return;
			}

			PreviousState = CurrentState;
			CurrentState = new()
			{
				type = CombatStateType.None,
			};
			
			OnCombatStopEvent?.Invoke();
		}

		private void LateUpdate()
		{
			PrevFrameCombatStateType = CurrentState?.type ?? CombatStateType.None;
		}
	}
}