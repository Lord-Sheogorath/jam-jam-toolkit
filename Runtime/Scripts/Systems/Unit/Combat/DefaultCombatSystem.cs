using System.Collections.Generic;

namespace LordSheo.JJTK
{
	public class DefaultCombatSystem : ICombatSystem
	{
		public CombatStateType PreviousState { get; private set; }
		public CombatStateType CurrentState { get; private set; }
		
		public event System.Action<CombatStateType, CombatStateType> OnChangedEvent;

		private readonly List<UnitController> _targets = new();
		
		public void SetCurrentState(CombatStateType state)
		{
			PreviousState = CurrentState;
			CurrentState = state;

			_targets.Clear();
			
			OnChangedEvent?.Invoke(PreviousState, CurrentState);
		}
		public void ResetCurrentState()
		{
			SetCurrentState(CombatStateType.None);
		}

		public void SetCurrentTargets(List<UnitController> targets)
		{
			_targets.Clear();
			AddCurrentTargets(targets);
		}
		public void AddCurrentTargets(List<UnitController> targets)
		{
			if (targets.IsNullOrEmpty())
			{
				return;
			}
			
			_targets.AddRange(targets);
		}
		public IReadOnlyList<UnitController> GetCurrentTargets()
		{
			return _targets;
		}
	}
}