using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceRandomAudioClipAction : IJuiceAction
	{
		public AudioSource source;
		public List<AudioClip> clips = new();
		
		public async Task Execute()
		{
			var clip = clips.Random();
			source.PlayOneShot(clip);

			await Task.Delay(TimeSpan.FromSeconds(clip.length));
		}
	}
}