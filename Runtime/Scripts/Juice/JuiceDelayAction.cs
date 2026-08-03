using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceDelayAction : IJuiceAction
	{
		public float delay;
		
		public async Task Execute()
		{
			await Task.Delay(TimeSpan.FromSeconds(delay));
		}
	}
}