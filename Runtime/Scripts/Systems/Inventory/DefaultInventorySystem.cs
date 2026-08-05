using System;
using System.Collections.Generic;
using TMPro;

namespace LordSheo.JJTK
{
	public class DefaultInventorySystem : IInventorySystem
	{
		public event System.Action<string, ChangedIntValue> OnAddedEvent;
		public event System.Action<string, ChangedIntValue> OnRemovedEvent;
		public event System.Action<string, ChangedIntValue> OnChangedEvent;
		
		private readonly Dictionary<string, InventoryItem> _items = new();

		public bool Contains(EnumString itemId)
		{
			return _items.ContainsKey(itemId);
		}

		public InventoryItem Get(EnumString itemId)
		{
			return _items.GetValueOrDefault(itemId);
		}

		
		private ChangedIntValue Modify(EnumString itemId, int value, ChangedNumValueType type)
		{
			var item = Get(itemId);

			if (item == null)
			{
				_items[itemId] = item = new InventoryItem();
			}
			
			var change = new ChangedIntValue()
			{
				type = type,
				requestedAmount = value,
				actualAmount = value,
				previous = item.amount,
				current = value,
			};

			item.amount = type switch
			{
				ChangedNumValueType.Add => item.amount + value,
				ChangedNumValueType.Remove => item.amount - value,
				ChangedNumValueType.Set => value,
				
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
			};

			return change;
		}

		public void Set(EnumString itemId, int value)
		{
			var change = Modify(itemId, value, ChangedNumValueType.Set);				
			OnChangedEvent?.Invoke(itemId, change);
		}

		public void Add(EnumString itemId, int amount)
		{
			if (amount <= 0)
			{
				return;
			}
			
			var change = Modify(itemId, amount, ChangedNumValueType.Add);
			OnAddedEvent?.Invoke(itemId, change);
		}
		public bool Remove(EnumString itemId, int amount)
		{
			if (amount < 0)
			{
				return false;
			}

			if (amount == 0)
			{
				return true;
			}
			
			var item = Get(itemId);

			if (item == null || item.amount < amount)
			{
				return false;
			}

			var change = Modify(itemId, amount, ChangedNumValueType.Remove);
			OnRemovedEvent?.Invoke(itemId, change);

			return true;
		}
	}
}