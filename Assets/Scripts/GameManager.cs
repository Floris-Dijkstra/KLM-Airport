using Airport.Plane;
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
		/// <summary>
		/// 
		/// </summary>
		[SerializeField]
		private UIManager uiManager;

		[SerializeField]
		private PlaneInfoUI planeInfoUI;

		[SerializeField]
		private List<Hangar> hangars;

		[SerializeField]
		private List<Plane.Plane> planes;

		[SerializeField]
		private List<PlaneInfoManager> planeUIManagers;

		private void Start()
		{
			hangars = GetAllObjectsOnlyInScene<Hangar>();
			planes = GetAllObjectsOnlyInScene<Plane.Plane>();
			planeUIManagers = GetAllObjectsOnlyInScene<PlaneInfoManager>();

			AssignHangarsToPlanes();
			AssignUIButtonsToPlanes();
			AssignPlaneUIManagerToPlaneInfoUIs();
		}

		private void OnEnable()
		{
			AssignUIButtonsToPlanes();
		}

		private void OnDisable()
		{
			UnassignUIButtonToPlanes();
		}

		private void AssignPlaneUIManagerToPlaneInfoUIs()
		{
			foreach (PlaneInfoManager planeUIManager in planeUIManagers)
			{
				planeUIManager.planeInfoUI = planeInfoUI;
			}
		}

		private void AssignHangarsToPlanes()
		{
			if (hangars.Count != planes.Count)
				throw new Exception("The amount of hangars is not equal to the amount of planes in the scene.");

			for (int i = 0; i < planes.Count; i++)
			{
				hangars[i].Number = i;
				planes[i].AssignHangar(hangars[i]);
			}
		}

		private void AssignUIButtonsToPlanes()
		{
			foreach (Plane.Plane plane in planes)
			{
				uiManager.LightsOn.AddListenerToOnClick(plane.LightsOn);
				uiManager.LightsOff.AddListenerToOnClick(plane.LightsOff);
				uiManager.Park.AddListenerToOnClick(plane.Park);
				uiManager.Drive.AddListenerToOnClick(plane.Drive);
			}
		}

		private void UnassignUIButtonToPlanes()
		{
			foreach (Plane.Plane plane in planes)
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
				if (!EditorUtility.IsPersistent(component.transform.root.gameObject) && !(component.hideFlags == HideFlags.NotEditable || component.hideFlags == HideFlags.HideAndDontSave))
					objectsInScene.Add(component as T);
			}

			return objectsInScene;
		}

	}
}
