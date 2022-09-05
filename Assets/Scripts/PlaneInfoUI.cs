using TMPro;
using UnityEngine;

namespace Airport
{
	public class PlaneInfoUI : MonoBehaviour
	{
		#region Fields
		[SerializeField]
		private Canvas canvas;

		[SerializeField]
		private CanvasGroup canvasGroup;

		[Space]

		[SerializeField]
		private TextMeshProUGUI type;

		[SerializeField]
		private TextMeshProUGUI manufacturer;

		[SerializeField]
		private TextMeshProUGUI owner;

		[Space]

		[SerializeField]
		private float fadeTime = 1f;

		private IPlaneInfo planeInfo;

		private RectTransform rectTransform;

		private int animationId;
		#endregion

		#region Properties
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
		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
		}

		private void Update()
		{
			if (planeInfo != null)
			{
				Position(planeInfo.Position);
			}
		}
		#endregion

		#region Methods
		private void SetTextFields()
		{
			type.text = planeInfo.PlaneScriptableObject.Type;
			manufacturer.text = planeInfo.PlaneScriptableObject.Manufacturer;
			owner.text = planeInfo.PlaneScriptableObject.OperatingCompany;
		}

		private void ClearTextFields()
		{
			type.text = string.Empty;
			manufacturer.text = string.Empty;
			owner.text = string.Empty;
		}

		private void FadeIn()
		{
			LeanTween.cancel(animationId);
			LeanTween.alphaCanvas(canvasGroup, 1f, fadeTime);
		}

		private void FadeOut()
		{
			animationId = LeanTween.alphaCanvas(canvasGroup, 0f, fadeTime).setOnComplete(() =>
			{
				ClearTextFields();
			}).id;
		}

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
