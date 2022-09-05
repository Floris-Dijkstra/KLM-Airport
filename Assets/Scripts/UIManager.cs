using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Airport
{
	public class UIManager : MonoBehaviour
	{
		#region Fields
		[SerializeField]
		private AssignableButton park;

		[SerializeField]
		private AssignableButton drive;

		[SerializeField]
		private AssignableButton lightsOn;

		[SerializeField]
		private AssignableButton lightsOff;
		#endregion

		#region Properties
		public AssignableButton Park => park;

		public AssignableButton Drive => drive;

		public AssignableButton LightsOn => lightsOn;

		public AssignableButton LightsOff => lightsOff;
		#endregion

		#region Classes
		[Serializable]
		public class AssignableButton
		{
			[SerializeField]
			private Button button;

			public void AddListenerToOnClick(UnityAction call)
			{
				button.onClick.AddListener(call);
			}

			public void RemoveListenerToOnClick(UnityAction call)
			{
				button.onClick.RemoveListener(call);
			}
		}
		#endregion
	}
}
