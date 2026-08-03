using System.Collections;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceDestroyAction : IJuiceAction
	{
		public float delay;
		[Tooltip("Won't actually destroy the object.")]
		public bool fake = false;
		public GameObject target;
		
		public event System.Action OnDestroyEvent;
		
		public IEnumerator Execute()
		{
			yield return new WaitForSeconds(delay);
			
			if (target == null)
			{
				yield break;
			}
				
			OnDestroyEvent?.Invoke();

			if (fake == false)
			{
				GameObject.Destroy(target);
			}
		}
	}
}