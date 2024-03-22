using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inn_in_Script : MonoBehaviour
{
    public GameObject savedata;
    public GameObject Rudencia_Inn_NPC_Folder;
   
    private void Start()
    {
        Rudencia_Inn_NPC_Folder = GameObject.Find("NPC Folder_Rudencia_Inn").gameObject;
        Rudencia_Inn_NPC_Folder.gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter(Collider other)
    {

        GameObject player = Managers.Game.GetPlayer();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata);


        Rudencia_Inn_NPC_Folder.gameObject.SetActive(true); // ���� NPC(��Ż) Ȱ��ȭ
       
        SceneManager.LoadScene("�絧�þ� ����");
    }
}
