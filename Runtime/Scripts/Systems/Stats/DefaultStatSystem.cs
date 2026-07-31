using System.Collections.Generic;

namespace LordSheo.JJTK
{
	public class DefaultStatSystem : IStatSystem
	{
		public event System.Action<StatType, ChangedFloatValue> OnAddedEvent;
		public event System.Action<StatType, ChangedFloatValue> OnRemovedEvent;
		public event System.Action<StatType, ChangedFloatValue> OnChangedEvent;

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
			var change = new ChangedFloatValue()
			{
				type = ChangedNumValueType.Set,
				
				requestedAmount = value,
				actualAmount = value,
				
				previous = Get(type),
				current = value,
			};
			
			_stats[type] = value;
			OnChangedEvent?.Invoke(type, change);
		}
		public virtual void Add(StatType type, float amount)
		{
			var value = Get(type) + amount;
			
			var change = new ChangedFloatValue()
			{
				type = ChangedNumValueType.Add,
				
				requestedAmount = amount,
				actualAmount = amount,
				
				previous = _stats.GetValueOrDefault(type),
				current = value,
			};
			
			Set(type, value);
			
			OnAddedEvent?.Invoke(type, change);
		}
		public virtual bool Remove(StatType type, float amount)
		{
			var value = Get(type) - amount;
			
			var change = new ChangedFloatValue()
			{
				type = ChangedNumValueType.Remove,
				
				requestedAmount = amount,
				actualAmount = amount,
				
				previous = _stats.GetValueOrDefault(type),
				current = value,
			};
			
			Set(type, value);

			OnRemovedEvent?.Invoke(type, change);
			
			return true;
		}
	}
}