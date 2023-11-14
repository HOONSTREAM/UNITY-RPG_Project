using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SimpleMiniMapSystem
{
	[CustomEditor (typeof(MiniMapIconContainer))]
	public class MiniMapIconContainerEditor : Editor
	{

		private string ver = "version: 0.6.8";

		public override void OnInspectorGUI ()
		{
			EditorGUILayout.LabelField ("Simple MiniMap System " + ver);

			EditorGUILayout.Space ();
			EditorGUILayout.LabelField ("Setup here your MiniMap Icon Prefabs");
			EditorGUILayout.Space ();

			// Draw the default inspector
			DrawDefaultInspector ();

		}
	}

}
