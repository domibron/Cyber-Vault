using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CyberVault
{
	public class Computer : MonoBehaviour
	{
		public static Computer Instance;

		// dont do this ever!! have a proper animation script!
		public Animator Animator;

		public bool UsingComputer = false;

		public GameObject UIMain;

		public GameObject[] UIElements;

		public Button DoorButton;
		public Button LaserButton;
		public Button TurretsButton;

		public Button LittleDoorButton;
		public Button LittleLaserButton;
		public Button LittleTurretsButton;

		public bool OverridingEscape = false;

		void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this.gameObject);
			}
			else
			{
				Instance = this;
			}
		}

		void Start()
		{
			OpenScreen(0);

			UIMain.SetActive(false);
		}

		void Update()
		{
			if (UsingComputer && Input.GetKeyDown(KeyCode.Escape) && !OverridingEscape)
			{
				ExitComputer();
			}

			if (UsingComputer)
			{
				if (GameManager.Instance.DoorUnlocked && DoorButton.interactable)
				{
					DoorButton.interactable = false;
					LittleDoorButton.interactable = false;
				}

				if (!GameManager.Instance.LasersOnline && LaserButton.interactable)
				{
					LaserButton.interactable = false;
					LittleLaserButton.interactable = false;
				}

				if (!GameManager.Instance.TurretsOnline && TurretsButton.interactable)
				{
					TurretsButton.interactable = false;
					LittleTurretsButton.interactable = false;
				}
			}
		}

		public void UseComputer()
		{


			// save me making a call to the interactavble sctipt thing.
			if (!UsingComputer) StartCoroutine(StartUsingComputer());



		}

		public IEnumerator StartUsingComputer()
		{
			PauseMenu.instance.OverridingPause = true;
			PauseMenu.instance.Resume();

			Animator.SetBool("pluggedin", true);

			yield return new WaitForSeconds(0.9f);


			UIMain.SetActive(true);
			OpenScreen(0);

			GameManager.Instance.PlayerLockLook = true;
			GameManager.Instance.PlayerLockMovement = true;
			GameManager.Instance.PlayerMouseVisible = true;

			UsingComputer = true;
		}

		public void ExitComputer()
		{
			if (UsingComputer)
			{
				StopCoroutine(StartUsingComputer());

				UsingComputer = false;
				OpenScreen(0);
				UIMain.SetActive(false);

				GameManager.Instance.PlayerLockLook = false;
				GameManager.Instance.PlayerLockMovement = false;
				GameManager.Instance.PlayerMouseVisible = false;

				PauseMenu.instance.OverridingPause = false;

				Animator.SetBool("pluggedin", false);
			}
		}

		public void OpenScreen(int screenNumb)
		{
			for (int i = 0; i < UIElements.Length; i++)
			{
				if (i != screenNumb)
				{
					UIElements[i].SetActive(false);
				}
				else
				{
					UIElements[i].SetActive(true);
				}
			}
		}
	}
}
