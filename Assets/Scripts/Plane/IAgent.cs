using UnityEngine;
using UnityEngine.AI;

namespace Airport
{
	/// <summary>
	/// Exposes the plane logic to the <see cref="AgentManager"/>.
	/// </summary>
	public interface IAgent
	{
		/// <summary>
		/// Unique id to identify planes.
		/// </summary>
		public int Id { get; }

		/// <summary>
		/// The current world position of the plane.
		/// </summary>
		public Vector3 WorldPosition { get; }

		/// <summary>
		/// The current destination of the plane.
		/// </summary>
		public Vector3 Destination { get; }

		/// <summary>
		/// The <see cref="NavMeshAgent"/> radius.
		/// </summary>
		public float AgentRadius { get; }

		/// <summary>
		/// The maximum allowed distance of the next random destination.
		/// </summary>
		public float DestinationRange { get; }
	}
}
