using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceDestroyAction : IJuiceAction
	{
		[Tooltip("Won't actually destroy the object.")]
		public bool fake = false;
		public GameObject target;
		
		public event System.Action OnDestroyEvent;
		
		public UniTask Execute()
		{
			if (target == null)
			{
				return UniTask.CompletedTask;
			}
				
			OnDestroyEvent?.Invoke();

			if (fake == false)
			{
				GameObject.Destroy(target);
			}
			
			return UniTask.CompletedTask;
		}
	}
}