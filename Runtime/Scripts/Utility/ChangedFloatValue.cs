namespace LordSheo.JJTK
{
	public struct ChangedFloatValue
	{
		public ChangedNumValueType type;
		
		public float requestedAmount;
		public float actualAmount;

		public float previous;
		public float current;
		
		public string ToDisplayString()
		{
			return $"{{(type: {type}), (request: {requestedAmount}), (actual: {actualAmount}), (prev: {previous}), (current: {current})}}";
		}
	}
}