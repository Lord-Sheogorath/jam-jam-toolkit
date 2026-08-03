using System.Diagnostics.Contracts;

namespace LordSheo.JJTK
{
	public interface IHealthSystem : ISystem
	{
		public readonly struct Change
		{
			public readonly UnitController sender;
			public readonly ChangedIntValue value;

			public Change(UnitController sender, ChangedIntValue value)
			{
				this.sender = sender;
				this.value = value;
			}

			[Pure]
			public string ToDisplayString()
			{
				return value.ToDisplayString();
			}
		}
		
		event System.Action<Change> OnDamagedEvent;
		event System.Action<Change> OnHealedEvent;
		event System.Action OnChangedEvent;
		event System.Action OnDeathEvent;

		int Max { get; }
		int Current { get; }
		bool Alive{ get; }
		
		void SetMax(int value);
		void SetCurrent(int value);

		void Damage(UnitController sender, int amount);
		void Heal(UnitController sender, int amount);

		void Kill();
		void Revive();
	}
}