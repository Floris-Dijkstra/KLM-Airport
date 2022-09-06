using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Airport
{
	/// <summary>
	/// Manager for the UI buttons in the scene.
	/// </summary>
	public class UIManager : MonoBehaviour
	{
		#region Fields
		/// <summary>
		/// Commands the planes to got to their assigned hangar.
		/// </summary>
		[SerializeField]
		private AssignableButton park;

		/// <summary>
		/// Commands the planes to drive around randomly.
		/// </summary>
		[SerializeField]
		private AssignableButton drive;

		/// <summary>
		/// Commands the planes to turn their lights on.
		/// </summary>
		[SerializeField]
		private AssignableButton lightsOn;

		/// <summary>
		/// Commands the planes to turn their lights off.
		/// </summary>
		[SerializeField]
		private AssignableButton lightsOff;
		#endregion

		#region Properties
		/// <summary>
		/// Commands the planes to got to their assigned hangar.
		/// </summary>
		public AssignableButton Park => park;

		/// <summary>
		/// Commands the planes to drive around randomly.
		/// </summary>
		public AssignableButton Drive => drive;

		/// <summary>
		/// Commands the planes to turn their lights on.
		/// </summary>
		public AssignableButton LightsOn => lightsOn;

		/// <summary>
		/// Commands the planes to turn their lights off.
		/// </summary>
		public AssignableButton LightsOff => lightsOff;
		#endregion

		#region Classes
		/// <summary>
		/// Exposed the onclick event of the <see cref="Button"/>.
		/// </summary>
		[Serializable]
		public class AssignableButton
		{
			/// <summary>
			/// The button.
			/// </summary>
			[SerializeField]
			private Button button;

			/// <summary>
			/// Adds a <see cref="UnityAction"/> to the onclick event of the <see cref="Button"/>.
			/// </summary>
			/// <param name="call">The listener to add.</param>
			public void AddListenerToOnClick(UnityAction call)
			{
				button.onClick.AddListener(call);
			}

			/// <summary>
			/// Removes a <see cref="UnityAction"/> to the onclick event of the <see cref="Button"/>.
			/// </summary>
			/// <param name="call">The listener to remove.</param>
			public void RemoveListenerToOnClick(UnityAction call)
			{
				button.onClick.RemoveListener(call);
			}
		}
		#endregion
	}
}
