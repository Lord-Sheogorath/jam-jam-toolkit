using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	[CreateAssetMenu(menuName = Constants.CREATE_ASSET_PREFIX + "Rewards/" + nameof(WeightedRewardContent))]
	public class WeightedRewardContent : RewardContent
	{
		private class RewardCollection : SerialisedDictionary<string, RewardAmount>
		{
			
		}
		
		[System.Serializable]
		public class RewardAmount
		{
			public float weight;
			public Vector2Int range;

			public int RandValue => range.Random();
		}

		[SerializeField]
		private RewardCollection _rewards = new();
		
		public override List<Instance> GetRolledRewards(int rollCount)
		{
			var rolledRewards = new List<Instance>();
			var computedRewards = new Dictionary<string, int>();
			var totalWeight = 0f;
			
			foreach (var kvp in _rewards)
			{
				totalWeight += kvp.Value.weight;
			}

			if (totalWeight == 0)
			{
				Debug.LogError("TotalWeight == 0, check why.", this);
				return rolledRewards;
			}

			for (int i = 0; i < rollCount; i++)
			{
				var randWeight = UnityEngine.Random.Range(0, totalWeight);
				var currentWeight = 0f;
				
				foreach (var kvp in _rewards)
				{
					currentWeight += kvp.Value.weight;

					if (randWeight <= currentWeight)
					{
						computedRewards.AddToCurrentValue(kvp.Key, kvp.Value.RandValue);
						break;
					}
				}
			}
			
			foreach (var kvp in computedRewards)
			{
				rolledRewards.Add(new(kvp.Key, kvp.Value));
			}

			return rolledRewards;
		}
	}
}