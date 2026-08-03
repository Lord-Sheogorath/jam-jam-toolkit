using System.Collections;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceSetActiveAction : IJuiceAction
	{
		public GameObject target;
		public bool activeState = false;
		
		public IEnumerator Execute()
		{
			target.SetActive(activeState);
			yield break;
		}
	}
}