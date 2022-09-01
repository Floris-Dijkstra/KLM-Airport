using UnityEngine;

namespace Airport
{
	[CreateAssetMenu(fileName = "Plane", menuName = "ScriptableObjects/Plane", order = 1)]
	public class PlaneScriptableObject : ScriptableObject
	{
		[SerializeField]
		private string type;

		[SerializeField]
		private string manufacturer;
	}
}
