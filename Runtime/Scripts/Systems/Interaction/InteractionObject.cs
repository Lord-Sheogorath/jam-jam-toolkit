using System;
using UnityEngine;
using UnityEngine.Events;

namespace LordSheo.JJTK
{
	public class InteractionObject : MonoBehaviour, IInteraction
	{
		public string displayName;
		
		public UnityEvent selectedEvent;
		
		[NonSerialized]
		public bool active = true;
		
		public event System.Action OnSelectEvent;

		public virtual bool IsActive => active;
		
		protected virtual void OnEnable()
		{
			InteractionState.interactions.Add(this);
		}

		protected virtual void OnDisable()
		{
			InteractionState.interactions.Remove(this);
		}

		public virtual void Select()
		{
			OnSelectEvent?.Invoke();
			selectedEvent?.Invoke();
		}
	}
}