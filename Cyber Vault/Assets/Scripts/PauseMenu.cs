using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CyberVault
{
	public class PauseMenu : MonoBehaviour
	{
		public static PauseMenu instance;

		public GameObject PauseMenuScreen;

		public bool Open = false;

		public bool OverridingPause = false;

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
			PauseMenuScreen.SetActive(Open);
		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape) && !OverridingPause)
			{
				if (Open)
				{
					Time.timeScale = 1;
					GameManager.Instance.PlayerLockMovement = false;
					GameManager.Instance.PlayerLockLook = false;
					GameManager.Instance.PlayerMouseVisible = false;
				}


				Open = !Open;

			}

			if (PauseMenuScreen.activeSelf != Open) PauseMenuScreen.SetActive(Open);

			if (Open)
			{
				Time.timeScale = 0;
				GameManager.Instance.PlayerLockMovement = true;
				GameManager.Instance.PlayerLockLook = true;
				GameManager.Instance.PlayerMouseVisible = true;
			}
		}

		public void Resume()
		{
			Open = false;


			Time.timeScale = 1;
			GameManager.Instance.PlayerLockMovement = false;
			GameManager.Instance.PlayerLockLook = false;
			GameManager.Instance.PlayerMouseVisible = false;

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
