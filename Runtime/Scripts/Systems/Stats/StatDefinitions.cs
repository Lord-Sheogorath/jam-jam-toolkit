using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	[CreateAssetMenu(menuName = Constants.CREATE_ASSET_PREFIX + "Definitions/" + nameof(StatDefinitions))]
	public class StatDefinitions : ScriptableObject
	{
		[System.Serializable]
		public class Definition : BaseDefinition
		{
		}
		
		[System.Serializable]
		private class Definitions : SerialisedDictionary<StatType, Definition>
		{
		}

		[SerializeField]
		private Definitions _definitions = new Definitions();

		public bool Contains(StatType type)
		{
			return _definitions.ContainsKey(type);
		}
		public Definition Get(StatType type)
		{
			return _definitions.GetValueOrDefault(type);
		}
	}
}