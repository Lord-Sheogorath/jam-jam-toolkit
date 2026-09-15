using UnityEngine;

namespace LordSheo.JJTK
{
	[CreateAssetMenu(menuName = Constants.CREATE_ASSET_PREFIX + "Definitions/" + nameof(GeneralDefinitions))]
	public class GeneralDefinitions : ScriptableObjectSingleton<GeneralDefinitions>
	{
		public StatDefinitions stats;
		public ResourceDefinitions resources;
	}
}