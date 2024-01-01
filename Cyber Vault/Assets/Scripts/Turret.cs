using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class Turret : MonoBehaviour
	{
		public Transform Player;

		public Transform TurretBody;
		public Transform TurretWeapon;

		public float Damage = 20;

		float x;
		float z;
		float y;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			x = transform.position.x - Player.position.x;
			z = transform.position.z - Player.position.z;
			y = transform.position.y - Player.position.y;

			float yrot = Mathf.Atan(x / z) * (180 / Mathf.PI);
			float xrot = Mathf.Atan(Mathf.Sqrt(z * z + x * x) / y) * (180 / Mathf.PI);

			// inverting values.
			if (z > 0) yrot = 180 - yrot;
			//if (z > 0) xrot = 180 - xrot;

			if (z > 0)
			{
				yrot *= -1;
			}

			xrot = 180 - xrot;

			xrot -= 90;

			TurretBody.localRotation = Quaternion.Euler(0, yrot, 0);
			TurretWeapon.localRotation = Quaternion.Euler(xrot, 0, -90);

			RaycastHit hit;

			int layer = 6;
			layer = 1 << layer;
			layer = ~layer;

			Physics.Raycast(TurretBody.position, Player.position - TurretBody.position, out hit, Vector3.Distance(TurretBody.position, Player.position), layer);

			// move to IEnerator
			if (hit.collider != null && hit.transform.gameObject.layer == 3)
			{

				Player.GetComponent<IHealth>()?.TakeDamage(Damage);
			}

		}
	}
}
