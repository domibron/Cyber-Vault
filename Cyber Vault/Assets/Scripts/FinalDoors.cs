using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class FinalDoors : MonoBehaviour
	{
		public Transform DoorLeft;
		public Transform DoorRight;

		public Vector3 RotationLeft;
		public Vector3 RotationRight;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			if (GameManager.Instance.HasDataShard)
			{
				DoorLeft.transform.localRotation = Quaternion.Euler(RotationLeft);
				DoorRight.transform.localRotation = Quaternion.Euler(RotationRight);
			}
		}
	}
}
