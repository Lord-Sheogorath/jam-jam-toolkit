using System;
using System.Collections;
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
			EnumeratorObject.Instance.StartCoroutine(Execute());
		}
		
		public virtual IEnumerator Execute()
		{
			if (Application.isPlaying == false)
			{
				yield break;
			}

			if (delayOnExecute > float.Epsilon)
			{
				yield return new WaitForSeconds(delayOnExecute);
			}
			
			yield return Execute_Internal();
		}

		private IEnumerator Execute_Internal()
		{
			OnBeforeExecuteEvent?.Invoke();
			yield return action.Execute();
			OnAfterExecuteEvent?.Invoke();
		}
	}
}