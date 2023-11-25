using UnityEngine;
using UnityEditor;


namespace SimpleMiniMapSystem
{
	
	[CustomEditor (typeof(MiniMapIcon))]
	public class MiniMapIconEditor : Editor
	{
		private Editor _editor;

		MiniMapIcon miniMapIcon;
		MiniMapIconContainer miniMapContainer;

		public string[] iconNames;


		public void OnEnable ()
		{
			miniMapIcon = (MiniMapIcon)target;
			miniMapContainer = miniMapIcon.iconContainer;

			iconNames = new string[miniMapIcon.iconContainer.icons.Length];

			for (int i = 0; i < miniMapContainer.icons.Length; i++) {
				iconNames [i] = miniMapContainer.icons [i].name;
			}
		}

		public override void OnInspectorGUI ()
		{
			EditorGUILayout.LabelField ("Select the icon to instantiate");

			if (miniMapIcon.iconContainer != null)
				miniMapIcon.index = EditorGUILayout.Popup (miniMapIcon.index, iconNames);

			// Draw the default inspector
			DrawDefaultInspector ();

		}
	}
}
