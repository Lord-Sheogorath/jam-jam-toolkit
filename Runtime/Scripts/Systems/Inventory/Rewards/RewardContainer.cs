using System.Collections.Generic;
using System.Linq;
using LordSheo.JJTK;
using UnityEngine;

namespace SiegeCrawler
{
	public class RewardContainer : MonoBehaviour
	{
		public RewardContent rewards;
		
		public virtual List<RewardContent.Instance> GetRolledRewards(int rollCount = 1)
		{
			return rewards.GetRolledRewards(rollCount);
		}
	}
}