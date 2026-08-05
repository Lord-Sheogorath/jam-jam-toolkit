using UnityEngine;

namespace LordSheo.JJTK
{
	[CreateAssetMenu(menuName = "LordSheo/JJTK/" + nameof(GeneralDefinitions))]
	public class GeneralDefinitions : ScriptableObjectSingleton<GeneralDefinitions>
	{
		public StatDefinitions stats;
		public ResourceDefinitions resources;
	}
}