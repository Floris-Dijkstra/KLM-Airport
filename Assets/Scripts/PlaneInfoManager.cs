using UnityEngine;

namespace Airport
{
	/// <summary>
	/// Manages the UI trigger hover for the plane.
	/// </summary>
	public class PlaneInfoManager : MonoBehaviour, IPlaneInfo
	{
		#region Fields
		/// <summary>
		/// Scriptable Object containing all info about the plane.
		/// </summary>
		[SerializeField]
		private PlaneScriptableObject planeScriptableObject;

		/// <summary>
		/// Reference to the Plane Info UI script that displayes the info.
		/// </summary>
		public PlaneInfoUI planeInfoUI;
		#endregion

		#region Properties
		/// <summary>
		/// The current world position of the plane.
		/// </summary>
		public Vector3 Position => transform.position;

		/// <summary>
		/// Scriptable Object containing all info about the plane. 
		/// </summary>
		public PlaneScriptableObject PlaneScriptableObject => planeScriptableObject;
		#endregion

		#region Unity
		/// <summary>
		/// Triggers when the mouse enters the collider of this plane.
		/// </summary>
		private void OnMouseEnter()
		{
			planeInfoUI.PlaneInfo = this;
		}

		/// <summary>
		/// Triggers when the mouse exits the collider of this plane.
		/// </summary>
		private void OnMouseExit()
		{
			planeInfoUI.PlaneInfo = null;
		}
		#endregion
	}
}
