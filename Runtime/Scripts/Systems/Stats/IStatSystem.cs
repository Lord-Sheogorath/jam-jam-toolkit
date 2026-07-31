namespace LordSheo.JJTK
{
	public enum StatType
	{
		max_health,
		
		attack_speed,
		attack_damage,
		
		move_speed,
	}
	public interface IStatSystem : ISystem
	{
		event System.Action<StatType, ChangedFloatValue> OnAddedEvent;
		event System.Action<StatType, ChangedFloatValue> OnRemovedEvent;
		event System.Action<StatType, ChangedFloatValue> OnChangedEvent;

		public bool Contains(StatType type);
		public float Get(StatType type);
		public void Set(StatType type, float value);
		public void Add(StatType type, float amount);
		public bool Remove(StatType type, float amount);
	}
}