using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CyberVault
{
	public class ItemInteract : MonoBehaviour
	{
		[SerializeField] public UnityEvent OnInteract;

		public void OnInteraction()
		{
			OnInteract.Invoke();
		}

		void Awake()
		{
			gameObject.tag = "Interactable";
		}

		void Start()
		{
			Outline outline = GetComponent<Outline>();
			if (outline != null) outline.OutlineWidth = 0;
		}

	}
}