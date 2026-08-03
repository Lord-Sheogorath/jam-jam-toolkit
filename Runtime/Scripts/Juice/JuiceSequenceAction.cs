using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
		
		public async UniTask Execute(CancellationToken token)
		{
			if (parallel)
			{
				await ExecuteParallel(token);
			}
			else
			{
				await ExecuteSequence(token);
			}
		}

		private async UniTask ExecuteParallel(CancellationToken token)
		{
			var tasks = new UniTask[actions.Count];

			for (var index = 0; index < actions.Count; index++)
			{
				var action = actions[index];

				tasks[index] = action.Execute(token);
			}

			await UniTask.WhenAll(tasks)
				.AttachExternalCancellation(token);
		}
		private async UniTask ExecuteSequence(CancellationToken token)
		{
			foreach (var action in actions)
			{
				token.ThrowIfCancellationRequested();
				
				await action.Execute(token);
			}
		}
	}
}