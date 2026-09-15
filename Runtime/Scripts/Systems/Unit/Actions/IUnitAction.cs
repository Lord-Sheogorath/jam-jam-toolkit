namespace LordSheo.JJTK
{
	public interface IUnitAction : ITickable
	{
		void Start(UnitController user);
		void Stop();

		bool IsValid(UnitController user);
	}
}