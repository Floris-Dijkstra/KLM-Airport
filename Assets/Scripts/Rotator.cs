using UnityEngine;

namespace Airport
{
	/// <summary>
	/// Rotates the transform of the gameobject this script is attached to.
	/// </summary>
	public class Rotator : MonoBehaviour
	{
		/// <summary>
		/// Rotations per minute for each axis.
		/// </summary>
		[Tooltip("Rotations per minute for each axis.")]
		[SerializeField]
		private Vector3 rotationsPerMinute;

		/// <summary>
		/// Rotates the transform of the gameobject this script is attached to.
		/// </summary>
		private void Update()
		{
			transform.Rotate(
				AxisRotation(rotationsPerMinute.x),
				AxisRotation(rotationsPerMinute.y),
				AxisRotation(rotationsPerMinute.z));
		}

		/// <summary>
		/// Converts rotations per minute to the amount to rotate in the current frame.
		/// </summary>
		/// <param name="rotationsPerMinute">Rotations per minute.</param>
		/// <returns>Amount to rotate in the current frame.</returns>
		private float AxisRotation(float rotationsPerMinute) =>
			6.0f * rotationsPerMinute * Time.deltaTime;
	}
}

