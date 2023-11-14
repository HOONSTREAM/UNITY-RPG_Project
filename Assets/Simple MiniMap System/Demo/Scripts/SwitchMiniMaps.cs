using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleMiniMapSystem
{
	public class SwitchMiniMaps : MonoBehaviour
	{

		public GameObject[] miniMaps;

		private int currentIndex = 0;

		public Text textStyle;


		void Awake ()
		{
			miniMaps [currentIndex].SetActive (true);
		}

		// Use this for initialization
		void SwitchMiniMap (int miniIndex)
		{
		
			// dont call if anyway actiacted
			if (currentIndex == miniIndex)
				return;

			foreach (var item in miniMaps) {
				item.SetActive (false);
			}


			miniMaps [miniIndex].SetActive (true);
			currentIndex = miniIndex;
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				SwitchMiniMap (0);
				textStyle.text = "Sci-Fi";
			} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
				SwitchMiniMap (1);
				textStyle.text = "RPG";
			} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
				SwitchMiniMap (2);
				textStyle.text = "Chrome";
			} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
				SwitchMiniMap (3);
				textStyle.text = "Modern";
			}
		}
	}
}
