using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class TargetController : MonoBehaviour
	{
		private class Cache
		{
			public float range;
			public float time;
			
			public List<UnitController> units = new();
		}

		private const float CACHE_DURATION = 1f;

		public LayerMask mask;
		
		private UnitController _unit;
		
		private readonly List<Cache> _cache = new();

		private void Awake()
		{
			_unit = GetComponentInParent<UnitController>();
		}

		public IReadOnlyList<UnitController> Find(float range, bool forcedRefresh = false)
		{
			var cache = _cache.FirstOrDefault(c => Mathf.Approximately(range, c.range));

			var refresh = forcedRefresh
				|| cache == null
				|| (Time.realtimeSinceStartup - cache.time) >= CACHE_DURATION;
			
			if (refresh)
			{
				cache = CreateCache(range);
				_cache.Add(cache);
			}

			return cache.units;
		}

		public UnitController GetClosestTo(Vector3 position, IReadOnlyList<UnitController> units)
		{
			var closestDistSqrd = 0f;
			var closestUnit = default(UnitController);
			
			foreach (var unit in units)
			{
				var distSqrd = (position - unit.VisualRoot.position).sqrMagnitude;
				
				if (closestUnit == null || distSqrd < closestDistSqrd)
				{
					closestDistSqrd = distSqrd;
					closestUnit = unit;
				}
			}

			return closestUnit;
		}

		public IReadOnlyList<T> Find<T>(float range)
		{
			var colliders = Physics.OverlapSphere(_unit.VisualRoot.position, range);

			var comps = new List<T>();
			
			foreach (var hit in colliders)
			{
				var comp = hit.GetComponentInParent<T>();

				if (comp == null)
				{
					continue;
				}
				
				comps.Add(comp);
			}

			return comps;
		}		
		
		public T GetClosestTo<T>(Vector3 position, IReadOnlyList<T> targets)
			where T : MonoBehaviour
		{
			var closestDistSqrd = 0f;
			var closestUnit = default(T);
			
			foreach (var target in targets)
			{
				var distSqrd = (position - target.transform.position).sqrMagnitude;
				
				if (closestUnit == null || distSqrd < closestDistSqrd)
				{
					closestDistSqrd = distSqrd;
					closestUnit = target;
				}
			}

			return closestUnit;
		}
		
		private Cache CreateCache(float range)
		{
			var cache = new Cache()
			{
				range = range,
				time = Time.realtimeSinceStartup,
			};

			var colliders = Physics.OverlapSphere(_unit.VisualRoot.position, range, mask);
			
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