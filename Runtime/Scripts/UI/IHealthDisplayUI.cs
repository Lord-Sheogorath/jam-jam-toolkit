namespace LordSheo.JJTK
{
	public interface IHealthDisplayUI
	{
		IHealthSystem Current { get; }
		
		void Show(IHealthSystem healthSystem);
		void Refresh();
	}
}