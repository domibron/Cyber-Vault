using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CyberVault
{
	public class MouseLockManager : MonoBehaviour
	{
		public static MouseLockManager Instance;

		private bool _isLocked = true;

		public bool IsLocked
		{
			get
			{
				return _isLocked;
			}
			set
			{
				_isLocked = value;
			}
		}

		// Start is called before the first frame update
		void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this.gameObject);
			}
			else
			{
				Instance = this;
			}
		}

		void Start()
		{
			Instance.IsLocked = true;
		}

		// Update is called once per frame
		void Update()
		{
			if (_isLocked)
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
			else
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}
	}
}
