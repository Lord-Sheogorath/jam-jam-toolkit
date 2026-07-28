using UnityEngine;

namespace LordSheo.JJTK
{
	public class DefaultHealthSystem : IHealthSystem
	{
		public event System.Action<int> OnDamagedEvent;
		public event System.Action<int> OnHealedEvent;
		public event System.Action OnChangedEvent;
		public event System.Action OnDeathEvent;

		private readonly IStatSystem _stat;
		
		public DefaultHealthSystem(IStatSystem stat)
		{
			_stat = stat;
		}

		public int GetMax()
		{
			return (int)_stat.Get(StatType.max_health);
		}
		
		public int GetCurrent()
		{
			return (int)_stat.Get(StatType.current_health);
		}

		public void SetCurrent(int value)
		{
			_stat.Set(StatType.current_health, value);
		}

		public void Damage(int amount)
		{
			var current = GetCurrent();
			current -= Mathf.Max(0, amount);
			current = ClampCurrent(current);

			SetCurrent(current);

			OnDamagedEvent?.Invoke(amount);
			OnChangedEvent?.Invoke();

			if (GetCurrent() <= 0)
			{
				OnDeathEvent?.Invoke();
			}
		}

		public void Heal(int amount)
		{
			var current = GetCurrent();
			current += Mathf.Max(0, amount);
			current = ClampCurrent(current);

			SetCurrent(current);

			OnHealedEvent?.Invoke(amount);
			OnChangedEvent?.Invoke();
		}

		private int ClampCurrent(int current)
		{
			return Mathf.Clamp(current, 0, GetMax());
		}
	}
}