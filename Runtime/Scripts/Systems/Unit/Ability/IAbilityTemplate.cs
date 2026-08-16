namespace LordSheo.JJTK
{
	public interface IAbilityTemplate
	{
		int Id { get; }
		float Cooldown { get; }
		
		bool IsValid(UnitController owner);
		
		IAbility Create();
	}
}