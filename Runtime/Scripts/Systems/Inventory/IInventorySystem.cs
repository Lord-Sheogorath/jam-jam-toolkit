namespace LordSheo.JJTK
{
	public enum ResourceType
	{
		gold,
	}

	public class InventoryItem
	{
		public string itemId;
		public int amount;
	}
	
	public interface IInventorySystem : ISystem
	{
		event System.Action<string, ChangedIntValue> OnAddedEvent;
		event System.Action<string, ChangedIntValue> OnRemovedEvent;
		event System.Action<string, ChangedIntValue> OnChangedEvent;

		bool Contains(EnumString itemId);
		InventoryItem Get(EnumString itemId);
		void Set(EnumString itemId, int value);
		
		void Add(EnumString itemId, int amount);
		bool Remove(EnumString itemId, int amount);
	}
}