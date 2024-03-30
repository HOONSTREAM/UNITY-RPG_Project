using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jewel_shop_in_Script : MonoBehaviour
{
    public GameObject savedata;
    public GameObject Rudencia_jewel_NPC_Folder;

    private const int RUDENCIAN_JEWEL_SHOP_SCENE_NUMBER = 7;
    private void Start()
    {
        Rudencia_jewel_NPC_Folder = GameObject.Find("NPC Folder_Rudencia_jewel").gameObject;
        Rudencia_jewel_NPC_Folder.gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = RUDENCIAN_JEWEL_SHOP_SCENE_NUMBER;

        GameObject player = Managers.Game.GetPlayer();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata);


        Rudencia_jewel_NPC_Folder.gameObject.SetActive(true); 

        SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
    }
}
