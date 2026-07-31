namespace LordSheo.JJTK
{
	public interface IHealthSystem : ISystem
	{
		event System.Action<ChangedIntValue> OnDamagedEvent;
		event System.Action<ChangedIntValue> OnHealedEvent;
		event System.Action OnChangedEvent;
		event System.Action OnDeathEvent;

		int Max { get; }
		int Current { get; }
		bool Alive{ get; }
		
		void SetMax(int value);
		void SetCurrent(int value);

		void Damage(int amount);
		void Heal(int amount);

		void Kill();
		void Revive();
	}
}