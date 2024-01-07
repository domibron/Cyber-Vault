using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

namespace CyberVault
{
	public class EndGameState : MonoBehaviour
	{
		public static EndGameState instance;

		public GameObject WinScreen;
		public TMP_Text TimerText;

		public GameObject LoseScreen;

		private bool running = false;

		void Awake()
		{
			if (instance != null && instance != this)
			{
				Destroy(this.gameObject);
			}
			else
			{
				instance = this;
			}
		}

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

			PauseMenu.instance.Resume();
			PauseMenu.instance.Open = false;
			PauseMenu.instance.PauseMenuScreen.gameObject.SetActive(false);
			PauseMenu.instance.enabled = false;

			GameManager.Instance.PlayerMouseVisible = true;

			GameManager.Instance.PlayerLockLook = true;
			GameManager.Instance.PlayerLockMovement = true;


			if (win)
			{
				StartCoroutine(Win());
			}
			else
			{
				StartCoroutine(Lose());
			}
		}

		IEnumerator Win()
		{

			if (GameManager.Instance.Timer < PlayerPrefs.GetFloat("bt", 999999))
			{
				TimerText.text = "New time! " + GameManager.Instance.TimerText.text;

				PlayerPrefs.SetString("bts", GameManager.Instance.TimerText.text);
				PlayerPrefs.SetFloat("bt", GameManager.Instance.Timer);
				PlayerPrefs.Save();
			}
			else
			{

				TimerText.text = $"Time to beat: {PlayerPrefs.GetString("bts", "00:00.00")}\nTime achieved: {GameManager.Instance.TimerText.text}";
			}


			yield return new WaitForSeconds(0.1f);

			WinScreen.SetActive(true);


		}

		IEnumerator Lose()
		{
			//TimerText.text = GameManager.Instance.TimerText.text;

			yield return new WaitForSeconds(2f);

			LoseScreen.SetActive(true);


		}

		public void Retry()
		{
			SceneManager.LoadScene(1);
		}

		public void MainMenu()
		{
			SceneManager.LoadScene(0);
		}

		public void Quit()
		{
			Application.Quit();
		}
	}
}
