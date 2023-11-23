using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class BasicMovement : MonoBehaviour
	{
		private CharacterController _characterContoller;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			float x = Input.GetAxisRaw("Horizontal");
			float z = Input.GetAxisRaw("Vertical");


        }
	}
}
