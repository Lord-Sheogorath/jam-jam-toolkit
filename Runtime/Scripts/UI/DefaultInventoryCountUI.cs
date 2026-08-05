using TMPro;
using UnityEngine;

namespace LordSheo.JJTK
{
	public abstract class DefaultInventoryCountUI : MonoBehaviour,
		IInventoryCountUI
	{
		public TextMeshProUGUI displayNameText;
		public TextMeshProUGUI amountText;

		public abstract string ItemId { get; }
		public abstract BaseDefinition Definition { get; }
        
		public IInventorySystem Current { get; protected set; }
        
		public virtual void Show(IInventorySystem inventory)
		{
			if (Current != null)
			{
				Current.OnChangedEvent -= OnInventoryChangedCallback;
			}
            
			Current = inventory;
			Current.OnChangedEvent += OnInventoryChangedCallback;

			Refresh();
		}

		protected virtual void OnInventoryChangedCallback(string itemId, ChangedIntValue change)
		{
			Refresh();
		}

		public virtual void Refresh()
		{
			displayNameText.text = Definition?.displayName ?? "UNKNOWN";
            
			var owned = Current.Get(ItemId);
            
			if (owned == null)
			{
				amountText.text = "0";
			}
			else
			{
				amountText.text = owned.amount.ToString();
			}
		}
	}
}