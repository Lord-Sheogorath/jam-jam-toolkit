using UnityEngine;

namespace LordSheo.JJTK
{
	public static class VectorExtensions
	{
		public static int Random(this Vector2Int range)
		{
			return UnityEngine.Random.Range(range.x, range.y);
		}

		public static float Random(this Vector2 range)
		{
			return UnityEngine.Random.Range(range.x, range.y);
		}
	}
}