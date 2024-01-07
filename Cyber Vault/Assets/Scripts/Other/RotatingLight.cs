using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CyberVault
{
	public class RotatingLight : MonoBehaviour
	{
		public List<Light> lights;

		public bool Active = false;

		public float Speed = 1f;
		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			foreach (var light in lights.ToList<Light>())
			{
				light.enabled = Active;
			}

			if (Active)
			{
				transform.Rotate(Vector3.forward * Speed * 100 * Time.deltaTime);

			}
		}

		public void LightEnabled(bool enabled)
		{
			Active = enabled;
		}
	}
}
