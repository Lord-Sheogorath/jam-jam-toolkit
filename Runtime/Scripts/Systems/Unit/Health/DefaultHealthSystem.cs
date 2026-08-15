using UnityEngine;

namespace LordSheo.JJTK
{
	public class DefaultHealthSystem : IHealthSystem
	{
		public event System.Action<IHealthSystem.Change> OnDamagedEvent;
		public event System.Action<IHealthSystem.Change> OnHealedEvent;
		public event System.Action OnChangedEvent;
		public event System.Action OnDeathEvent;

		public int Max { get; private set; }
		public int Current { get; private set; }
		public bool Alive => Current > 0;

		public IHealthSystem Setup(int max, int current)
		{
			Max = max;
			Current = current;

			return this;
		}
		
		public void SetMax(int value)
		{
			Max = value;
			
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
		
		public void Damage(UnitController sender, int amount)
		{
			if (Alive == false)
			{
				return;
			}

			var clampedAmount = Mathf.Max(0, amount);
			clampedAmount = Mathf.Min(Current, clampedAmount);

			var changedValue = new ChangedIntValue()
			{
				type = ChangedNumValueType.Remove,
				
				requestedAmount = amount,
				actualAmount = clampedAmount,
				
				previous = Current,
			};
			
			var current = Current;
			current -= changedValue.actualAmount;

			SetCurrent(current, true);

			changedValue.current = Current;
			
			OnDamagedEvent?.Invoke(new(sender, changedValue));
			OnChangedEvent?.Invoke();

			if (Alive == false)
			{
				OnDeathEvent?.Invoke();
			}
		}

		public void Heal(UnitController sender, int amount)
		{
			if (Alive == false)
			{
				return;
			}
			
			var clampedAmount = Mathf.Max(0, amount);
			clampedAmount = Mathf.Min(Max - Current, clampedAmount);
			
			var changedValue = new ChangedIntValue()
			{
				type = ChangedNumValueType.Add,
				
				requestedAmount = amount,
				actualAmount = clampedAmount,
				
				previous = Current,
			};
			
			var current = Current;
			current += changedValue.actualAmount;

			SetCurrent(current, true);

			changedValue.current = Current;

			OnHealedEvent?.Invoke(new(sender, changedValue));
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