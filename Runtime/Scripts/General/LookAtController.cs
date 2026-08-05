using UnityEngine;

namespace LordSheo.JJTK
{
	public class LookAtController : MonoBehaviour
	{
		public float speed;
		
		[Space]
		
		public bool lockX;
		public bool lockY;
		public bool lockZ;

		public bool IsLooking => IsLookingAtTarget(1f);
		
		private Vector3 _target;
		private Vector3 _direction;
		private float _angle;

		public void SetTarget(Vector3 target)
		{
			_target = target;
			_direction = (_target - transform.position).normalized;
		}

		public void Update()
		{
			if (_target == Vector3.zero)
			{
				return;
			}

			// Direction to target
			_direction = (_target - transform.position).normalized;

			// Desired rotation
			Quaternion lookRot = Quaternion.LookRotation(_direction, Vector3.up);

			// Apply constraints by zeroing out unwanted axes in Euler space
			Vector3 euler = lookRot.eulerAngles;
			Vector3 currentEuler = transform.rotation.eulerAngles;

			if (lockX)
			{
				euler.x = currentEuler.x;
			}
			if (lockY)
			{
				euler.y = currentEuler.y;
			}
			if (lockZ)
			{
				euler.z = currentEuler.z;
			}

			lookRot = Quaternion.Euler(euler);
			_angle = Quaternion.Angle(transform.rotation, lookRot);
			
			if (IsLooking)
			{
				return;
			}

			// Smooth rotation toward target
			transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, speed * Time.deltaTime);
		}

		public bool IsLookingAtTarget(float degreeOffset)
		{
			return _angle <= degreeOffset;
		}
	}
}