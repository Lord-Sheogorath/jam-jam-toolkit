using System.Collections.Generic;

namespace LordSheo.JJTK
{
	public enum CombatStateType
	{
		None = 0,
			
		PreCombat = 1,
		ActiveCombat = 2,
	}
	
	public interface ICombatSystem : ISystem
	{
		CombatStateType PreviousState { get; }
		CombatStateType CurrentState { get; }

		/// <summary>
		/// {Previous, Current}
		/// </summary>
		event System.Action<CombatStateType, CombatStateType> OnChangedEvent;

		void SetCurrentState(CombatStateType state);
		void ResetCurrentState();
		
		void SetCurrentTargets(List<UnitController> targets);
		void AddCurrentTargets(List<UnitController> targets);
		IReadOnlyList<UnitController> GetCurrentTargets();
	}
}