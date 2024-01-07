using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class FixKey : MonoBehaviour
	{
		public Material Bad;
		public Material Good;
		public Material Normal;

		public GameObject LightIndicator;

		public Objective Objective;

		// Start is called before the first frame update
		void Start()
		{
			//LightIndicator.GetComponent<Renderer>().material = Bad;
		}

		// Update is called once per frame
		void Update()
		{
			if (GameManager.Instance.HasBrokenKeyPartOne && GameManager.Instance.HasBrokenKeyPartTwo && GameManager.Instance.HasGlassCutter && !Objective.AddedTask)
			{
				Objective.AddTask();
			}
		}

		public void Interact()
		{
			if (GameManager.Instance.HasBrokenKeyPartOne && GameManager.Instance.HasBrokenKeyPartTwo && GameManager.Instance.HasGlassCutter)
			{
				StartCoroutine(ChangeMat(Good));
				GameManager.Instance.RepairKey();
				Objective.CompleateTask();
			}
			else
			{
				PlayerInteractionText.instance.CreateNewHint(5, "<color=red>Cant");
				StartCoroutine(ChangeMat(Bad));
			}
		}

		IEnumerator ChangeMat(Material mat)
		{
			LightIndicator.GetComponent<Renderer>().material = mat;

			yield return new WaitForSeconds(1f);

			LightIndicator.GetComponent<Renderer>().material = Normal;
		}
	}
}
