using System;
using System.Collections.Generic;

namespace LordSheo.JJTK
{
	public class DefaultSignalSystem : ISignalSystem
	{
		// NOTE: Potentially add buffered dictionary similar to UpdateSystem
		private readonly Dictionary<Type, Delegate> _listeners = new();

		public void Subscribe<TSignal>(Action<TSignal> listener)
			where TSignal : ISignal
		{
			var type = typeof(TSignal);

			if (_listeners.TryGetValue(type, out var existing))
			{
				_listeners[type] = Delegate.Combine(existing, listener);
			}
			else
			{
				_listeners[type] = listener;
			}
		}

		public void Unsubscribe<TSignal>(Action<TSignal> listener)
			where TSignal : ISignal
		{
			var type = typeof(TSignal);

			if (_listeners.TryGetValue(type, out var existing) == false)
			{
				return;
			}

			var result = Delegate.Remove(existing, listener);

			if (result == null)
			{
				_listeners.Remove(type);
			}
			else
			{
				_listeners[type] = result;
			}
		}

		public void Publish<TSignal>(TSignal signal)
			where TSignal : ISignal
		{
			var type = typeof(TSignal);

			if (_listeners.TryGetValue(type, out var existing))
			{
				((Action<TSignal>)existing)?.Invoke(signal);
			}
		}
	}
}