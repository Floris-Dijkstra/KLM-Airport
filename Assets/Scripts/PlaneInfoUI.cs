using TMPro;
using UnityEngine;

namespace Airport
{
	/// <summary>
	/// Responsible for displaying the plane info in the UI.
	/// </summary>
	public class PlaneInfoUI : MonoBehaviour
	{
		#region Fields
		/// <summary>
		/// Reference to the canvas to concert world positions to anchorpositions.
		/// </summary>
		[SerializeField]
		private Canvas canvas;

		/// <summary>
		/// The canvasgroup of the UI that is used for the animations.
		/// </summary>
		[SerializeField]
		private CanvasGroup canvasGroup;

		/// <summary>
		/// The type field of the <see cref="IPlaneInfo.PlaneScriptableObject"/>.
		/// </summary>
		[Space]
		[SerializeField]
		private TextMeshProUGUI type;

		/// <summary>
		/// The manufacturer field of the <see cref="IPlaneInfo.PlaneScriptableObject"/>.
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI manufacturer;

		/// <summary>
		/// The owner field of the <see cref="IPlaneInfo.PlaneScriptableObject"/>.
		/// </summary>
		[SerializeField]
		private TextMeshProUGUI owner;

		/// <summary>
		/// The fadeIn and fadeOut time of the animations.
		/// </summary>
		[Space]
		[SerializeField]
		private float fadeTime = 1f;

		/// <summary>
		/// The currently displayed plane info.
		/// </summary>
		private IPlaneInfo planeInfo;

		/// <summary>
		/// Reference to the rectTransform of the UI.
		/// </summary>
		private RectTransform rectTransform;

		/// <summary>
		/// The Id of the <see cref="FadeOut"/> animation.
		/// </summary>
		private int animationId;
		#endregion

		#region Properties
		/// <summary>
		/// The currently displayed plane info.
		/// </summary>
		public IPlaneInfo PlaneInfo
		{
			set
			{
				planeInfo = value;
				if (value == null)
				{
					FadeOut();
				}
				else
				{
					SetTextFields();
					FadeIn();
				}
			}
		}
		#endregion

		#region Unity
		/// <summary>
		/// Retrieves a reference to the rectTransform.
		/// </summary>
		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
		}

		/// <summary>
		/// Updates the UI position so that it hoves over the plane.
		/// </summary>
		private void Update()
		{
			if (planeInfo != null)
			{
				Position(planeInfo.Position);
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Fills the text fields in the UI with the plane info.
		/// </summary>
		private void SetTextFields()
		{
			type.text = planeInfo.PlaneScriptableObject.Type;
			manufacturer.text = planeInfo.PlaneScriptableObject.Manufacturer;
			owner.text = planeInfo.PlaneScriptableObject.OperatingCompany;
		}

		/// <summary>
		/// Cleares the text fields in the UI.
		/// </summary>
		private void ClearTextFields()
		{
			type.text = string.Empty;
			manufacturer.text = string.Empty;
			owner.text = string.Empty;
		}

		/// <summary>
		/// Fades in the UI.
		/// </summary>
		private void FadeIn()
		{
			LeanTween.cancel(animationId);
			LeanTween.alphaCanvas(canvasGroup, 1f, fadeTime);
		}

		/// <summary>
		/// Fades out the UI.
		/// </summary>
		private void FadeOut()
		{
			animationId = LeanTween.alphaCanvas(canvasGroup, 0f, fadeTime).setOnComplete(() =>
			{
				ClearTextFields();
			}).id;
		}

		/// <summary>
		/// Converts a world position to a anchored position on the canvas.
		/// </summary>
		/// <param name="worldPosition">The world position to convert.</param>
		private void Position(Vector3 worldPosition)
		{
			Vector3 screenPosition = canvas.worldCamera.WorldToScreenPoint(worldPosition);
			float h = Screen.height;
			float w = Screen.width;
			float x = screenPosition.x - (w / 2);
			float y = screenPosition.y - (h / 2);
			float s = canvas.scaleFactor;
			rectTransform.anchoredPosition = new Vector2(x, y) / s;
		}
		#endregion
	}
}
