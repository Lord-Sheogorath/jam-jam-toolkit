using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceAudioFadeInOutAction : IJuiceAction
	{
		public AudioSource source;

		[Header("FADE IN")]
		public float fadeInDuration = 0.25f;

		public float fadeInValue = 1;
		public Ease fadeInEase = Ease.InSine;

		[Header("FADE OUT")]
		public float fadeOutDuration = 0.25f;

		public float fadeOutValue = 0;
		public Ease fadeOutEase = Ease.OutSine;

		public async Task Execute()
		{
			source.Play();

			var taskCompletionSource = new TaskCompletionSource<bool>();
			var sequence = DOTween.Sequence();

			sequence.OnKill(() => taskCompletionSource.TrySetCanceled());
			sequence.OnComplete(() => taskCompletionSource.SetResult(true));

			var tween = DOTween.To(() => source.volume, val => source.volume = val, fadeInValue, fadeInDuration)
				.SetTarget(source)
				.SetEase(fadeInEase);

			sequence.Append(tween);

			tween = DOTween.To(() => source.volume, val => source.volume = val, fadeOutValue, fadeOutDuration)
				.SetTarget(source)
				.SetEase(fadeOutEase);
			
			sequence.Append(tween);

			sequence.Play();

			await taskCompletionSource.Task;
		}
	}
}