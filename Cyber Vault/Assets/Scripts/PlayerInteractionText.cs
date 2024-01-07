using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public partial class PlayerInteractionText : MonoBehaviour
{
	public static PlayerInteractionText instance;

	public TMP_Text DisplayUIText;
	//public TMP_Text subDisplayUIText;

	public int CurrentCount = 0;

	public float duration = 1f;


	List<DisplayText> _displayTexts = new List<DisplayText>();
	//List<DisplayText> subDisplayTexts = new List<DisplayText>();

	private float _currentDisplayTime = 0f;



	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = this;
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		// displayTexts.Add(new DisplayText("text", 2));
		// displayTexts.Add(new DisplayText("tesadasdasdxt", 1));
		// displayTexts.Add(new DisplayText("115252125125", 26));

		_displayTexts.Sort((a, b) => b.priority.CompareTo(a.priority));
		//subDisplayTexts.Sort((a, b) => a.priority.CompareTo(b.priority));


		// string temp = "";
		// foreach (DisplayText dt in displayTexts)
		// {
		//     temp += dt.text + " ";
		// }
		// print(temp);
	}

	// Update is called once per frame
	void Update()
	{
		CurrentCount = _displayTexts.Count;

		if (_displayTexts.Count > 0)
		{
			_currentDisplayTime += Time.deltaTime;


			if (_currentDisplayTime <= duration)
			{
				_displayTexts.Sort((a, b) => b.priority.CompareTo(a.priority));
				DisplayUIText.text = _displayTexts[0].text;
			}
			else
			{

				_displayTexts.RemoveAt(0);
				_displayTexts.Sort((a, b) => b.priority.CompareTo(a.priority));
				_currentDisplayTime = 0f;

				if (_displayTexts.Count > 1)
				{
					_displayTexts.Clear();
				}
			}

		}
		else
		{
			DisplayUIText.text = "";
			if (_currentDisplayTime != 0) _currentDisplayTime = 0;
		}


		// if (subDisplayTexts.Count > 0)
		// {
		//     currentDisplayTime += Time.deltaTime;

		//     if (currentDisplayTime <= 2)
		//     {

		//         subDisplayTexts.Sort((a, b) => a.priority.CompareTo(b.priority));
		//         subDisplayUIText.text = subDisplayTexts[0].text;
		//     }
		//     else
		//     {

		//         subDisplayTexts.RemoveAt(0);
		//         subDisplayTexts.Sort((a, b) => a.priority.CompareTo(b.priority));
		//         currentDisplayTime = 0f;
		//     }
		// }
		// else
		// {
		//     subDisplayUIText.text = "";
		//     if (currentDisplayTime != 0) currentDisplayTime = 0;
		// }
	}

#nullable enable
	public DisplayText? CreateNewHint(int priority, string display)
	{
		DisplayText dis = new(display, priority);

		if (!IsInTheList(dis))
		{
			AddInteractionText(dis);
			return dis;
		}
		else
		{
			return null;
		}
	}
#nullable restore

	public void AddInteractionText(DisplayText display)
	{
		if (!IsInTheList(display))
		{
			_displayTexts.Add(display);
		}
		else
		{
			if (DisplayUIText.text == display.text)
			{
				_currentDisplayTime = 0;

			}
		}
	}

	public void RemoveInteractionText(DisplayText display)
	{
		if (IsInTheList(display))
		{
			_displayTexts.Remove(display);
		}
	}

	public bool IsInTheList(DisplayText display)
	{
		if (_displayTexts.Contains(display))
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	// public void AddSubText(DisplayText display)
	// {
	//     subDisplayTexts.Add(display);
	// }

	// public void RemoveSubText(DisplayText display)
	// {
	//     subDisplayTexts.Remove(display);
	// }

	// public bool IsSubTextInTheList(DisplayText display)
	// {
	//     if (subDisplayTexts.Contains(display))
	//     {
	//         return true;
	//     }
	//     else
	//     {
	//         return false;
	//     }
	// }
}
