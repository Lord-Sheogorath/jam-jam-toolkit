using UnityEngine;

namespace LordSheo.JJTK
{
	[System.Serializable]
	public class ValueCurve
	{
		public float start = 0;
		public float mult = 1;
		public AnimationCurve curve = AnimationCurve.Linear(0, 1, 1, 1);

		public float GetValueAt(float t)
		{
			return start + (curve.Evaluate(t) * mult);
		}
		public int GetRoundValueAt(int t)
		{
			return Mathf.RoundToInt(GetValueAt(t));
		}
	}
}