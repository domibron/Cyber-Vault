using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CyberVault
{
	public class MainMenu : MonoBehaviour
	{
		public List<GameObject> UIElements;

		public TMP_Text TimerText;

		public TMP_Dropdown WindowModeDropDown;

		// Start is called before the first frame update
		void Start()
		{
			OpenUI(0);

			TimerText.text = "Best Time:\n" + PlayerPrefs.GetString("bts", "00:00.00");

			ChangeWindow(PlayerPrefs.GetInt("window", 0));
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void OpenUI(int index)
		{
			for (int i = 0; i < UIElements.Count; i++)
			{
				if (i == index)
				{
					UIElements[index].SetActive(true);
				}
				else
				{
					UIElements[i].SetActive(false);
				}
			}
		}

		public void StartGame()
		{
			Debug.Log("loading Game");
			SceneManager.LoadScene(1);
		}

		public void OpenSettings()
		{
			UIElements[1].SetActive(true);
		}

		public void CloseSettings()
		{
			UIElements[1].SetActive(false);
		}


		public void ClearData()
		{
			PlayerPrefs.DeleteAll();
			ChangeWindow(PlayerPrefs.GetInt("window", 0));

		}

		public void ChangeWindow()
		{
			if (WindowModeDropDown.value == 0)
			{
				Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
				PlayerPrefs.SetInt("window", 0);
			}
			else if (WindowModeDropDown.value == 1)
			{
				Screen.fullScreenMode = FullScreenMode.Windowed;
				PlayerPrefs.SetInt("window", 1);
			}
			else if (WindowModeDropDown.value == 2)
			{
				Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
				PlayerPrefs.SetInt("window", 2);
			}


			PlayerPrefs.Save();
		}

		public void ChangeWindow(int i)
		{
			if (i == 0)
			{
				Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;

			}
			else if (i == 1)
			{
				Screen.fullScreenMode = FullScreenMode.Windowed;
			}
			else if (i == 2)
			{
				Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
			}

			WindowModeDropDown.value = i;

			PlayerPrefs.SetInt("window", i);
			PlayerPrefs.Save();
		}

		public void Quit()
		{
			Application.Quit();
		}
	}
}
