namespace LordSheo.JJTK
{
	public struct ChangedFloatValue
	{
		public ChangedNumValueType type;
		
		public float requestedAmount;
		public float actualAmount;

		public float previous;
		public float current;
	}
}