using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CyberVault
{
	public class Health : MonoBehaviour, IHealth
	{
		public Image HealthBar;

		public float MaxHealth;
		public float CurrentHealth;

		public Animator CameraAnimator;

		private bool dead = false;

		public void TakeDamage(float damage)
		{
			CurrentHealth -= damage;
		}


		// Start is called before the first frame update
		void Start()
		{
			CurrentHealth = MaxHealth;
		}

		// Update is called once per frame
		void Update()
		{
			if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;

			if (HealthBar != null)
			{
				HealthBar.fillAmount = CurrentHealth / MaxHealth;
			}

			if (CurrentHealth < 0)
			{



				GameManager.Instance.PlayerLockLook = true;
				GameManager.Instance.PlayerLockMovement = true;

				if (!dead)
				{
					dead = true;
					StartCoroutine(EndGame());

					GameManager.Instance.EndGame(false);
				}


				// DIE
			}
		}

		IEnumerator EndGame()
		{
			CameraAnimator.SetBool("dead", true);
			yield return new WaitForSeconds(2f);

			GameManager.Instance.Dead = true;
		}


	}
}
