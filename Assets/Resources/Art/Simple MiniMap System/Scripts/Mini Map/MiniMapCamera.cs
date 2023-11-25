using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;



namespace SimpleMiniMapSystem
{
	/// <summary>
	/// Mini map camera.
	/// </summary>
	public class MiniMapCamera : MonoBehaviour
	{
		[Header("Canvas Settings")]
		[Tooltip ("The Target Canvas")]
		public Canvas miniMapCanvas;
		
		[Header ("Target Settings")]
		[Tooltip ("The Target for the camera")]
		public Transform target;
		[Tooltip ("Should the Minimap rotate with target?")]
		public bool rotateWithTarget;

		[Header ("Zoom Settings")]
		[Tooltip ("The button for the zoom in")]
		public Button button_ZoomIn;
		[FormerlySerializedAs("Button_ZoomOut")] [Tooltip ("The button for the zoom out")]
		public Button button_ZoomOut;
		[Tooltip ("Start value of the zoom")]
		public float startZoom;
		[Tooltip ("Minimum of the zoom")]
		public float minZoom;
		[Tooltip ("Maximum of the zoom")]
		public float maxZoom;
		[Tooltip ("How speed of the zoom")]
		public float zoomSteps = 1;
		[Tooltip ("Sound for the buttons")]
		public AudioClip buttonClickSound;


		private Camera miniMapCamera;
		private AudioSource buttonAudio;

		[Header("Distance Text")] 
		public bool showDistanceText;
		public Text distanceText;


		/// <summary>
		/// Awake this instance.
		/// </summary>
		public virtual void Awake ()
		{
			if (target == null) {
				Debug.LogError ("Simple MiniMap System: There is no target set");
			}

			miniMapCamera = GetComponent <Camera> ();
			
			if (miniMapCamera != null)
			{
				miniMapCamera.orthographicSize = startZoom;
			}
			
			if (button_ZoomIn != null && button_ZoomOut != null) {
				InitZoomButtons ();
			}
			
		}

		public void Update()
		{
			//if(showDistanceText)
				//ProcessDistanceText();
		}

		
		public virtual void LateUpdate ()
		{
			if (target != null) {

				transform.position = new Vector3 (target.position.x, transform.position.y, target.position.z);
			
				if (rotateWithTarget) {
					transform.rotation = Quaternion.Euler (90f, target.eulerAngles.y, 0f);
				}
			
			}
		}
		
		/// <summary>
		/// Inits the zoom buttons.
		/// </summary>
		public virtual void InitZoomButtons ()
		{
			button_ZoomIn.onClick.AddListener (() => ZoomIN ());
			button_ZoomOut.onClick.AddListener (() => ZoomOUT ());

			if (buttonClickSound != null) {
				gameObject.AddComponent <AudioSource> ();
				buttonAudio = GetComponent <AudioSource> ();
			}

		}

		/// <summary>
		/// Zooms IN
		/// </summary>
		public virtual void ZoomIN ()
		{
			miniMapCamera.orthographicSize -= zoomSteps;

			if (miniMapCamera.orthographicSize <= minZoom)
				miniMapCamera.orthographicSize = minZoom;

			if (buttonAudio != null)
				buttonAudio.PlayOneShot (buttonClickSound);

		}

		/// <summary>
		/// Zooms OUT
		/// </summary>
		public virtual void ZoomOUT ()
		{
			miniMapCamera.orthographicSize += zoomSteps;

			if (miniMapCamera.orthographicSize >= maxZoom)
				miniMapCamera.orthographicSize = maxZoom;

			if (buttonAudio != null)
				buttonAudio.PlayOneShot (buttonClickSound);
		}


		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		public virtual void OnDestroy ()
		{
			if (button_ZoomIn != null && button_ZoomOut != null) {
				button_ZoomIn.onClick.RemoveListener (() => ZoomIN ());
				button_ZoomOut.onClick.RemoveListener (() => ZoomOUT ());
			}
		}

		public virtual void EnableCamera()
		{
			if(miniMapCamera != null)
				miniMapCamera.enabled = true;
		}
		
		public virtual void DisableCamera()
		{
			if(miniMapCamera != null)
				miniMapCamera.enabled = false;
		}
		
		public virtual void EnableCanvas()
		{
			if(miniMapCanvas != null)
				miniMapCanvas.enabled = true;
		}
		
		public virtual void DisableCanvas()
		{
			if(miniMapCanvas != null)
				miniMapCanvas.enabled = false;
		}

		
		//public virtual void ProcessDistanceText()
		//{
		
		//	if (distanceText == null)
		//		Debug.Log("Please assign a Text to show the distance");
			
		//	int distance = MiniMapArrow.Instance.GetDistanceToTarget();
		//	distanceText.text = distance + "M";

		//}
		
	}

}