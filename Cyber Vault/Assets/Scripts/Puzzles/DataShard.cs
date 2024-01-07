using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class DataShard : MonoBehaviour
	{
		public DisableItemInteract DisableItemInteract;

		public Objective Objective;


		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void AddTask()
		{
			Objective.AddTask();
		}

		public void PickUpDataShard()
		{
			if (GameManager.Instance.RemovedGlass)
			{
				GameManager.Instance.HasDataShard = true;
				Objective.CompleateTask();
				GameManager.Instance.AddTask("ESCAPE!");
				DisableItemInteract.DisableAndDelete();
			}
			else
			{
				PlayerInteractionText.instance.CreateNewHint(5, "<color=red>Cant");
			}
		}
	}
}
