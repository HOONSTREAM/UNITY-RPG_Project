using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shop_in_script : MonoBehaviour
{
    public GameObject savedata;
  


    private void OnTriggerEnter(Collider other)
    {
        GameObject player = Managers.Game.GetPlayer();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata);

        GameObject.Find("NPC Folder_Rudencia").gameObject.SetActive(false); // �絧�þ� ���� NPC Off
        SceneManager.LoadScene("�絧�þ� ����");
    }


}
