namespace LordSheo.JJTK
{
	public interface IUnitAction
	{
		void OnEnter(UnitController user);
		void OnExit();
		void OnUpdate();

		bool IsValid(UnitController user);
	}
}