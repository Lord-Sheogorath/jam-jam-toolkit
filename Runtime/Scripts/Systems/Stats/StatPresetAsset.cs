using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LordSheo.JJTK
{
	[CreateAssetMenu(menuName = "GMTK2026/Asset/Stats/StatPreset")]
	public class StatPresetAsset : ScriptableObject
	{
		[System.Serializable]
		public class StatDictionary : SerialisedDictionary<StatType, float>
		{
		}
		
		public StatDictionary stats = new();
		
#if UNITY_EDITOR
		[Button]
		private void AddAllMissingStatTypes()
		{
			var statTypes = System.Enum.GetValues(typeof(StatType));
			var modified = false;
			
			foreach (StatType statType in statTypes)
			{
				modified = stats.TryAdd(statType, 0) || modified; 
			}

			if (modified)
			{
				UnityEditor.EditorUtility.SetDirty(this);
			}
		}
#endif
	}
}