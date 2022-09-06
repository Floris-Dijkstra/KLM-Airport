using System;
using UnityEngine;

namespace Airport
{
	/// <summary>
	/// Orchestrates all logic tied to this plane.
	/// </summary>
	public class Plane : MonoBehaviour
	{
		#region Fields
		/// <summary>
		/// Moves the plane around in the airport.
		/// </summary>
		[SerializeField]
		private Movement movement;

		/// <summary>
		/// Responsible for the lights on and off.
		/// </summary>
		[SerializeField]
		private Lights lights;

		/// <summary>
		/// Reference to the assing hangar.
		/// </summary>
		[SerializeField]
		private Hangar hangar;
		#endregion

		#region Methods
		/// <summary>
		/// Assign a new hangar to this plane.
		/// </summary>
		/// <param name="hangar">The hangar to assign to this plane.</param>
		public void AssignHangar(Hangar hangar)
		{
			if (this.hangar)
				movement.OnDestinationReachedAndStopped.RemoveListener(this.hangar.PlayParkedAnimation);

			this.hangar = hangar;
			movement.OnDestinationReachedAndStopped.AddListener(this.hangar.PlayParkedAnimation);
		}

		/// <summary>
		/// Tells the plane go to its assigned parkingspot.
		/// </summary>
		/// <exception cref="Exception">Throws an error if the assign parkingspot can't be sampled on the navmesh.</exception>
		public void Park()
		{
			if (!hangar) return;
			movement.StopOnReachedDestination = true;
			if (!movement.SetDestination(hangar.ParkingSpot))
			{
				throw new Exception("The parking spot is not a valid position on the nav mesh.");
			}
		}

		/// <summary>
		/// Tells the plane to drive randomly around the airport.
		/// </summary>
		public void Drive()
		{
			movement.StopOnReachedDestination = false;
		}

		/// <summary>
		/// Turns on the lights of the plane.
		/// </summary>
		public void LightsOn() => lights.SetLights(true);

		/// <summary>
		/// Turns off the ligths of the plane.
		/// </summary>
		public void LightsOff() => lights.SetLights(false);
		#endregion
	}
}
