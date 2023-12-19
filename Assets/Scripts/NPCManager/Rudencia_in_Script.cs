using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Rudencia_in_Script : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //GameObject.Find �Լ��� ��Ȱ��ȭ �� ������Ʈ�� ã�� ���Ѵ�.
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>(); //��Ȱ��ȭ �� Don'tDestoryOnLoad ���� ã�´�.

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
      
        SceneManager.LoadScene("�絧�þ�");
    }
}
