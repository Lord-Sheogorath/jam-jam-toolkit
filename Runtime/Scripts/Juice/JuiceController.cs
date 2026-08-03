using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class JuiceController : MonoBehaviour,
		IJuiceAction
	{
		[SerializeReference]
		public IJuiceAction action;

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

		public void DOExecute()
		{
			_ = Execute();
		}
		
		public virtual async Task Execute()
		{
			if (Application.isPlaying == false)
			{
				return;
			}

			if (delayOnExecute > float.Epsilon)
			{
				await Task.Delay(TimeSpan.FromSeconds(delayOnExecute));
			}
			
			OnBeforeExecuteEvent?.Invoke();
			await action.Execute();
			OnAfterExecuteEvent?.Invoke();
		}
	}
}