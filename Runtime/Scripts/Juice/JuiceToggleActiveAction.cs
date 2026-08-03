using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceToggleActiveAction : IJuiceAction
	{
		public GameObject target;
		public float delay = 1f;
		
		public async UniTask Execute()
		{
			target.SetActive(!target.activeInHierarchy);
			await UniTask.Delay(TimeSpan.FromSeconds(delay));
			target.SetActive(!target.activeInHierarchy);
		}
	}
}