using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuicePlayParticleSystemsAction : IJuiceAction
	{
		public List<ParticleSystem> systems = new();

#if UNITY_EDITOR
		[Button]
		private void GatherParticleSystems(GameObject target)
		{
			var subSystems = target.GetComponentsInChildren<ParticleSystem>();
			
			foreach (var sys in subSystems)
			{
				if (systems.Contains(sys) == false)
				{
					systems.Add(sys);
				}
			}
		}
#endif
		
		public UniTask Execute(CancellationToken token)
		{
			foreach (var sys in systems)
			{
				sys.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
				sys.Play(true);
			}

			return UniTask.CompletedTask;
		}
	}
}