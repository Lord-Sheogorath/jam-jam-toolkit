using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LordSheo.JJTK
{
	[CreateAssetMenu(menuName = "GMTK2026/Asset/Stats/StatUpgrades")]
	public class StatUpgradesAsset : ScriptableObject
	{
		[System.Serializable]
		public class UpgradesDictionary : SerialisedDictionary<StatType, UpgradesList>
		{
		}

		[System.Serializable]
		public class UpgradesList
		{
			public List<Upgrade> values = new();
		}
		
		[System.Serializable]
		public class Upgrade
		{
			public int cost;
			public float stat;
		}

		public ResourceType resourceCost;
		public UpgradesDictionary values = new();
		
#if UNITY_EDITOR
		[Button]
		private void AddUpgradesTo(StatType type, int count, int multCost, float multAmount)
		{
			if (values.TryGetValue(type, out var statLevels) == false)
			{
				values[type] = statLevels = new();
			}
			
			for (int i = 0; i < count; i++)
			{
				var mult = i + 1;
				
				var cost = multCost * mult;
				var amount = multAmount * mult;
				
				statLevels.values.Add(new()
				{
					cost = cost,
					stat = amount,
				});
			}

			UnityEditor.EditorUtility.SetDirty(this);
		}
#endif
	}
}