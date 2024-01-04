using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CyberVault
{
	public class GasController : MonoBehaviour
	{
		public float Damage = 10;

		public float TickRate = 1f;

		public bool Active = false;

		public List<ParticleSystem> ParticalSystem;

		private float _time = 0;
		private float _waitTime = 0;

		public void StartGas()
		{
			Active = true;


			foreach (var item in ParticalSystem.ToList<ParticleSystem>())
			{
				item.Play();
			}
		}

		void OnTriggerStay(Collider other)
		{
			if (other.gameObject.layer == 3 && other.tag == "Player" && Active)
			{
				StartCoroutine(DoDamage(other.transform.GetComponent<IHealth>()));
			}
		}

		// Update is called once per frame
		void Update()
		{
			_time += Time.deltaTime;
		}

		IEnumerator DoDamage(IHealth health)
		{

			if (_waitTime < _time)
			{
				health.TakeDamage(Damage);

				_waitTime = _time + TickRate;

			}

			yield return new();
		}
	}
}
