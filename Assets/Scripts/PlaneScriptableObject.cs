using UnityEngine;

namespace Airport
{
	/// <summary>
	/// Scriptable Object that holds info about a certain type of plane.
	/// </summary>
	[CreateAssetMenu(fileName = "Plane", menuName = "ScriptableObjects/Plane", order = 1)]
	public class PlaneScriptableObject : ScriptableObject
	{
		#region Fields
		/// <summary>
		/// The type of the plane.
		/// </summary>
		[SerializeField]
		private string type;

		/// <summary>
		/// The manufacturer of the plane.
		/// </summary>
		[SerializeField]
		private string manufacturer;

		/// <summary>
		/// The operating company of the plane.
		/// </summary>
		[SerializeField]
		private string operatingCompany;
		#endregion

		#region Properties
		/// <summary>
		/// The type of the plane.
		/// </summary>
		public string Type => type;

		/// <summary>
		/// The manufacturer of the plane.
		/// </summary>
		public string Manufacturer => manufacturer;

		/// <summary>
		/// The operating company of the plane.
		/// </summary>
		public string OperatingCompany => operatingCompany;
		#endregion
	}
}
