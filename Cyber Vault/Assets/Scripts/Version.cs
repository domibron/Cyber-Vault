using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CyberVault
{
	public class Version : MonoBehaviour
	{
		public TMP_Text text;

		// Start is called before the first frame update
		void Start()
		{
			if (text == null) text = GetComponent<TMP_Text>();
			text.text = "Version:\n" + Application.version;
		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}
