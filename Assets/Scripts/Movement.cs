using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Airport
{
	/// <summary>
	/// Moves the plane around airport in random directions or a set destination.
	/// </summary>
	public class Movement : MonoBehaviour, IAgent
	{
		#region Fields
		/// <summary>
		/// The navmeshagent used to navigate the airport.
		/// </summary>
		[SerializeField]
		private NavMeshAgent navMeshAgent;

		/// <summary>
		/// The maximum allowed range of the next destination.
		/// </summary>
		[SerializeField, FormerlySerializedAs("nextTargetRange")]
		private float destinationRange;

		/// <summary>
		/// Extra space from the actual navmeshagent radius.
		/// </summary>
		[SerializeField]
		private float agentRadiusMargin;

		/// <summary>
		/// If the plane should stop when it reached its current destination.
		/// </summary>
		private bool stopOnReachedDestination = false;

		/// <summary>
		/// If the <see cref="OnDestinationReachedAndStopped"/> event is already invoked to prevent reinvoking every frame.
		/// </summary>
		private bool invokedOnDestinationReachedAndStopped = false;
		#endregion

		#region Properties
		/// <summary>
		/// The unique id of this plane.
		/// </summary>
		public int Id => GetInstanceID();

		/// <summary>
		/// The current world position of the plane.
		/// </summary>
		public Vector3 WorldPosition => transform.position;

		/// <summary>
		/// The current destination of the plane.
		/// </summary>
		public Vector3 Destination => navMeshAgent.destination;

		/// <summary>
		/// The <see cref="NavMeshAgent"/> radius.
		/// </summary>
		public float AgentRadius => navMeshAgent.radius + agentRadiusMargin;

		/// <summary>
		/// The maximum allowed distance of the next random destination.
		/// </summary>
		public float DestinationRange => destinationRange;

		/// <summary>
		/// If the plane should stop when it reached its current destination.
		/// </summary>
		public bool StopOnReachedDestination
		{
			get => stopOnReachedDestination;
			set
			{
				stopOnReachedDestination = value;
				if (value) invokedOnDestinationReachedAndStopped = false;
			}
		}
		#endregion

		#region Events
		/// <summary>
		/// Fires when the plane reached its destination.
		/// </summary>
		public UnityEvent OnDestinationReached;

		/// <summary>
		/// Fires when the plane reached its destination and has stopped.
		/// </summary>
		public UnityEvent OnDestinationReachedAndStopped;
		#endregion

		#region Unity
		/// <summary>
		/// Add this to the destination manager.
		/// </summary>
		private void Start()
		{
			AgentManager.AddAgent(this);
		}

		/// <summary>
		/// Remove this from the destination manager.
		/// </summary>
		private void OnDestroy()
		{
			AgentManager.RemoveAgent(Id);
		}

		/// <summary>
		/// Sets a new destination when the current one has been reached.
		/// </summary>
		private void Update()
		{
			if (PathComplete() && !stopOnReachedDestination)
			{
				OnDestinationReached.Invoke();

				if (AgentManager.UpdateDestination(this, out Vector3 point))
				{
					navMeshAgent.destination = point;
				}
			}
			else if (PathComplete() && stopOnReachedDestination && !invokedOnDestinationReachedAndStopped)
			{
				invokedOnDestinationReachedAndStopped = true;
				OnDestinationReachedAndStopped.Invoke();
			}
		}

		/// <summary>
		/// Draws the set destination in the scene view.
		/// </summary>
		private void OnDrawGizmos()
		{
			if (!navMeshAgent) return;
			Gizmos.color = Color.blue;
			float radius = 0.1f;
			Gizmos.DrawSphere(navMeshAgent.destination, radius);
			Gizmos.DrawLine(transform.position, navMeshAgent.destination);
		}
		#endregion

		#region Methods
		/// <summary>
		/// Sets a new destination.
		/// </summary>
		/// <param name="destination">The new destination.</param>
		/// <returns>True if the new destianion can be found on the navMesh.</returns>
		public bool SetDestination(Vector3 destination)
		{
			float maxDistance = 10f;
			if (NavMesh.SamplePosition(destination, out NavMeshHit hit, maxDistance, NavMesh.AllAreas))
			{
				navMeshAgent.destination = hit.position;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Checks if the navMeshAgent reached the set destination.
		/// </summary>
		/// <returns>True if the destianion has been reached.</returns>
		private bool PathComplete()
		{
			if (Vector3.Distance(navMeshAgent.destination, navMeshAgent.transform.position) <= navMeshAgent.stoppingDistance)
			{
				if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
				{
					return true;
				}
			}
			return false;
		}
		#endregion
	}
}
