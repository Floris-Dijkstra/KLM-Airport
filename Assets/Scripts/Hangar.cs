using TMPro;
using UnityEngine;

namespace Airport
{
	/// <summary>
	/// Resposible for holding the hangar number and the world position of the parkingspot.
	/// </summary>
	public class Hangar : MonoBehaviour
	{
		#region Fields
		/// <summary>
		/// The hangar number.
		/// </summary>
		private int number;

		/// <summary>
		/// The transform of the parkingspot.
		/// </summary>
		[SerializeField]
		private Transform parkingSpot;

		/// <summary>
		/// Text that displays the hangar number.
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI text;

		/// <summary>
		/// Canvasgroup that is used for the parked animation.
		/// </summary>
		[SerializeField]
		private CanvasGroup canvasGroup;

		/// <summary>
		/// If the parked animation is playing.
		/// </summary>
		[SerializeField]
		private bool animationPlaying = false;
		#endregion

		#region Properties
		/// <summary>
		/// The hangar number.
		/// </summary>
		public int Number
		{
			set
			{
				number = value;
				text.text = number.ToString();
			}
			get => number;
		}

		/// <summary>
		/// World position of the parkingspot.
		/// </summary>
		public Vector3 ParkingSpot => parkingSpot.position;
		#endregion

		#region Methods
		/// <summary>
		/// Lets the <see cref="canvasGroup"/> fade-in and -out shortly.
		/// </summary>
		public void PlayParkedAnimation()
		{
			if (animationPlaying) return;
			animationPlaying = true;
			LeanTween.alphaCanvas(canvasGroup, 1f, 1f).setOnComplete(() =>
			{
				LeanTween.alphaCanvas(canvasGroup, 0f, 1f).setOnComplete(() =>
				{
					animationPlaying = false;
				});
			});
		}
		#endregion
	}
}
