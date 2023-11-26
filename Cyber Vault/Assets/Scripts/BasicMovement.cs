using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class BasicMovement : MonoBehaviour
	{
		public float Speed = 5f;

		public float Gravity = -9.81f;

		public float JumpHeight = 1.4f;


		private CharacterController _characterContoller;

		private Vector3 velocity;

		private bool isGrounded = false;

		// Start is called before the first frame update
		void Start()
		{
			_characterContoller = GetComponent<CharacterController>();
		}

		// Update is called once per frame
		void Update()
		{
			HandleMovement();
			HandleGravity();
			HandleJumping();
			HandleGroundCheck();
		}

		private void HandleMovement()
		{
			Vector3 moveDirection = new();

			moveDirection.x = Input.GetAxisRaw("Horizontal");
			moveDirection.z = Input.GetAxisRaw("Vertical");

			moveDirection = transform.right * moveDirection.x + transform.forward * moveDirection.z;

			_characterContoller.Move(moveDirection * Time.deltaTime * Speed);
		}

		private void HandleGravity()
		{
			if (isGrounded && velocity.y < 0)
			{
				velocity.y = -2f;
			}
			else
			{
				velocity.y += Gravity * Time.deltaTime;
			}

			_characterContoller.Move(velocity * Time.deltaTime);
		}

		private void HandleJumping()
		{
			if (Input.GetKey(KeyCode.Space))
			{
				if (isGrounded)
				{
					velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
				}
			}
		}

		private void HandleGroundCheck()
		{

			if (_characterContoller.isGrounded && Physics.Raycast(transform.position, -transform.up, 1.1f))
			{
				isGrounded = true;
			}
			else
			{
				isGrounded = false;
			}

		}
	}
}
