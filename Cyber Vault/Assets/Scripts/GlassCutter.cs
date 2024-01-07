using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class GlassCutter : MonoBehaviour
	{
		public Transform Player;
		public Transform Camera;

		public GameObject glassCutter;

		public bool inMiniGame = false;

		public DisableItemInteract DisableItemInteract;

		public Objective Objective;

		public DataShard DataShard;

		private float _rotationSpeed = 0;

		[SerializeField]
		private float _cutPercent = 0;
		// Start is called before the first frame update
		void Start()
		{
			glassCutter.SetActive(false);
		}

		void Update()
		{
			if (inMiniGame)
			{
				if (Input.GetKey(KeyCode.Mouse0))
				{
					PlayerInteractionText.instance.CreateNewHint(10, $"{_cutPercent.ToString("00.00")}%");
				}
				else
				{
					PlayerInteractionText.instance.CreateNewHint(10, $"{_cutPercent.ToString("00.00")}%\nKeep holding left mouse button to cut the glass");
				}
			}
		}

		// Update is called once per frame
		void FixedUpdate()
		{
			if (inMiniGame)
			{
				if (Input.GetKey(KeyCode.Mouse0))
				{
					_rotationSpeed += 10;
				}
				else
				{
					_rotationSpeed -= 10;
				}

				float maxSpeed = 1000;
				_rotationSpeed = Mathf.Clamp(_rotationSpeed, 0, maxSpeed);

				if (_rotationSpeed > 0) _cutPercent += _rotationSpeed / (maxSpeed * 20);
				print(_rotationSpeed / maxSpeed * 10);

				glassCutter.transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);

				if (_cutPercent >= 100)
				{
					FinishMiniGame();
				}
			}
		}

		public void StartTask()
		{
			Objective.AddTask();
		}

		public void FinishMiniGame()
		{
			if (inMiniGame)
			{
				print("done");
				glassCutter.SetActive(false);

				Objective.CompleateTask();
				DataShard.AddTask();

				GameManager.Instance.PlayerLockLook = false;
				GameManager.Instance.PlayerLockMovement = false;

				inMiniGame = false;


				GameManager.Instance.RemovedGlass = true;

				PauseMenu.instance.OverridingPause = false;



				DisableItemInteract.Disable();
				gameObject.SetActive(false);
			}
		}

		public void GlassCutterStart()
		{
			if (GameManager.Instance.HasGlassCutter)
			{
				glassCutter.SetActive(true);

				GameManager.Instance.PlayerLockLook = true;
				GameManager.Instance.PlayerLockMovement = true;

				inMiniGame = true;

				Player.localPosition = new Vector3(2, 2, -12);
				Camera.localRotation = Quaternion.Euler(350, 0, 0);


				PauseMenu.instance.OverridingPause = true;
			}
		}
	}
}
