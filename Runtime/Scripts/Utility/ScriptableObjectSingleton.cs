using UnityEngine;

namespace LordSheo.JJTK
{
	public class ScriptableObjectSingleton<T> : ScriptableObject
		where T : ScriptableObject
	{
		public static T Instance => Load();
		private static T _instance;

		private static T Load()
		{
			if (_instance == null)
			{
				_instance = Resources.Load<T>("Singletons/" + nameof(T));
			}

			return _instance;
		}
	}
}