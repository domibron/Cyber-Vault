using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class DoorControlPanel : MonoBehaviour
	{
		public Door Door;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void Interact()
		{
			if (GameManager.Instance.BorkenKeyFixed && GameManager.Instance.HasBrokenKeyPartOne && GameManager.Instance.HasBrokenKeyPartTwo && GameManager.Instance.DoorUnlocked)
			{
				Door.OpenDoor();
			}
		}
	}
}
