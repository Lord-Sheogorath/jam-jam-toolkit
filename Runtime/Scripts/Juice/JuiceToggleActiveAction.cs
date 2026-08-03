using System.Collections;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceToggleActiveAction : IJuiceAction
	{
		public GameObject target;
		public float delay = 1f;
		
		public IEnumerator Execute()
		{
			target.SetActive(!target.activeInHierarchy);
			yield return new WaitForSeconds(delay);
			target.SetActive(!target.activeInHierarchy);
		}
	}
}