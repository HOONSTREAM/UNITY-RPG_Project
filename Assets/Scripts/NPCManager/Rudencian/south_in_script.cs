using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class south_in_script : MonoBehaviour
{
    public GameObject savedata;
    public GameObject Rudencia_south_NPC_Folder;
    private const int SOUTH_SCENE_NUMBER = 3;
    private const int SOUTH2_SCENE_NUMBER = 9;



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

        switch (SceneManager.GetActiveScene().name)
        {
            case "루덴시안":
                Rudencia_south_NPC_Folder.gameObject.SetActive(true); // 남쪽 NPC(포탈) 활성화
                LoadingScene.NEXT_SCENE_NUMBER = SOUTH_SCENE_NUMBER;
                player.transform.position = new Vector3(-1.1499f, 0, -33.6f);
                SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
                break;

            case "루덴시안 남쪽2":
                LoadingScene.NEXT_SCENE_NUMBER = SOUTH_SCENE_NUMBER;
                player.transform.position = new Vector3(37f, 0, 30f);            
                SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
                break;

        }
    }
}
