using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceMultiSpawnAction : IJuiceAction
	{
		[Tooltip("If false, will spawn with no parent at TARGET object's position.")]
		public bool createAsChild;
		public Transform target;
		public List<GameObject> prefabs = new();
		
		public Task Execute()
		{
			foreach (var prefab in prefabs)
			{
				var instance = GameObject.Instantiate(prefab, createAsChild ? target : null);
			
				if (createAsChild == false)
				{
					instance.transform.position = target.position;
				}
			}

			return Task.CompletedTask;
		}
	}
}