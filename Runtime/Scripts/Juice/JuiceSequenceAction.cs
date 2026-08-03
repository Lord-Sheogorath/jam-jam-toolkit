using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceSequenceAction : IJuiceAction
	{
		[SerializeReference]
		public List<IJuiceAction> actions = new();
		public bool parallel = false;
		
		public async UniTask Execute()
		{
			if (parallel)
			{
				await ExecuteParallel();
			}
			else
			{
				await ExecuteSequence();
			}
		}

		private async UniTask ExecuteParallel()
		{
			var tasks = new UniTask[actions.Count];

			for (var index = 0; index < actions.Count; index++)
			{
				var action = actions[index];

				tasks[index] = action.Execute();
			}

			await UniTask.WhenAll(tasks);
		}
		private async UniTask ExecuteSequence()
		{
			foreach (var action in actions)
			{
				await action.Execute();
			}
		}
	}
}