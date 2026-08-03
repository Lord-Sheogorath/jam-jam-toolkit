using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceSetActiveAction : IJuiceAction
	{
		public GameObject target;
		public bool activeState = false;
		
		public UniTask Execute(CancellationToken token)
		{
			target.SetActive(activeState);

			return UniTask.CompletedTask;
		}
	}
}