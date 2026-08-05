namespace LordSheo.JJTK
{
	public interface IInventoryCountUI
	{
		string ItemId { get; }
		BaseDefinition Definition { get; }
		
		IInventorySystem Current { get; }
		
		void Show(IInventorySystem inventorySystem);
		void Refresh();
	}
}