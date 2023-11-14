using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using System.Reflection.Emit;

namespace SimpleMiniMapSystem
{
	/// <summary>
	/// Mini map icon.
	/// </summary>
	public class MiniMapIcon : MonoBehaviour
	{
		[Tooltip ("The Container which hold the icons. To create a MiniMap Icon Container right click in the Project Folder -> Create -> MiniMap Icon Container")]
		public MiniMapIconContainer iconContainer;

		[Tooltip ("The height of the instancing icon")]
		public float instancingHeight = 3f;

		[HideInInspector]
		public int index = 0;

		[Tooltip ("The event raised when the object is destroyed")]
		public UnityEvent onDestroyEvent;

		// Reference to object
		private GameObject miniMapIcon;

		public virtual void Awake ()
		{
			AssignIcon ();
		}

		/// <summary>
		/// Assigns the icon.
		/// </summary>
		public virtual void AssignIcon ()
		{
			Quaternion rotation = Quaternion.Euler (new Vector3 (90, 0, 0));

			miniMapIcon = Instantiate (iconContainer.icons [index].prefab, transform.position, Quaternion.identity);

			// parent and postion the icon
			miniMapIcon.transform.parent = this.transform;
			miniMapIcon.transform.localPosition = new Vector3 (0, instancingHeight, 0);
			miniMapIcon.transform.rotation = rotation;
			miniMapIcon.layer = 30; //미니맵 아이콘의 레이어는 Minimap(30)번이고, 메인카메라에 렌더링 되지 않아야함

		}

		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		public virtual void OnDestroy ()
		{
			onDestroyEvent.Invoke ();
		}


		/// <summary>
		/// Sets the icon visibilty.
		/// </summary>
		/// <param name="visible">If set to <c>true</c> visible.</param>
		public virtual void SetIconVisibilty (bool visible)
		{
			miniMapIcon.SetActive (visible);
		}

		/// <summary>
		/// Changes the minimap icon on fly. Use the index of your iconContainer
		/// </summary>
		/// <param name="newIconIndex">New icon index.</param>
		public virtual void ChangeMinimapIcon (int newIconIndex)
		{

			if (newIconIndex >= iconContainer.icons.Length) {
				Debug.Log ("Index out of range dude");
				return;
			}


			// delete old icon
			Destroy (miniMapIcon);

			Quaternion rotation = Quaternion.Euler (new Vector3 (90, 0, 0));

			miniMapIcon = Instantiate (iconContainer.icons [newIconIndex].prefab, transform.position, Quaternion.identity);

			// parent and postion the icon
			miniMapIcon.transform.parent = this.transform;
			miniMapIcon.transform.localPosition = new Vector3 (0, instancingHeight, 0);
			miniMapIcon.transform.rotation = rotation;
			
		}

	}
}

