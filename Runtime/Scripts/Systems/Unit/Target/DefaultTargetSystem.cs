using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class DefaultTargetSystem : ITargetSystem
	{
		private class Cache
		{
			public float range;
			public float time;
			
			public List<UnitController> units = new();
		}
		
		private const float CACHE_DURATION = 1f;
		
		private readonly List<Cache> _cache = new();
		
		public IEnumerable<UnitController> Find(Vector3 point, float range)
		{
			var cache = _cache.FirstOrDefault(c => Mathf.Approximately(range, c.range));

			var refresh = cache == null || (Time.realtimeSinceStartup - cache.time) >= CACHE_DURATION;
			
			if (refresh)
			{
				cache = CreateCache(point, range);
				_cache.Add(cache);
			}

			return cache.units;
		}
		
		private Cache CreateCache(Vector3 point, float range)
		{
			var cache = new Cache()
			{
				range = range,
				time = Time.realtimeSinceStartup,
			};

			var colliders = Physics.OverlapSphere(point, range);
			
			foreach (var hit in colliders)
			{
				var unit = hit.GetComponentInParent<UnitController>();

				if (unit == null)
				{
					continue;
				}

				cache.units.Add(unit);
			}

			return cache;
		}
	}
}