using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CyberVault
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;


		[Header("Variables")]
		public Transform Player;

		public TMP_Text TimerText;
		public TMP_Text TaskBox;
		public TMP_Text HintBox;

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

		public bool RemovedGlass = false;

		public bool HasDataShard = false;

		public bool Dead = false;
		public bool Won = false;

		public float Timer = 0;

		public string TaskBoxString = "";
		// int is the ID and the string is the tasks.
		public Dictionary<int, string> Tasks = new Dictionary<int, string>();
		public int TaskCounter { get; private set; } = 0;

		public string HintBoxString = "";

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

				AddTask("TASKS:");
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

			if (!Dead && !Won)
			{
				Timer += Time.deltaTime;
			}

			TimerText.text = Mathf.Floor(Timer / 60).ToString("00:") + (Timer % 60f).ToString("00.00");

			TaskBoxString = "";
			foreach (var item in Tasks)
			{
				TaskBoxString += "* - " + item.Value + "\n";
			}

			TaskBox.text = TaskBoxString;
			HintBox.text = HintBoxString;
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
			if (HasBrokenKeyPartOne && HasBrokenKeyPartTwo && HasGlassCutter)
			{
				BorkenKeyFixed = true;
			}
			else
			{
				// we want to let player know they need the parts,
				// but not here.
			}
		}

		public void CollectGlassCutter()
		{
			HasGlassCutter = true;
		}

		public void FixKey()
		{
			BorkenKeyFixed = true;
		}


		public void EndGame(bool isWin)
		{
			EndGameState.instance.EndGame(isWin);
		}



		// ===================== For the task display

		public int AddTask(string text)
		{
			print("Running");
			foreach (var item in Tasks)
			{
				if (item.Value == text)
				{
					print($"{item.Value} == {text}");
					return item.Key;
				}
			}

			TaskCounter++;
			Tasks.Add(TaskCounter, text);
			return TaskCounter;

		}

		public void RemoveTask(string text)
		{
			foreach (var item in Tasks)
			{
				if (item.Value == text)
				{
					Tasks.Remove(item.Key);
				}
			}
		}

		public void RemoveTask(int ID)
		{
			Tasks.Remove(ID);
		}
	}


}
