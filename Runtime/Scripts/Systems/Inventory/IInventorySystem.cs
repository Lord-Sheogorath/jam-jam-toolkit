namespace LordSheo.JJTK
{
	public enum ResourceType
	{
		gold,
	}

	public interface IInventorySystem : ISystem
	{
		event System.Action<ResourceType, int> OnAddedEvent;
		event System.Action<ResourceType, int> OnRemovedEvent;
		event System.Action<ResourceType, int> OnChangedEvent;

		bool Contains(ResourceType type);
		int Get(ResourceType type);
		void Set(ResourceType type, int value);
		
		void Add(ResourceType type, int amount);
		bool Remove(ResourceType type, int amount);
	}
}