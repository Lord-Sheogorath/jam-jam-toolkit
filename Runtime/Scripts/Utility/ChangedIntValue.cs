namespace LordSheo.JJTK
{
	public struct ChangedIntValue
	{
		public ChangedNumValueType type;
		
		public int requestedAmount;
		public int actualAmount;

		public int previous;
		public int current;
	}
}