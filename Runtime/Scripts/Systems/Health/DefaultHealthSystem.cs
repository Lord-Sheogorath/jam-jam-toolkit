using UnityEngine;

namespace LordSheo.JJTK
{
	public class DefaultHealthSystem : IHealthSystem
	{
		public event System.Action<ChangedIntValue> OnDamagedEvent;
		public event System.Action<ChangedIntValue> OnHealedEvent;
		public event System.Action OnChangedEvent;
		public event System.Action OnDeathEvent;

		public int Max => (int)_stat.Get(StatType.max_health);
		public int Current { get; private set; }
		public bool Alive => Current > 0;
		
		private readonly IStatSystem _stat;
		
		public DefaultHealthSystem(IStatSystem stat)
		{
			_stat = stat;

			stat.OnChangedEvent += OnStatChanged;
		}

		private void OnStatChanged(StatType type, ChangedFloatValue change)
		{
			
		}

		public void SetMax(int value)
		{
			if (Alive == false)
			{
				return;
			}
			
			_stat.Set(StatType.max_health, value);

			if (Current > value)
			{
				SetCurrent(value, true);
			}
			
			OnChangedEvent?.Invoke();
		}

		public void SetCurrent(int value)
		{
			if (Alive == false)
			{
				return;
			}
			
			SetCurrent(value, false);
		}

		private void SetCurrent(int value, bool silent)
		{
			Current = ClampCurrent(value);

			if (silent == false)
			{
				OnChangedEvent?.Invoke();
				
				if (Alive == false)
				{
					OnDeathEvent?.Invoke();
				}
			}
		}
		
		public void Damage(int amount)
		{
			if (Alive == false)
			{
				return;
			}

			var clampedAmount = Mathf.Max(0, amount);
			clampedAmount = Mathf.Min(Current, clampedAmount);

			var change = new ChangedIntValue()
			{
				type = ChangedNumValueType.Remove,
				
				requestedAmount = amount,
				actualAmount = clampedAmount,
				
				previous = Current,
			};
			
			var current = Current;
			current -= change.actualAmount;

			SetCurrent(current, true);

			change.current = Current;
			
			OnDamagedEvent?.Invoke(change);
			OnChangedEvent?.Invoke();

			if (Alive == false)
			{
				OnDeathEvent?.Invoke();
			}
		}

		public void Heal(int amount)
		{
			if (Alive == false)
			{
				return;
			}
			
			var clampedAmount = Mathf.Max(0, amount);
			clampedAmount = Mathf.Min(Max - Current, clampedAmount);
			
			var change = new ChangedIntValue()
			{
				type = ChangedNumValueType.Add,
				
				requestedAmount = amount,
				actualAmount = clampedAmount,
				
				previous = Current,
			};
			
			var current = Current;
			current += change.actualAmount;

			SetCurrent(current, true);

			change.current = Current;

			OnHealedEvent?.Invoke(change);
			OnChangedEvent?.Invoke();
		}

		private int ClampCurrent(int current)
		{
			return Mathf.Clamp(current, 0, Max);
		}

		public void Kill()
		{
			SetCurrent(0);
		}
		public void Revive()
		{
			SetCurrent(Max, false);
		}
	}
}