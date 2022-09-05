using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Airport
{
	/// <summary>
	/// The main manager of this game.
	/// </summary>
	public class GameManager : MonoBehaviour
	{
		#region Fields
		/// <summary>
		/// Reference to the UIManager.
		/// </summary>
		[SerializeField]
		private UIManager uiManager;

		/// <summary>
		/// Reference to the PlaneInfoUI.
		/// </summary>
		[SerializeField]
		private PlaneInfoUI planeInfoUI;

		/// <summary>
		/// List with all hangars in the scene.
		/// </summary>
		[SerializeField]
		private List<Hangar> hangars;

		/// <summary>
		/// List with all planes in the scene.
		/// </summary>
		[SerializeField]
		private List<Plane> planes;

		/// <summary>
		/// List with all PlaneInfoManagers in the scene.
		/// </summary>
		[SerializeField]
		private List<PlaneInfoManager> planeUIManagers;
		#endregion

		#region Unity
		/// <summary>
		/// Assigns all required references in the scene.
		/// </summary>
		private void Start()
		{
			hangars = GetAllObjectsOnlyInScene<Hangar>();
			planes = GetAllObjectsOnlyInScene<Plane>();
			planeUIManagers = GetAllObjectsOnlyInScene<PlaneInfoManager>();

			AssignHangarsToPlanes();
			AssignUIButtonsToPlanes();
			AssignPlaneInfoManagerToPlaneInfoUI();
		}

		/// <summary>
		/// Assigns the UI buttons to each plane.
		/// </summary>
		private void OnEnable()
		{
			AssignUIButtonsToPlanes();
		}

		/// <summary>
		/// unassigns the UI buttons to each plane.
		/// </summary>
		private void OnDisable()
		{
			UnassignUIButtonsToPlanes();
		}
		#endregion

		#region Methods
		/// <summary>
		/// Assign the planeInfoManagers to the planeInfoUI.
		/// </summary>
		private void AssignPlaneInfoManagerToPlaneInfoUI()
		{
			foreach (PlaneInfoManager planeUIManager in planeUIManagers)
			{
				planeUIManager.planeInfoUI = planeInfoUI;
			}
		}

		/// <summary>
		/// Assigns the hangars to each plane.
		/// </summary>
		/// <exception cref="Exception"></exception>
		private void AssignHangarsToPlanes()
		{
			if (hangars.Count != planes.Count)
				throw new Exception("The amount of hangars is not equal to the amount of planes in the scene.");

			for (int i = 0; i < planes.Count; i++)
			{
				hangars[i].Number = i + 1; // The hangars shouldn't start at 0.
				planes[i].AssignHangar(hangars[i]);
			}
		}

		/// <summary>
		/// Assigns the onclick listeners.
		/// </summary>
		private void AssignUIButtonsToPlanes()
		{
			foreach (Plane plane in planes)
			{
				uiManager.LightsOn.AddListenerToOnClick(plane.LightsOn);
				uiManager.LightsOff.AddListenerToOnClick(plane.LightsOff);
				uiManager.Park.AddListenerToOnClick(plane.Park);
				uiManager.Drive.AddListenerToOnClick(plane.Drive);
			}
		}

		/// <summary>
		/// Unassigns the onclick listeners.
		/// </summary>
		private void UnassignUIButtonsToPlanes()
		{
			foreach (Plane plane in planes)
			{
				uiManager.LightsOn.RemoveListenerToOnClick(plane.LightsOn);
				uiManager.LightsOff.RemoveListenerToOnClick(plane.LightsOff);
				uiManager.Park.RemoveListenerToOnClick(plane.Park);
				uiManager.Drive.RemoveListenerToOnClick(plane.Drive);
			}
		}

		/// <summary>
		/// Finds all objects of type T in the scene hierachy.
		/// </summary>
		/// <typeparam name="T">Contrained to <see cref="Component"/>.</typeparam>
		/// <returns>A list with all found objects of type T.</returns>
		private List<T> GetAllObjectsOnlyInScene<T>() where T : Component
		{
			List<T> objectsInScene = new List<T>();

			foreach (Component component in Resources.FindObjectsOfTypeAll(typeof(T)))
			{
				if (!EditorUtility.IsPersistent(component.transform.root.gameObject) &&
					!(component.hideFlags == HideFlags.NotEditable ||
					component.hideFlags == HideFlags.HideAndDontSave))
					objectsInScene.Add(component as T);
			}

			return objectsInScene;
		}
		#endregion
	}
}
