using UnityEngine;

namespace Airport
{
	[CreateAssetMenu(fileName = "Plane", menuName = "ScriptableObjects/Plane", order = 1)]
	public class PlaneScriptableObject : ScriptableObject
	{
		#region Fields
		[SerializeField]
		private string type;

		[SerializeField]
		private string manufacturer;

		[SerializeField]
		private string operatingCompany;
		#endregion

		#region Properties
		public string Type => type;

		public string Manufacturer => manufacturer;

		public string OperatingCompany => operatingCompany;
		#endregion
	}
}
