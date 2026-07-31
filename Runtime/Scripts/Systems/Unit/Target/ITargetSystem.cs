using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public interface ITargetSystem : ISystem
	{
		IEnumerable<UnitController> Find(Vector3 point, float range);
	}
}