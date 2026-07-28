namespace LordSheo.JJTK
{
	public interface IHealthSystem : ISystem
	{
		event System.Action<int> OnDamagedEvent;
		event System.Action<int> OnHealedEvent;
		event System.Action OnChangedEvent;
		event System.Action OnDeathEvent;

		int GetMax();
		int GetCurrent();
		void SetCurrent(int value);

		void Damage(int amount);
		void Heal(int amount);
	}
}