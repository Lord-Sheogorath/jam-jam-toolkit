using System.Diagnostics.Contracts;

namespace LordSheo.JJTK
{
	public struct ChangedIntValue
	{
		public ChangedNumValueType type;
		
		public int requestedAmount;
		public int actualAmount;

		public int previous;
		public int current;

		[Pure]
		public string ToDisplayString()
		{
			return $"{{(type: {type}), (request: {requestedAmount}), (actual: {actualAmount}), (prev: {previous}), (current: {current})}}";
		}
	}
}