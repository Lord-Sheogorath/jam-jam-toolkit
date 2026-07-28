using System;
using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public enum StatType
	{
		MaxHealth,
		CurrentHealth,
		
		/// <summary>
		/// How many times can we fish per second
		/// </summary>
		FishRate,
		/// <summary>
		/// How many fish do we get per attempt
		/// </summary>
		FishAmount,
		
		AttackRate,
		AttackDamage,
		
		ReadyToLeaveCountdown,
		
		MoveSpeed,
	}

	public class StatController : MonoBehaviour
	{
		public StatPresetAsset presetAsset;
		public StatUpgradesAsset upgradesAsset;
		
		public event System.Action<StatType> OnChangeEvent;

		private readonly Dictionary<StatType, float> _stats = new();
		private readonly Dictionary<StatType, int> _upgrades = new();
		
		public void Awake()
		{
			foreach (var kvp in presetAsset.stats)
			{
				Set(kvp.Key, kvp.Value);
			}
		}

		public bool Contains(StatType type)
		{
			return _stats.ContainsKey(type);
		}

		public float Get(StatType type)
		{
			return _stats.GetValueOrDefault(type);
		}

		public void Set(StatType type, float value)
		{
			_stats[type] = value;
			OnChangeEvent?.Invoke(type);
		}

		public void Upgrade(StatType type, float amount)
		{
			var level = GetCurrentUpgrade(type);

			_upgrades[type] = level + 1;

			var value = Get(type);
			value += amount;
			Set(type, value);
		}

		public int GetCurrentUpgrade(StatType type)
		{
			return _upgrades.GetValueOrDefault(type);
		}
	}
}