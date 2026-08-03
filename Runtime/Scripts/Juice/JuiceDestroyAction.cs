using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceDestroyAction : IJuiceAction
	{
		[Tooltip("Won't actually destroy the object.")]
		public bool fake = false;
		public GameObject target;
		
		public event System.Action OnDestroyEvent;
		
		public Task Execute()
		{
			if (target == null)
			{
				return Task.CompletedTask;
			}
				
			OnDestroyEvent?.Invoke();

			if (fake == false)
			{
				GameObject.Destroy(target);
			}
			
			return Task.CompletedTask;
		}
	}
}