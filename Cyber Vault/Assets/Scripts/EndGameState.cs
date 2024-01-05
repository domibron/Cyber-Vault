using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CyberVault
{
	public class EndGameState : MonoBehaviour
	{

		public GameObject WinScreen;
		public TMP_Text TimerText;

		public GameObject LoseScreen;

		// Start is called before the first frame update
		void Start()
		{
			WinScreen.SetActive(false);
			LoseScreen.SetActive(false);
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void EndGame(bool win)
		{
			Computer.Instance.ExitComputer();
			Computer.Instance.enabled = false;


			if (win)
			{

			}
			else
			{

			}
		}
	}
}
