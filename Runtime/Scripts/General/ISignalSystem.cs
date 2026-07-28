using System;

namespace LordSheo.JJTK
{
	public interface ISignalSystem : ISystem
	{
		void Subscribe<TSignal>(Action<TSignal> listener)
			where TSignal : ISignal;

		void Unsubscribe<TSignal>(Action<TSignal> listener)
			where TSignal : ISignal;

		void Publish<TSignal>(TSignal signal)
			where TSignal : ISignal;
	}
}