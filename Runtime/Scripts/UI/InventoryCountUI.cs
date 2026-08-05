using TMPro;
using UnityEngine;

namespace LordSheo.JJTK
{
	public abstract class InventoryCountUI : MonoBehaviour
	{
		public TextMeshProUGUI displayNameText;
		public TextMeshProUGUI amountText;

		protected abstract string ItemId { get; }
		protected abstract BaseDefinition Definition { get; }
        
		protected IInventorySystem inventory;
        
		public virtual void Show(IInventorySystem inventory)
		{
			if (this.inventory != null)
			{
				this.inventory.OnChangedEvent -= OnInventoryChangedCallback;
			}
            
			this.inventory = inventory;
			this.inventory.OnChangedEvent += OnInventoryChangedCallback;

			Refresh();
		}

		protected virtual void OnInventoryChangedCallback(string itemId, ChangedIntValue change)
		{
			Refresh();
		}

		protected virtual void Refresh()
		{
			displayNameText.text = Definition?.displayName ?? "UNKNOWN";
            
			var owned = inventory.Get(ItemId);
            
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