namespace LordSheo.JJTK
{
	public interface IUnitActionSystem : ISystem
	{
		IUnitAction GetActive();
		
		void Start(IUnitAction action);
		void Stop();
	}
}