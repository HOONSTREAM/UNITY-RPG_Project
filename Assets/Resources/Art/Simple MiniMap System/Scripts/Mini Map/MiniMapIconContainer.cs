using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleMiniMapSystem
{

	[System.Serializable]
	[CreateAssetMenu]
	public class MiniMapIconContainer : ScriptableObject
	{
		public Icon[] icons;

	}

	[System.Serializable]
	public class Icon
	{
		public string name;
		public GameObject prefab;
	}

}