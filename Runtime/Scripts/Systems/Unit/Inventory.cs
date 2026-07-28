using System.Collections.Generic;

namespace LordSheo.JJTK
{
	public enum ResourceType
	{
		Gold,
		Fish
	}

	public class Inventory
	{
		private readonly Dictionary<ResourceType, int> _resources = new();

		public event System.Action<ResourceType, int> OnResourceAdded;
		public event System.Action<ResourceType, int> OnResourceRemoved;
		public event System.Action<ResourceType> OnResourceChange;
		
		public int GetResourceCount(ResourceType type)
		{
			return _resources.GetValueOrDefault(type);
		}

		public void SetResourceCount(ResourceType type, int value)
		{
			_resources[type] = value;
			OnResourceChange?.Invoke(type);
		}
		public void AddResourceCount(ResourceType type, int amount)
		{
			if (amount <= 0)
			{
				return;
			}
			
			var current = GetResourceCount(type);
			
			SetResourceCount(type, current + amount);
			OnResourceAdded?.Invoke(type, amount);
		}
		public bool RemoveResourceCount(ResourceType type, int amount)
		{
			if (amount < 0)
			{
				return false;
			}

			if (amount == 0)
			{
				return true;
			}
			
			var current = GetResourceCount(type);

			if (current < amount)
			{
				return false;
			}
			
			SetResourceCount(type, current - amount);
			OnResourceRemoved?.Invoke(type, amount);

			return true;
		}
	}
}