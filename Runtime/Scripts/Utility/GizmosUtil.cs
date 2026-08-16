using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public static class GizmosUtil
	{
		private static readonly Queue<Color> _colorScopes = new();

		public static void StartColorScope(Color color)
		{
			_colorScopes.Enqueue(Gizmos.color);

			Gizmos.color = color;
		}

		public static void EndColorScope()
		{
			if (_colorScopes.Count == 0)
			{
				Debug.LogError("Must call StartColorScope before EndColorScope.");
				return;
			}
			
			Gizmos.color = _colorScopes.Dequeue();
		}
	}
}