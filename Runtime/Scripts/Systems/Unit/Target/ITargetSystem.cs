using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public interface ITargetSystem : ISystem
	{
		IEnumerable<UnitController> Find(Transform point, float range);
	}
}