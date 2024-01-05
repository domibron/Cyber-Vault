using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace CyberVault
{
	public class Timer : MonoBehaviour
	{
		public float StartTimeSeconds = 60f;

		public bool StartCountDown = false;

		private TMP_Text _displayText;

		public float _time = 0;

		[SerializeField] public UnityEvent TimerFinished;

		// Start is called before the first frame update
		void Start()
		{
			_displayText = GetComponent<TMP_Text>();

			_time = StartTimeSeconds;
		}

		public void StartTimer()
		{
			StartCountDown = true;
		}

		// Update is called once per frame
		void Update()
		{
			if (StartCountDown)
			{
				_time -= Time.deltaTime;

				// causes jittery ness but not too bad.
				float milisecs = _time % 1;

				milisecs *= 100;

				//print(_time.ToString("00") + ":" + milisecs.ToString("00"));
				_displayText.text = _time.ToString("00") + ":" + milisecs.ToString("00");

				if (_time <= 0)
				{
					TimerFinished.Invoke();
					StartCountDown = false;
				}
			}
		}
	}
}
