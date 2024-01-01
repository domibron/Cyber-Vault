using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class Laser : MonoBehaviour
	{
		public float Damage = 100;

		void OnTriggerEnter(Collider other)
		{

			if (other.gameObject.layer == 3)
			{
				other.GetComponent<IHealth>()?.TakeDamage(Damage);
			}
		}

		void OnTriggerStay(Collider other)
		{
			if (other.gameObject.layer == 3)
			{
				other.GetComponentInParent<IHealth>()?.TakeDamage(Damage);
			}
		}
	}
}
