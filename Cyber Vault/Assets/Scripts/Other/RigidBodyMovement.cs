using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CyberVault
{
	public class RigidBodyMovement : MonoBehaviour
	{
		[SerializeField]
		private float forceMagnitude;

		void OnControllerColliderHit(ControllerColliderHit hit)
		{
			Rigidbody rigidbody = hit.collider.attachedRigidbody;

			if (rigidbody != null)
			{
				Vector3 forceDirection = hit.gameObject.transform.position - transform.position;

				forceDirection.y = 0;
				forceDirection.Normalize();

				rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
			}
		}
	}
}
