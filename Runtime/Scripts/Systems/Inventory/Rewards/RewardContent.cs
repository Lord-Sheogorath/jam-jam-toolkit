using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public abstract class RewardContent : ScriptableObject
	{
		public struct Instance
		{
			public string itemId;
			public int amount;

			public Instance(EnumString itemId, int amount)
			{
				this.itemId = itemId;
				this.amount = amount;
			}
		}
		
		public abstract List<Instance> GetRolledRewards(int rollCount);
	}
}