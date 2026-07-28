using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class HealthController : MonoBehaviour
	{
		public event System.Action<int> OnDamageEvent;
		public event System.Action<int> OnHealEvent;
		public event System.Action OnChangeEvent;
		public event System.Action OnDeathEvent;

		public int Max => (int)_unit.StatController.Get(StatType.MaxHealth);

		public int Current
		{
			get => (int)_unit.StatController.Get(StatType.CurrentHealth);
			private set => _unit.StatController.Set(StatType.CurrentHealth, value);
		}

		private UnitController _unit;

		private void Start()
		{
			_unit = GetComponentInParent<UnitController>();
		}

		public void Damage(int amount)
		{
			var current = Current;
			current -= Mathf.Max(0, amount);
			current = ClampCurrent(current);
			
			Current = current;

			OnDamageEvent?.Invoke(amount);
			OnChangeEvent?.Invoke();

			if (Current <= 0)
			{
				OnDeathEvent?.Invoke();
			}
		}

		public void Heal(int amount)
		{
			var current = Current;
			current += Mathf.Max(0, amount);
			current = ClampCurrent(current);
			
			Current = current;

			OnHealEvent?.Invoke(amount);
			OnChangeEvent?.Invoke();
		}

		private int ClampCurrent(int current)
		{
			return Mathf.Clamp(current, 0, Max);
		}
	}
}