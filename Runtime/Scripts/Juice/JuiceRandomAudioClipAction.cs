using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceRandomAudioClipAction : IJuiceAction
	{
		public AudioSource source;
		public List<AudioClip> clips = new();
		
		public async UniTask Execute()
		{
			var clip = clips.Random();
			source.PlayOneShot(clip);

			await UniTask.Delay(TimeSpan.FromSeconds(clip.length));
		}
	}
}