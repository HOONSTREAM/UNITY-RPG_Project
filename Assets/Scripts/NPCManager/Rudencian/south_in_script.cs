using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class south_in_script : MonoBehaviour
{
    public GameObject savedata;
    
    private const int SOUTH_SCENE_NUMBER = 3;
    private const int SOUTH2_SCENE_NUMBER = 9;


    private void Start()
    {
        savedata = GameObject.Find("Save_Data").gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = SOUTH_SCENE_NUMBER;

        GameObject player = Managers.Game.GetPlayer();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata);

        switch (SceneManager.GetActiveScene().name)
        {
            case "·çµ§½Ã¾È":
                
                LoadingScene.NEXT_SCENE_NUMBER = SOUTH_SCENE_NUMBER;
                player.transform.position = new Vector3(-1.1499f, 0, -33.6f);
                SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
                break;

            case "·çµ§½Ã¾È ³²ÂÊ2":
                LoadingScene.NEXT_SCENE_NUMBER = SOUTH_SCENE_NUMBER;
                player.transform.position = new Vector3(37f, 0, 30f);            
                SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
                break;

        }
    }
}
