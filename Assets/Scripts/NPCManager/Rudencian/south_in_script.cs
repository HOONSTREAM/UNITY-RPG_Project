using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class south_in_script : MonoBehaviour
{
    public GameObject savedata;
    public GameObject Rudencia_south_NPC_Folder;
    private const int SOUTH_SCENE_NUMBER = 3;
    

    private void Start()
    {
        Rudencia_south_NPC_Folder = GameObject.Find("NPC Folder_Rudencia_south").gameObject;
        Rudencia_south_NPC_Folder.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = SOUTH_SCENE_NUMBER;

        GameObject player = Managers.Game.GetPlayer();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata);

        Rudencia_south_NPC_Folder.gameObject.SetActive(true); // ���� NPC(��Ż) Ȱ��ȭ
   
        SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
    }
}
