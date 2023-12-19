using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Rudencia_in_Script : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //GameObject.Find 함수는 비활성화 된 오브젝트는 찾지 못한다.
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>(); //비활성화 및 Don'tDestoryOnLoad 까지 찾는다.

        foreach (var obj in allObjects)
        {
            if (obj.name == "NPC Folder_Rudencia")
            {
                obj.SetActive(true);
                break;
            }
         
        }

        GameObject player = Managers.Game.GetPlayer();
        player.transform.position = new Vector3(8.4f, 0, -43.34f);
      
        SceneManager.LoadScene("루덴시안");
    }
}
