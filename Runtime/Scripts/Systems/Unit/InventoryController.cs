using System;
using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class InventoryController : MonoBehaviour
	{
		public event System.Action<ResourceType, int> OnResourceAdded
		{
			add => GetInventory().OnResourceAdded += value;
			remove => GetInventory().OnResourceAdded -= value;
		}
		
		public event System.Action<ResourceType, int> OnResourceRemoved
		{
			add => GetInventory().OnResourceRemoved += value;
			remove => GetInventory().OnResourceRemoved -= value;
		}
		public event System.Action<ResourceType> OnResourceChange
		{
			add => GetInventory().OnResourceChange += value;
			remove => GetInventory().OnResourceChange -= value;
		}

		protected Inventory inventory = new();
		
		protected virtual Inventory GetInventory()
		{
			return inventory;
		}
		
		public int GetResourceCount(ResourceType type)
		{
			return GetInventory().GetResourceCount(type);
		}

		public void SetResourceCount(ResourceType type, int value)
		{
			GetInventory().SetResourceCount(type, value);
		}
		public void AddResourceCount(ResourceType type, int amount)
		{
			GetInventory().AddResourceCount(type, amount);
		}
		public bool RemoveResourceCount(ResourceType type, int amount)
		{
			return GetInventory().RemoveResourceCount(type, amount);
		}
	}
}