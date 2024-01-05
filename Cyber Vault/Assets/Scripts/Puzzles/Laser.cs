using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class Laser : MonoBehaviour
	{
		public float Damage = 100;

		public bool Active = true;

		private LineRenderer _lineRenderer;
		private Collider _collider;

		void Start()
		{
			_lineRenderer = GetComponent<LineRenderer>();
			_collider = GetComponent<Collider>();
		}

		void OnTriggerEnter(Collider other)
		{

			if (other.gameObject.layer == 3 && Active)
			{
				other.GetComponent<IHealth>()?.TakeDamage(Damage);
			}
		}

		void OnTriggerStay(Collider other)
		{
			if (other.gameObject.layer == 3 && Active)
			{
				other.GetComponentInParent<IHealth>()?.TakeDamage(Damage);
			}
		}

		void Update()
		{
			if (GameManager.Instance.LasersOnline != Active) Active = GameManager.Instance.LasersOnline;

			_lineRenderer.enabled = Active;
			_collider.enabled = Active;
		}

	}
}
