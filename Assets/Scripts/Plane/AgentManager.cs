using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Airport
{
	/// <summary>
	/// This class ensures the random destinations of the planes are sampled correctly and are not within a certain range of each other to prevent planes getting stuck.
	/// </summary>
	public static class AgentManager
	{
		#region Fields
		/// <summary>
		/// List of all active planes in the airport.
		/// /// </summary>
		private static List<IAgent> agents = new List<IAgent>();
		#endregion

		#region Methods
		/// <summary>
		/// Adds an agent to this manager.
		/// </summary>
		/// <param name="destination"></param>
		public static void AddAgent(IAgent destination)
		{
			// Only add the agent if it isn't added yet.
			if (!agents.Where(x => x.Id == destination.Id).Any())
			{
				agents.Add(destination);
			}
		}

		/// <summary>
		/// Assigns a new valid agent for the plane to travel to.
		/// </summary>
		/// <param name="agent"></param>
		/// <param name="result">The new position of the agent.</param>
		/// <returns>Weither a new agent is found.</returns>
		public static bool UpdateDestination(IAgent agent, out Vector3 result)
		{
			// Try to find a new destination n times to prevent infinite looping.
			int tries = 30;
			for (int i = 0; i < tries; i++)
			{
				// Generate a random point.
				Vector3 randomPoint = agent.WorldPosition + Random.insideUnitSphere * agent.DestinationRange;

				// If the generated point can be places on the navmesh, it results to true.
				if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
				{
					result = hit.position;
					bool valid = true;

					// Check if the generated point is not colliding with other generated points.
					foreach (IAgent otherAgent in agents)
					{
						if (Vector3.Distance(otherAgent.Destination, result) < otherAgent.AgentRadius + agent.AgentRadius)
						{
							valid = false;
						}
					}
					if (valid) return true;
				}
			}
			result = Vector3.zero;
			return false;
		}

		/// <summary>
		/// Remove an agent.
		/// </summary>
		/// <param name="id"></param>
		public static void RemoveAgent(int id)
		{
			agents.RemoveAll(x => x.Id == id);
		}
		#endregion
	}
}
