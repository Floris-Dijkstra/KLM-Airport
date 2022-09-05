using TMPro;
using UnityEngine;

namespace Airport
{
	public class Hangar : MonoBehaviour
	{
		#region Fields
		private int number;

		[SerializeField]
		private Transform parkingSpot;

		[SerializeField]
		private TextMeshProUGUI text;

		[SerializeField]
		private CanvasGroup canvasGroup;

		[SerializeField]
		private bool animationPlaying = false;
		#endregion

		#region Properties
		public int Number
		{
			set
			{
				number = value;
				text.text = number.ToString();
			}
			get => number;
		}

		public Vector3 ParkingSpot => parkingSpot.position;
		#endregion

		#region Methods
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
