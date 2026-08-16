namespace LordSheo.JJTK
{
	public interface IAbility : ITickable
	{
		bool IsActive { get; }
		
		void Setup(UnitController owner, int level);
		
		void Start();
		void Stop();
	}
}