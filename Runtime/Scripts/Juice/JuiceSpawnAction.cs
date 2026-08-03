using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceSpawnAction : IJuiceAction
	{
		[Tooltip("If false, will spawn with no parent at THIS object's position.")]
		public bool createAsChild;
		public Transform target;
		public List<GameObject> prefabs = new();
		
		public IEnumerator Execute()
		{
			var prefab = prefabs.Random();
			var instance = GameObject.Instantiate(prefab, createAsChild ? target : null);
			
			if (createAsChild == false)
			{
				instance.transform.position = target.position;
			}

			yield break;
		}
	}
}