using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class south_in_script : MonoBehaviour
{
    public GameObject savedata;
    public GameObject Rudencia_south_NPC_Folder;


    private void Start()
    {
        Rudencia_south_NPC_Folder = GameObject.Find("NPC Folder_Rudencia_south").gameObject;
        Rudencia_south_NPC_Folder.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        GameObject player = Managers.Game.GetPlayer();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata);


        Rudencia_south_NPC_Folder.gameObject.SetActive(true); // 남쪽 NPC(포탈) 활성화

        SceneManager.LoadScene("루덴시안 남쪽");
    }
}
