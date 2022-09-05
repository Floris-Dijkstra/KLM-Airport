using UnityEngine;

namespace Airport
{
	/// <summary>
	/// Exposed the plane to the <see cref="PlaneInfoUI"/>.
	/// </summary>
	public interface IPlaneInfo
	{
		/// <summary>
		/// The current world position of the plane.
		/// </summary>
		Vector3 Position { get; }

		/// <summary>
		/// The scriptableObject containg the information about the plane.
		/// </summary>
		PlaneScriptableObject PlaneScriptableObject { get; }
	}
}
