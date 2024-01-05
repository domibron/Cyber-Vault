using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CyberVault
{
	public class MainMenu : MonoBehaviour
	{
		public List<GameObject> UIElements;

		// Start is called before the first frame update
		void Start()
		{
			OpenUI(0);
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

		}

		public void Quit()
		{
			Application.Quit();
		}
	}
}
