using System;
using System.Collections.Generic;

namespace LordSheo.JJTK
{
	public static class EnumerableExtensions
	{
		public static bool IsNullOrEmpty<T>(this ICollection<T> source)
		{
			return source == null || source.Count == 0;
		}

		public static void Perform<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (var item in source)
			{
				action.Invoke(item);
			}
		}

		public static T Random<T>(this IList<T> source)
		{
			var index = UnityEngine.Random.Range(0, source.Count - 1);
			return source[index];
		}
		public static void AddRange<T>(HashSet<T> source, IEnumerable<T> collection)
		{
			foreach (var elem in collection)
			{
				source.Add(elem);
			}
		}

		public static TValue GetOrAddDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue defaultValue = default)
		{
			if (source.TryGetValue(key, out var value) == false)
			{
				source[key] = value = defaultValue;
			}

			return value;
		}

		public static void AddToCurrentValue<TKey>(this IDictionary<TKey, int> source, TKey key, int value)
		{
			source.TryGetValue(key, out var current);
			source[key] = current + value;
		}
		public static void SubtractFromCurrentValue<TKey>(this IDictionary<TKey, int> source, TKey key, int value)
		{
			source.TryGetValue(key, out var current);
			source[key] = current - value;
		}
		
		public static void AddToCurrentValue<TKey>(this IDictionary<TKey, float> source, TKey key, float value)
		{
			source.TryGetValue(key, out var current);
			source[key] = current + value;
		}
		public static void SubtractFromCurrentValue<TKey>(this IDictionary<TKey, float> source, TKey key, float value)
		{
			source.TryGetValue(key, out var current);
			source[key] = current - value;
		}
	}
}