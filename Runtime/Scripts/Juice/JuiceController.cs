using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceController : MonoBehaviour,
		IJuiceAction
	{
		[SerializeReference]
		public List<IJuiceAction> actions = new();
		public bool executeInParallel = false;

		[Header("TIMING")]
		public float delayOnExecute = 0;

		public bool executeOnAwake;
		public bool executeOnStart;
		public bool executeOnEnable;
		public bool executeOnDisable;
		public bool executeOnDestroy;
		public bool executeAwakeEnableAndStartOnNextFrame = true;

		public event System.Action OnBeforeExecuteEvent;
		public event System.Action OnAfterExecuteEvent;

		private CancellationTokenSource _cancellationTokenSource = new();

		private void Awake()
		{
			if (executeOnAwake)
			{
				if (executeAwakeEnableAndStartOnNextFrame)
				{
					EnumeratorObject.Instance.PerformOnNextFrame(DOExecute);
				}
				else
				{
					DOExecute();
				}
			}
		}

		private void Start()
		{
			if (executeOnStart)
			{
				if (executeAwakeEnableAndStartOnNextFrame)
				{
					EnumeratorObject.Instance.PerformOnNextFrame(DOExecute);
				}
				else
				{
					DOExecute();
				}
			}
		}

		private void OnEnable()
		{
			if (executeOnEnable)
			{
				if (executeAwakeEnableAndStartOnNextFrame)
				{
					EnumeratorObject.Instance.PerformOnNextFrame(DOExecute);
				}
				else
				{
					DOExecute();
				}
			}
		}

		private void OnDisable()
		{
			// Check to see if the scene is being unloaded (leaving playmode).
			if (executeOnDisable && gameObject.scene.isLoaded == false)
			{
				DOExecute();
			}
		}

		private void OnDestroy()
		{
			// Check to see if the scene is being unloaded (leaving playmode).
			if (executeOnDestroy && gameObject.scene.isLoaded)
			{
				DOExecute();
			}
		}

		[Button]
		public void DOExecute()
		{
			DOCancel();
			
			_cancellationTokenSource = new();
			_ = Execute(_cancellationTokenSource.Token);
		}

		[Button]
		public void DOCancel()
		{
			_cancellationTokenSource?.Cancel();
			_cancellationTokenSource?.Dispose();
			_cancellationTokenSource = null;
		}
		
		public virtual async UniTask Execute(CancellationToken token)
		{
			if (Application.isPlaying == false)
			{
				return;
			}
			
			if (delayOnExecute > float.Epsilon)
			{
				await UniTask.Delay(TimeSpan.FromSeconds(delayOnExecute), DelayType.DeltaTime, cancellationToken: token);
			}

			var sequence = new JuiceSequenceAction();
			sequence.actions.AddRange(actions);
			sequence.parallel = executeInParallel;
			
			OnBeforeExecuteEvent?.Invoke();
			
			try
			{
				await sequence.Execute(token);
			}
			catch (OperationCanceledException)
			{
				// Expected: object destroyed, interrupted, replaced, etc.
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
			
			OnAfterExecuteEvent?.Invoke();
		}
	}
}