using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class DisableItemInteract : MonoBehaviour
	{
		private Outline _outline;
		private ItemInteract _itemInteract;
		private InteractionScript _interactionScript;

		// Start is called before the first frame update
		void Start()
		{
			_outline = GetComponent<Outline>();
			_itemInteract = GetComponent<ItemInteract>();
			_interactionScript = GameObject.FindWithTag("Player").GetComponent<InteractionScript>();
		}

		public void Disable()
		{

			_interactionScript.RemoveObject(_itemInteract.gameObject);

			if (_outline != null)
			{
				_outline.OutlineWidth = 0;
			}

			_itemInteract.enabled = false;

			//_itemInteract.gameObject.SetActive(false);
		}

		public void DisableAndDelete()
		{
			_interactionScript.RemoveObject(_itemInteract.gameObject);

			if (_outline != null)
			{
				_outline.OutlineWidth = 0;
			}

			_itemInteract.enabled = false;

			_itemInteract.gameObject.SetActive(false);
		}
	}
}
