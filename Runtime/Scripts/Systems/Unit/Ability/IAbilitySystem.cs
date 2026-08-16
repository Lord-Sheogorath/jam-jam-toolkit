using System.Linq;

namespace LordSheo.JJTK
{
	public class AbilityContext
	{
		public int instanceId;
		public float startTime;
		
		public IAbility ability;
	}
	
	public interface IAbilitySystem : ISystem,
		ITickable
	{
		AbilityContext Start(int id, int level);
		void Stop(int instanceId);

		void Add(IAbilityTemplate template);
		void Remove(IAbilityTemplate template);
		
		bool TryGetTemplate(int id, out IAbilityTemplate template);
	}
}