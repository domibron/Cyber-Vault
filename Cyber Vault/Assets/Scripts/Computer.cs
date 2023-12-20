using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class Computer : MonoBehaviour
	{
		public bool UsingComputer = false;

		public GameObject UIMain;

		public GameObject[] UIElements;

		void Start()
		{
			OpenScreen(0);

			UIMain.SetActive(false);
		}

		void Update()
		{
			if (UsingComputer && Input.GetKeyDown(KeyCode.Escape))
			{
				ExitComputer();
			}
		}

		public void UseComputer()
		{
			// save me making a call to the interactavble sctipt thing.
			if (!UsingComputer)
			{
				UsingComputer = true;

				UIMain.SetActive(true);
				OpenScreen(0);

				GameManager.Instance.PlayerLockLook = true;
				GameManager.Instance.PlayerLockMovement = true;
				GameManager.Instance.PlayerMouseVisible = true;
			}

		}

		public void ExitComputer()
		{
			if (UsingComputer)
			{
				UsingComputer = false;
				OpenScreen(0);
				UIMain.SetActive(false);

				GameManager.Instance.PlayerLockLook = false;
				GameManager.Instance.PlayerLockMovement = false;
				GameManager.Instance.PlayerMouseVisible = false;
			}
		}

		private void OpenScreen(int screenNumb)
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
