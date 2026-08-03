using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceDelayAction : IJuiceAction
	{
		public float delay;
		
		public async UniTask Execute()
		{
			await UniTask.Delay(TimeSpan.FromSeconds(delay));
		}
	}
}