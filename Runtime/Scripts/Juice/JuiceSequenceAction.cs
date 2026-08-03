using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceSequenceAction : IJuiceAction
	{
		[SerializeReference]
		public List<IJuiceAction> actions = new();
		public bool parallel = false;
		
		public async Task Execute()
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

		private async Task ExecuteParallel()
		{
			var tasks = new Task[actions.Count];

			for (var index = 0; index < actions.Count; index++)
			{
				var action = actions[index];

				tasks[index] = action.Execute();
			}

			await Task.WhenAll(tasks);
		}
		private async Task ExecuteSequence()
		{
			foreach (var action in actions)
			{
				await action.Execute();
			}
		}
	}
}