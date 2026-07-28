using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace LordSheo.JJTK
{
	public interface IInteractable
	{
		void Interact();
	}

	public class InteractableSelector : MonoBehaviour
	{
		public LayerMask mask;
		
		public void Update()
		{
			if (Pointer.current == null)
			{
				return;
			}
			
			if (Pointer.current.press.wasPressedThisFrame)
			{
				Select();
			}
		}

		public void Select()
		{
			if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(Pointer.current.deviceId))
			{
				return;
			}
			
			// Create a ray from the camera through the mouse position
			Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);
			RaycastHit hit;

			// Perform the raycast
			if (Physics.Raycast(ray, out hit, float.MaxValue, mask))
			{
				var interactable = hit.transform.GetComponentInParent<IInteractable>();

				if (interactable != null)
				{
					interactable.Interact();
				}
			}
		}
	}
}