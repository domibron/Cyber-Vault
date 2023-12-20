using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CyberVault
{
	public class InteractionScript : MonoBehaviour
	{
		[SerializeField] private float _threshold = 0.9f;
		[SerializeField] public float _maxDistance = 5f;

		private Transform _selectedObject;

		private List<GameObject> _interactables = new List<GameObject>();

		private bool avalibleObjects = false;

		private Camera _camera;

		// Start is called before the first frame update
		void Awake()
		{
			GameObject[] temp__ = GameObject.FindGameObjectsWithTag("Interactable");

			if (temp__.Length <= 0) return;
			else avalibleObjects = true;

			for (int i = 0; i < temp__.Length; i++)
			{
				if (temp__[i].GetComponent<ItemInteract>() == null) continue;

				_interactables.Add(temp__[i]);
			}


			_camera = Camera.main;
		}

		void Check(Ray ray)
		{
			_selectedObject = null;

			var closest = 0f;
			var distance = float.MaxValue;

			for (int i = 0; i < _interactables.Count; i++)
			{
				if (_interactables[i] == null) _interactables.RemoveAt(i);

				var vector1 = ray.direction;
				var vector2 = _interactables[i].transform.position - ray.origin;

				var lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);
				var distanceCalc = Vector3.Distance(_interactables[i].transform.position, transform.position);

				// selectables[i].lookPercentage = lookPercentage;

				if (lookPercentage > _threshold && lookPercentage > closest && distanceCalc < distance && distanceCalc < _maxDistance)
				{
					closest = lookPercentage;
					distance = distanceCalc;
					_selectedObject = _interactables[i].transform;
				}

			}

		}

		// Update is called once per frame
		void Update()
		{
			if (avalibleObjects) Check(_camera.ScreenPointToRay(Input.mousePosition));

			if (_selectedObject != null)
			{
				if (Input.GetKeyDown(KeyCode.E))
				{
					_selectedObject.GetComponent<ItemInteract>().OnInteraction();
				}
			}
		}
	}
}
