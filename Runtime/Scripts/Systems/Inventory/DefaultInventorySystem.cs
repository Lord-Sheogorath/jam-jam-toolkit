using System.Collections.Generic;

namespace LordSheo.JJTK
{
	public class DefaultInventorySystem : IInventorySystem
	{
		public event System.Action<ResourceType, int> OnAddedEvent;
		public event System.Action<ResourceType, int> OnRemovedEvent;
		public event System.Action<ResourceType, int> OnChangedEvent;
		
		private readonly Dictionary<ResourceType, int> _resources = new();

		public bool Contains(ResourceType type)
		{
			return _resources.ContainsKey(type);
		}

		public int Get(ResourceType type)
		{
			return _resources.GetValueOrDefault(type);
		}

		public void Set(ResourceType type, int value)
		{
			_resources[type] = value;
			OnChangedEvent?.Invoke(type, value);
		}
		public void Add(ResourceType type, int amount)
		{
			if (amount <= 0)
			{
				return;
			}
			
			var current = Get(type);
			
			Set(type, current + amount);
			OnAddedEvent?.Invoke(type, amount);
		}
		public bool Remove(ResourceType type, int amount)
		{
			if (amount < 0)
			{
				return false;
			}

			if (amount == 0)
			{
				return true;
			}
			
			var current = Get(type);

			if (current < amount)
			{
				return false;
			}
			
			Set(type, current - amount);
			OnRemovedEvent?.Invoke(type, amount);

			return true;
		}
	}
}