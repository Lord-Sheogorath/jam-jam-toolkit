using System.Collections.Generic;

namespace LordSheo.JJTK
{
	public class DefaultStatSystem : IStatSystem
	{
		public event System.Action<StatType, float> OnAddedEvent;
		public event System.Action<StatType, float> OnRemovedEvent;
		public event System.Action<StatType, float> OnChangedEvent;

		private readonly Dictionary<StatType, float> _stats = new();
		
		public virtual bool Contains(StatType type)
		{
			return _stats.ContainsKey(type);
		}

		public virtual float Get(StatType type)
		{
			return _stats.GetValueOrDefault(type);
		}

		public virtual void Set(StatType type, float value)
		{
			_stats[type] = value;
			OnChangedEvent?.Invoke(type, value);
		}
		public virtual void Add(StatType type, float amount)
		{
			var value = Get(type) + amount;
			Set(type, value);
			
			OnAddedEvent?.Invoke(type, amount);
		}
		public virtual bool Remove(StatType type, float amount)
		{
			var value = Get(type) - amount;
			Set(type, value);

			OnRemovedEvent?.Invoke(type, amount);
			
			return true;
		}
	}
}