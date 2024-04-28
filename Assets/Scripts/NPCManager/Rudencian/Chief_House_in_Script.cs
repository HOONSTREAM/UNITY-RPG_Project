using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chief_House_in_Script : MonoBehaviour
{
    public GameObject savedata;


    private const int RUDENCIAN_CHIEF_HOUSE_SCENE_NUMBER = 10;

    private void Start()
    {
        savedata = GameObject.Find("Save_Data").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = RUDENCIAN_CHIEF_HOUSE_SCENE_NUMBER;

        GameObject player = Managers.Game.GetPlayer();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata);

        SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
    }

}
