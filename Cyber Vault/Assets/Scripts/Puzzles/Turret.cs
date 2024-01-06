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

		public Light Spot;
		public Light Point;

		public float Damage = 20;

		public float FireRate = 1f;

		public bool Active = true;



		private AudioSource _audioSource;

		public AudioClip ShotSFX;
		public AudioClip BeepSFX;

		public ParticleSystem Effects;

		private float x;
		private float z;
		private float y;

		private bool _targetLock = false;
		private bool _targetLockedSuccsess = false;

		private RaycastHit hit;

		private float _time = 0;
		private float _waitTime = 0;
		private int _count = 0;

		// Start is called before the first frame update
		void Start()
		{
			if (Player == null) Player = GameObject.FindWithTag("Player").transform;
			_audioSource = GetComponent<AudioSource>();
		}

		// Update is called once per frame
		void Update()
		{
			if (GameManager.Instance.TurretsOnline != Active) Active = GameManager.Instance.TurretsOnline;

			if (Active)
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



				int layer = 6;
				layer = 1 << layer;
				layer = ~layer;

				Physics.Raycast(TurretBody.position, Player.position - TurretBody.position, out hit, Vector3.Distance(TurretBody.position, Player.position), layer);

				_time += Time.deltaTime;

				// move to IEnerator
				if (hit.collider != null && hit.transform.gameObject.layer == 3)
				{
					_targetLock = true;

					StartCoroutine(TurretShoot());

				}
				else
				{
					_targetLock = false;
					_targetLockedSuccsess = false;
					_count = 0;
					_waitTime = 0;
				}

				Spot.intensity = 5;
				Point.intensity = 150;

			}
			else
			{
				Spot.intensity = 0;
				Point.intensity = 0;
				StopCoroutine(TurretShoot());
			}
		}

		private IEnumerator TurretShoot()
		{

			while (!_targetLockedSuccsess && _targetLock)
			{
				if (_time > _waitTime)
				{
					_audioSource.Stop();
					_audioSource.clip = BeepSFX;
					_audioSource.Play();

					_count++;
					_waitTime = _time + FireRate;
				}

				if (_count >= 4)
				{
					_targetLockedSuccsess = true;
					_waitTime = 0;
				}

				yield return new();
			}




			while (_targetLock && _targetLockedSuccsess)
			{
				if (_waitTime < _time)
				{
					print("ENGAGING");
					_audioSource.Stop();
					_audioSource.clip = ShotSFX;
					_audioSource.Play();

					Effects.Play();

					Player.GetComponent<IHealth>().TakeDamage(Damage);


					_waitTime = _time + FireRate;
				}

				yield return new();

			}

			if (!_targetLock && _targetLockedSuccsess)
			{
				print("lost target");
				_audioSource.Stop();
				_audioSource.clip = BeepSFX;
				_audioSource.Play();
				_targetLockedSuccsess = false;
				_count = 0;
				_waitTime = 0;
			}

			yield return new();
		}
	}
}
