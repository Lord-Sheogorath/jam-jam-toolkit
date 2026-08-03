using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceRandomAudioClipAction : IJuiceAction
	{
		public AudioSource source;
		public List<AudioClip> clips = new();
		
		public IEnumerator Execute()
		{
			var clip = clips.Random();
			source.PlayOneShot(clip);

			yield return clip.length;
		}
	}
}