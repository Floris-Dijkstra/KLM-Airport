using UnityEngine;

namespace Airport
{
	public interface IPlaneInfo
	{
		Vector3 Position { get; }
		PlaneScriptableObject PlaneScriptableObject { get; }
	}
}
