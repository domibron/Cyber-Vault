using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CyberVault
{
	public class HoverDisplay : MonoBehaviour, IHintBox
	{
		public string HintText = "";
		public int priority = 0;

		public UnityEvent OnHover;

		private DisplayText _displayText;

		// Start is called before the first frame update
		void Start()
		{
			_displayText = new(HintText, priority);
		}

		// Update is called once per frame
		void Update()
		{

		}

		void IHintBox.OnHover()
		{
			PlayerInteractionText.instance.AddInteractionText(_displayText);
			OnHover.Invoke();
		}
	}
}
