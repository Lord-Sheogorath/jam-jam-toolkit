using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceSetActiveAction : IJuiceAction
	{
		public GameObject target;
		public bool activeState = false;
		
		public Task Execute()
		{
			target.SetActive(activeState);

			return Task.CompletedTask;
		}
	}
}