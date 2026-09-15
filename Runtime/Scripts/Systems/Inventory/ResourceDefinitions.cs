using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	[CreateAssetMenu(menuName = Constants.CREATE_ASSET_PREFIX + "Definitions/" + nameof(ResourceDefinitions))]
	public class ResourceDefinitions : ScriptableObject
	{
		[System.Serializable]
		public class Definition : BaseDefinition
		{
		}
		
		[System.Serializable]
		private class Definitions : SerialisedDictionary<ResourceType, Definition>
		{
		}

		[SerializeField]
		private Definitions _definitions = new Definitions();

		public bool Contains(ResourceType type)
		{
			return _definitions.ContainsKey(type);
		}
		public Definition Get(ResourceType type)
		{
			return _definitions.GetValueOrDefault(type);
		}
	}
}