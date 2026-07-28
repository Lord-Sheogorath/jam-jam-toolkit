namespace LordSheo.JJTK
{
	public interface IUnitActionSystem : ISystem
	{
		IUnitAction GetActive();
		void SetActive(IUnitAction action);
		void StopActive();
	}
}