using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class BasicLookScript : MonoBehaviour
	{
		public Transform CameraHolder;

		public float Sensitivity = 1f;



		private Transform _playerBody;
		private Transform _camera;

		private float _yRotation;

		// Start is called before the first frame update
		void Start()
		{
			// yes this can work with child in child objects.
			_camera = GetComponentInChildren<Camera>().transform;
			_playerBody = GetComponent<Transform>();
		}

		// Update is called once per frame
		void Update()
		{
			Vector2 lookDirection = new();

			lookDirection.x = Input.GetAxisRaw("Mouse X");
			lookDirection.y = Input.GetAxisRaw("Mouse Y");

			_yRotation -= lookDirection.y;
			_yRotation = Mathf.Clamp(_yRotation, -90f, 90f);

			// could put sensitivity here, like ved2D * sens

			_playerBody.Rotate(Vector3.up * lookDirection.x * Sensitivity);

			CameraHolder.localRotation = Quaternion.Euler(_yRotation * Sensitivity, 0, 0);


		}
	}
}
