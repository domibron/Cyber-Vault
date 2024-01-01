using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class Door : MonoBehaviour
	{
		public Animator Animator;

		//public bool Open = false;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			// if (GameManager.Instance.DoorUnlocked && Open)
			// {
			// 	Animator.SetBool("open", true);
			// }
			// else
			// {
			// 	Animator.SetBool("open", false);
			// }
		}

		public void OpenDoor()
		{
			if (GameManager.Instance.DoorUnlocked)
			{
				Animator.SetBool("open", true);
			}
			else
			{
				Animator.SetBool("open", false);
			}
		}
	}
}
