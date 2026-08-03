namespace LordSheo.JJTK
{
	public interface IInteraction
	{
		event System.Action OnSelectEvent;
		
		bool IsActive { get; }
		
		void Select();
	}
}