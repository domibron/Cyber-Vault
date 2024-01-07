using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class Objective : MonoBehaviour
	{
		public string Task = "";

		public int TaskID = -1;

		public bool OverideAssingTask = false;

		public bool AddedTask = false;

		// Start is called before the first frame update
		void Start()
		{
			if (!OverideAssingTask)
			{
				TaskID = GameManager.Instance.AddTask(Task);
				AddedTask = true;
			}
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void AddTask()
		{
			TaskID = GameManager.Instance.AddTask(Task);
			AddedTask = true;
		}

		public void CompleateTask()
		{
			GameManager.Instance.RemoveTask(TaskID);
		}
	}
}
