using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CyberVault
{


	public class HackScreen : MonoBehaviour
	{
		enum HackPuzzleItems
		{
			Empty = 0,
			Star = 1,
			Wall = 2,
			Start = 3,
			End = 4,
			Player = 5
		}

		enum HackType
		{
			None = 0,
			Door = 1,
			Lasers = 2,
			Turrets = 3
		}

		public Sprite StarSprite;
		public Image Box;

		public GameObject FaliureScreen;
		public CanvasGroup FaliureCanvasGroup;
		public GameObject SuccessScreen;
		public CanvasGroup SuccessCanvasGroup;

		public float Spacing = 5;

		public const int XAmmount = 8;
		public const int YAmmount = 8;

		public RectTransform PanelRectTransform;

		private Image[,] _hackScreenArrayImages = new Image[XAmmount, YAmmount];
		private int[,] _hackScreenArrayInt = new int[XAmmount, YAmmount];

		private Vector2 _boxSize;
		private Vector3 _holderOffet;

		private float _width;
		private float _height;

		// what the size needs to be.
		private float _boxWidth;
		private float _boxHeight;

		private int DoorTaskID = -1;
		private int LaserTaskID = -1;
		private int TurretTaskID = -1;

		// player
		private Vector2Int _currentPos = new Vector2Int();
		private Vector2Int _endPos = new Vector2Int();
		// private Vector2Int _moveDir = new();
		private int _touching = 0;
		private bool _inPuzzle = false;
		private bool _firstTimeSetup = true;
		private List<Vector2Int> _starLocations = new();

		private bool _failure = false;
		private bool _allStarsCollected = false;
		private bool _successful = false;

		private HackType _hackType = HackType.None;

		void Awake()
		{
			DoorTaskID = GameManager.Instance.AddTask("Hack the door");
			LaserTaskID = GameManager.Instance.AddTask("Hack the lasers");
			TurretTaskID = GameManager.Instance.AddTask("Hack the turrets");
		}

		// Start is called before the first frame update
		void Start()
		{
			_holderOffet = PanelRectTransform.position;

			_width = PanelRectTransform.sizeDelta.x;
			_height = PanelRectTransform.sizeDelta.y;

			_boxWidth = Box.rectTransform.sizeDelta.x;
			_boxHeight = Box.rectTransform.sizeDelta.y;

			float totalWidthNoSpacing = _width - (Spacing * XAmmount);
			float totalHeighthNoSpacing = _height - (Spacing * YAmmount);

			_boxWidth = totalWidthNoSpacing / XAmmount;
			_boxHeight = totalHeighthNoSpacing / YAmmount;

			_boxSize = new Vector2(_boxWidth, _boxHeight);

			for (int y = 0; y < YAmmount; y++)
			{
				for (int x = 0; x < XAmmount; x++)
				{
					Image _temp = Instantiate(Box, PanelRectTransform.transform);

					_temp.rectTransform.sizeDelta = _boxSize;
					_temp.rectTransform.position = new Vector3(_holderOffet.x + (_boxWidth + Spacing) * x, _holderOffet.y + (_boxHeight + Spacing) * y, 0);

					_temp.name = _temp.name + $" ({(YAmmount - 1) - y}, {x})";

					_hackScreenArrayImages[(YAmmount - 1) - y, x] = _temp;
				}
			}


		}

		// Update is called once per frame
		void Update()
		{
			// print(StringArray(_hackScreenArrayInt));

			if (_inPuzzle && !_failure && !_successful)
			{



				Computer.Instance.OverridingEscape = true;

				DrawHack(_hackScreenArrayInt);

				if (_firstTimeSetup) _firstTimeSetup = !_firstTimeSetup;

				//print(_currentPos);

				// a chain to stop strange bug of occorring.

				_touching = 0;

				int possibleMoveCount = 4;

				if (_currentPos.x - 1 < 0 || _hackScreenArrayInt[_currentPos.x - 1, _currentPos.y] == 5 || _hackScreenArrayInt[_currentPos.x - 1, _currentPos.y] == 2)
				{
					possibleMoveCount -= 1;
				}

				if (_currentPos.y - 1 < 0 || _hackScreenArrayInt[_currentPos.x, _currentPos.y - 1] == 5 || _hackScreenArrayInt[_currentPos.x, _currentPos.y - 1] == 2)
				{
					possibleMoveCount -= 1;
				}

				if (_currentPos.x + 1 >= XAmmount || _hackScreenArrayInt[_currentPos.x + 1, _currentPos.y] == 5 || _hackScreenArrayInt[_currentPos.x + 1, _currentPos.y] == 2)
				{
					possibleMoveCount -= 1;
				}

				if (_currentPos.y + 1 >= YAmmount || _hackScreenArrayInt[_currentPos.x, _currentPos.y + 1] == 5 || _hackScreenArrayInt[_currentPos.x, _currentPos.y + 1] == 2)
				{
					possibleMoveCount -= 1;
				}

				if (possibleMoveCount <= 0)
				{
					print("no more moves");
					StartCoroutine(Failed());
				}

				if (Input.GetKeyDown(KeyCode.W))
				{
					if (_currentPos.x - 1 < 0) return;

					if (_hackScreenArrayInt[_currentPos.x - 1, _currentPos.y] == 5 || _hackScreenArrayInt[_currentPos.x - 1, _currentPos.y] == 2)
					{
						return;
					}

					_currentPos += Vector2Int.left;

				}
				else if (Input.GetKeyDown(KeyCode.A))
				{

					if (_currentPos.y - 1 < 0) return;

					if (_hackScreenArrayInt[_currentPos.x, _currentPos.y - 1] == 5 || _hackScreenArrayInt[_currentPos.x, _currentPos.y - 1] == 2)
					{
						return;
					}
					_currentPos += Vector2Int.down;

				}
				else if (Input.GetKeyDown(KeyCode.S))
				{

					if (_currentPos.x + 1 >= XAmmount) return;

					if (_hackScreenArrayInt[_currentPos.x + 1, _currentPos.y] == 5 || _hackScreenArrayInt[_currentPos.x + 1, _currentPos.y] == 2)
					{
						return;
					}

					_currentPos += Vector2Int.right;

				}
				else if (Input.GetKeyDown(KeyCode.D))
				{

					if (_currentPos.y + 1 >= YAmmount) return;

					if (_hackScreenArrayInt[_currentPos.x, _currentPos.y + 1] == 5 || _hackScreenArrayInt[_currentPos.x, _currentPos.y + 1] == 2)
					{
						return;
					}

					_currentPos += Vector2Int.up;

				}

				if (_hackScreenArrayInt[_currentPos.x, _currentPos.y] != (int)HackPuzzleItems.Player)
				{
					_hackScreenArrayInt[_currentPos.x, _currentPos.y] = (int)HackPuzzleItems.Player;
				}


				if (_currentPos.x - 1 >= 0)
				{
					if (_hackScreenArrayInt[_currentPos.x - 1, _currentPos.y] == 5)
					{

						_touching++;

						//ResetPuzzle();

						//Computer.Instance.OpenScreen(0);
					}
				}

				if (_currentPos.y - 1 >= 0)
				{
					if (_hackScreenArrayInt[_currentPos.x, _currentPos.y - 1] == 5)
					{
						_touching++;

						//ResetPuzzle();

						//Computer.Instance.OpenScreen(0);
					}
				}

				if (_currentPos.x + 1 < XAmmount)
				{
					if (_hackScreenArrayInt[_currentPos.x + 1, _currentPos.y] == 5)
					{
						_touching++;

						//ResetPuzzle();

						//Computer.Instance.OpenScreen(0);
					}
				}

				if (_currentPos.y + 1 < YAmmount)
				{
					if (_hackScreenArrayInt[_currentPos.x, _currentPos.y + 1] == 5)
					{
						_touching++;

						//ResetPuzzle();

						//Computer.Instance.OpenScreen(0);
					}
				}

				if (_touching > 1)
				{
					StartCoroutine(Failed());
				}

				_allStarsCollected = true;
				foreach (Vector2Int star in _starLocations.ToList<Vector2Int>())
				{

					// print(_hackScreenArrayInt[star.y, star.x]);


					if (_hackScreenArrayInt[star.y, star.x] != 5)
					{
						_allStarsCollected = false;
					}


				}


				if (_currentPos == _endPos && _allStarsCollected && !_failure)
				{
					// game end
					// display
					// close
					StartCoroutine(Successful());
				}
				else if (_currentPos == _endPos && !_allStarsCollected && !_failure)
				{
					StartCoroutine(Failed());
				}

				if (Input.GetKeyDown(KeyCode.Escape))
				{
					ResetPuzzle();

					Computer.Instance.OpenScreen(0);
				}
			}
		}

		private IEnumerator Failed()
		{
			_failure = true;

			DrawHack(_hackScreenArrayInt);

			float localTime = 0f;
			float targTime = localTime + 5f;

			FaliureScreen.SetActive(true);

			bool skip = false;

			while (localTime < targTime && !skip)
			{
				localTime += Time.deltaTime;

				FaliureCanvasGroup.alpha = Mathf.Abs(Mathf.Sin(localTime * 3f));

				if (Input.GetKey(KeyCode.Escape)) skip = true;

				yield return new();
			}

			yield return new();

			ResetPuzzle();

			Computer.Instance.OpenScreen(0);
		}

		private IEnumerator Successful()
		{
			_successful = true;

			DrawHack(_hackScreenArrayInt);

			float localTime = 0f;
			float targTime = localTime + 5f;

			SuccessScreen.SetActive(true);

			if (_hackType == HackType.Door)
			{
				GameManager.Instance.DoorUnlocked = true;
				GameManager.Instance.RemoveTask(DoorTaskID);
			}
			else if (_hackType == HackType.Lasers)
			{
				GameManager.Instance.LasersOnline = false;
				GameManager.Instance.RemoveTask(LaserTaskID);
			}
			else if (_hackType == HackType.Turrets)
			{
				GameManager.Instance.TurretsOnline = false;
				GameManager.Instance.RemoveTask(TurretTaskID);
			}

			bool skip = false;

			while (localTime < targTime && !skip)
			{
				localTime += Time.deltaTime;

				SuccessCanvasGroup.alpha = Mathf.Abs(Mathf.Sin(localTime * 3f));

				if (Input.GetKey(KeyCode.Escape)) skip = true;

				yield return new();
			}


			yield return new();

			ResetPuzzle();

			Computer.Instance.OpenScreen(0);
		}

		private void ResetPuzzle()
		{
			FaliureCanvasGroup.alpha = 0;
			FaliureScreen.SetActive(false);

			SuccessCanvasGroup.alpha = 0;
			SuccessScreen.SetActive(false);

			_hackScreenArrayInt = new int[XAmmount, YAmmount];

			foreach (Image image in _hackScreenArrayImages)
			{
				image.color = Color.white;
				image.sprite = null;
			}

			_currentPos = new Vector2Int(0, 0);

			_endPos = new Vector2Int(XAmmount - 1, YAmmount - 1);

			DrawHack(HackPuzzleLayouts.Base);

			_hackType = HackType.None;


			_starLocations.Clear();

			_firstTimeSetup = true;

			_inPuzzle = false;

			_failure = false;

			_successful = false;

			Computer.Instance.OverridingEscape = false;
		}


		public void HackDoor()
		{
			StartPuzle(HackPuzzleLayouts.Easy1, HackType.Door);
		}

		public void HackLasers()
		{
			StartPuzle(HackPuzzleLayouts.Easy2, HackType.Lasers);
		}

		public void HackTurrets()
		{
			StartPuzle(HackPuzzleLayouts.Easy3, HackType.Turrets);
		}


		private void StartPuzle(int[,] array, HackType hackType)
		{
			StopAllCoroutines();

			ResetPuzzle();

			CopyArray(array, ref _hackScreenArrayInt);

			_starLocations.Clear();

			DrawHack(array, true);

			_hackType = hackType;

			_inPuzzle = true;

			_firstTimeSetup = true;

			Computer.Instance.OpenScreen(1);
		}




		private HackPuzzleItems? ConvertIntIntoPuzzlePeice(int i)
		{
			HackPuzzleItems[] _array = (HackPuzzleItems[])Enum.GetValues(typeof(HackPuzzleItems));

			for (int x = 0; x < _array.Length; x++)
			{
				if (i == x) return _array[x];
			}

			return null;
		}

		// private int? ConvertPuzzlePeiceIntoInt(HackPuzzleItems item)
		// {
		// 	HackPuzzleItems[] _array = (HackPuzzleItems[])Enum.GetValues(typeof(HackPuzzleItems));

		// 	for (int x = 0; x < _array.Length; x++)
		// 	{
		// 		if (item == _array[x]) return x;
		// 	}

		// 	return null;
		// }

		private int? ConvertPuzzlePeiceIntoInt(HackPuzzleItems item)
		{
			return (int)item;
		}

		private void CopyArray(int[,] arrayData, ref int[,] array)
		{
			for (int y = 0; y < YAmmount; y++)
			{
				for (int x = 0; x < XAmmount; x++)
				{
					array[y, x] = arrayData[y, x];
				}
			}
		}

		public void DrawHack(int[,] array, bool setVars = false)
		{
			for (int y = 0; y < YAmmount; y++)
			{
				for (int x = 0; x < XAmmount; x++)
				{
					if (array[x, y] == (int)HackPuzzleItems.Empty)
					{
						// empt colour
						_hackScreenArrayImages[x, y].sprite = null;
						_hackScreenArrayImages[x, y].color = Color.white;
						// if (_hackScreenArrayInt[x, y] != array[x, y])
						// {
						// 	_hackScreenArrayInt[x, y] = array[x, y];
						// }
					}
					else if (array[x, y] == (int)HackPuzzleItems.Star)
					{
						_hackScreenArrayImages[x, y].sprite = StarSprite;
						_hackScreenArrayImages[x, y].color = Color.white;



						// fipped because easier.
						if (setVars)
						{
							_starLocations.Add(new Vector2Int(y, x));
						}

						// if (_hackScreenArrayInt[x, y] != array[x, y])
						// {
						// 	_hackScreenArrayInt[x, y] = array[x, y];
						// }
					}
					else if (array[x, y] == (int)HackPuzzleItems.Wall)
					{
						_hackScreenArrayImages[x, y].sprite = null;
						_hackScreenArrayImages[x, y].color = Color.gray;
						// if (_hackScreenArrayInt[x, y] != array[x, y])
						// {
						// 	_hackScreenArrayInt[x, y] = array[x, y];
						// }
					}
					else if (array[x, y] == (int)HackPuzzleItems.Start)
					{
						_hackScreenArrayImages[x, y].sprite = null;
						_hackScreenArrayImages[x, y].color = Color.yellow;
						// if (_hackScreenArrayInt[x, y] != array[x, y])
						// {
						// 	_hackScreenArrayInt[x, y] = array[x, y];
						// }

						if (setVars)
						{
							_currentPos = new Vector2Int(x, y);
						}
					}
					else if (array[x, y] == (int)HackPuzzleItems.End)
					{
						_hackScreenArrayImages[x, y].sprite = null;
						_hackScreenArrayImages[x, y].color = Color.blue;
						// if (_hackScreenArrayInt[x, y] != array[x, y])
						// {
						// 	_hackScreenArrayInt[x, y] = array[x, y];
						// }

						if (setVars)
						{
							_endPos = new Vector2Int(x, y);
						}
					}
					else if (array[x, y] == (int)HackPuzzleItems.Player)
					{
						// if game over red otherwise green
						//_hackScreenArrayImages[x, y].sprite = null;

						// print($"{_currentPos} {x} {y}");


						if (_currentPos.x == x && _currentPos.y == y && !_failure && !_successful)
						{
							_hackScreenArrayImages[x, y].color = new Color32(160, 100, 0, 255);
						}
						else if (!_failure && !_successful)
						{
							_hackScreenArrayImages[x, y].color = new Color32(255, 150, 0, 255);
						}
						else if (_currentPos.x == x && _currentPos.y == y && _failure && !_successful)
						{
							_hackScreenArrayImages[x, y].color = new Color(0.5f, 0, 0, 1);
						}
						else if (_failure && !_successful)
						{
							_hackScreenArrayImages[x, y].color = Color.red;
						}
						else if (_currentPos.x == x && _currentPos.y == y && !_failure && _successful)
						{
							_hackScreenArrayImages[x, y].color = new Color32(0, 150, 0, 255);
						}
						else if (!_failure && _successful)
						{
							_hackScreenArrayImages[x, y].color = Color.green;
						}
						// if (_hackScreenArrayInt[x, y] != array[x, y])
						// {
						// 	_hackScreenArrayInt[x, y] = array[x, y];
						// }
					}
					else
					{
						Debug.LogError($"I cannot find a def for {array[x, y]}");
					}
				}
			}

		}

		private string StringArray(int[,] array)
		{
			string str = "";

			for (int y = 0; y < array.GetLength(1); y++)
			{
				for (int x = 0; x < array.GetLength(0); x++)
				{
					str += array[y, x].ToString();
				}
				str += "\n";
			}

			return str;
		}
	}

	public class HackPuzzleLayouts
	{
		public static int[,] Easy1 = new int[8, 8]
		{
			{0,0,0,0,0,3,0,0},
			{0,1,2,0,2,0,2,1},
			{0,2,2,0,2,0,2,0},
			{0,0,0,0,0,1,2,0},
			{0,0,1,0,2,0,2,0},
			{0,0,0,0,2,0,2,0},
			{0,0,0,0,2,0,2,1},
			{0,0,4,0,0,0,0,0}
		};

		public static int[,] Easy2 = new int[8, 8]
		{
			{1,0,0,0,2,3,0,0},
			{0,0,2,1,0,0,0,0},
			{0,0,0,2,2,2,0,0},
			{2,2,0,2,0,1,0,0},
			{2,2,0,0,0,2,2,0},
			{0,0,0,2,0,1,0,0},
			{0,0,0,2,0,0,2,0},
			{4,2,0,1,0,0,0,0}
		};

		public static int[,] Easy3 = new int[8, 8]
		{
			{3,0,0,0,0,1,0,0},
			{2,2,0,2,2,2,2,0},
			{0,0,0,0,1,0,0,0},
			{0,2,2,2,0,0,0,0},
			{0,0,0,1,0,0,0,0},
			{0,2,0,0,2,0,2,1},
			{0,0,0,0,0,1,2,0},
			{4,2,0,0,2,0,0,0}
		};

		public static int[,] Base = new int[8, 8]
		{
			{0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0}
		};
	}
}
