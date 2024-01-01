using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;


		[Header("Variables")]
		public Transform Player;

		private BasicMovement _movement;
		private BasicLookScript _look;

		[Header("Game State")]
		public bool HasGlassCutter = false;

		public bool LasersOnline = true;

		public bool TurretsOnline = true;

		public bool DoorUnlocked = false;

		public bool HasBrokenKeyPartOne = false;
		public bool HasBrokenKeyPartTwo = false;

		public bool BorkenKeyFixed = false;



		[Header("Player State")]
		public bool PlayerLockMovement = false;
		public bool PlayerLockLook = false;
		public bool PlayerMouseVisible = false;

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
			_movement = Player.GetComponent<BasicMovement>();
			_look = Player.GetComponent<BasicLookScript>();
		}

		void Update()
		{
			MouseLockManager.Instance.IsLocked = !PlayerMouseVisible;

			_movement.Locked = PlayerLockMovement;
			_look.Locked = PlayerLockLook;
		}

		public void LookLockToggle()
		{
			PlayerLockLook = !PlayerLockLook;
		}

		public void LockMovementTogle()
		{
			PlayerLockMovement = !PlayerLockMovement;
		}

		public void PlayerMouseToggle()
		{
			PlayerMouseVisible = !PlayerMouseVisible;
		}

		public void ComplpeateKey1()
		{
			HasBrokenKeyPartOne = true;
		}

		public void ComplpeateKey2()
		{
			HasBrokenKeyPartTwo = true;
		}

		public void RepairKey()
		{
			BorkenKeyFixed = true;
		}

		public void CollectGlassCutter()
		{
			HasGlassCutter = true;
		}
	}


}
