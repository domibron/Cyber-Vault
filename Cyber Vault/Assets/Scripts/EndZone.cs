using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class EndZone : MonoBehaviour
	{
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		void OnTriggerEnter(Collider other)
		{
			if (other.tag == "player" || other.gameObject.layer == 3)
			{
				if (GameManager.Instance.HasDataShard)
				{
					GameManager.Instance.Won = true;
					GameManager.Instance.EndGame(true);
				}
				else
				{
					// GameManager.Instance.EndGame(false);
				}
			}
		}
	}
}
