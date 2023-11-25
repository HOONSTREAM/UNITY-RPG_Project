using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleMiniMapSystem
{
	/// <summary>
	/// Big map camera.
	/// </summary>
	public class BigMapCamera : MonoBehaviour
	{
		[Tooltip ("The canvas of the big map")]
		public GameObject bigMapCanvas;
		private Camera bigMapCamera;

		[Tooltip ("Here you can assign a button to open the big map")]
		public Button button_BigMap;

		[Tooltip ("Activate Camera & Canvas on Play Mode - good for debugging")]
		public bool cameraEnabled = false;

		void Awake ()
		{
			if (bigMapCanvas == null) {
				Debug.Log ("Please assign a Big Map Canvas");
			} else {
				bigMapCamera = GetComponent <Camera> ();
				bigMapCamera.enabled = cameraEnabled;
				bigMapCanvas.SetActive (cameraEnabled);
			}

			if (button_BigMap != null) {
				InitBigMapButton ();
			}

		}

		/// <summary>
		/// Inits the big map button.
		/// </summary>
		void InitBigMapButton ()
		{
			button_BigMap.onClick.AddListener (() => ToggleBigMap ());
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (Input.GetKeyDown (KeyCode.M)) {
				ToggleBigMap ();
			}

		}

		/// <summary>
		/// Toggles the big map.
		/// </summary>
		void ToggleBigMap ()
		{
			cameraEnabled = !cameraEnabled;
			bigMapCamera.enabled = cameraEnabled;
			bigMapCanvas.SetActive (cameraEnabled);
		}

		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		void OnDestroy ()
		{
			if (button_BigMap != null) {
				button_BigMap.onClick.RemoveListener (() => ToggleBigMap ());
			}
		}
	}
}