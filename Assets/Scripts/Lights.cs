using System.Collections.Generic;
using UnityEngine;

namespace Airport
{
	/// <summary>
	/// The lights controller of the plane.
	/// </summary>
	public class Lights : MonoBehaviour
	{
		/// <summary>
		/// List of all added light behaviours to this component.
		/// </summary>
		[SerializeField]
		private List<Behaviour> behaviours = new List<Behaviour>();

		/// <summary>
		/// Turn the lights on (true) or off (false).
		/// </summary>
		/// <param name="isOn">Weither the lights should be on (true) or off (false).</param>
		public void SetLights(bool isOn)
		{
			foreach (Behaviour behaviour in behaviours)
			{
				behaviour.enabled = isOn;
			}
		}
	}
}
