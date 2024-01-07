using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class Lock : MonoBehaviour
	{
		public bool MX = false;
		public bool MY = false;
		public bool MZ = false;

		public bool RX = false;
		public bool RY = false;
		public bool RZ = false;

		public Vector3 defualtMove;
		public Quaternion defualtRotation;

		// Start is called before the first frame update
		void Start()
		{
			defualtMove = transform.position;
			defualtRotation = transform.rotation;
		}

		// Update is called once per frame
		void Update()
		{
			if (MX)
			{
				if (transform.position.x != defualtMove.x) transform.position = new Vector3(defualtMove.x, transform.position.y, transform.position.z);
			}

			if (MY)
			{
				if (transform.position.y != defualtMove.y) transform.position = new Vector3(transform.position.x, defualtMove.y, transform.position.z);
			}

			if (MZ)
			{
				if (transform.position.z != defualtMove.z) transform.position = new Vector3(transform.position.x, transform.position.y, defualtMove.z);
			}

			if (RX)
			{
				if (transform.rotation.x != defualtRotation.x) transform.rotation = new Quaternion(defualtRotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
			}

			if (RY)
			{
				if (transform.rotation.y != defualtRotation.y) transform.rotation = new Quaternion(transform.rotation.z, defualtRotation.y, transform.rotation.z, transform.rotation.w);
			}

			if (RZ)
			{
				if (transform.rotation.x != defualtRotation.z) transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, defualtRotation.z, transform.rotation.w);
			}
		}
	}
}
