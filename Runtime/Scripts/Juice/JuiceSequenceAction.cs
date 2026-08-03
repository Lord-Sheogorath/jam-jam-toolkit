using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceSequenceAction : IJuiceAction
	{
		[SerializeReference]
		public List<IJuiceAction> actions = new();
		
		public IEnumerator Execute()
		{
			foreach (var action in actions)
			{
				yield return action.Execute();
			}
		}
	}
}