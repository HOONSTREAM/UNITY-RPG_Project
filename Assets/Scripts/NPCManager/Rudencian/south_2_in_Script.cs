using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class south_2_in_Script : MonoBehaviour
{
    public GameObject savedata;
    private const int SOUTH2_SCENE_NUMBER = 9;



    private void Start()
    {
        savedata = GameObject.Find("Save_Data").gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = SOUTH2_SCENE_NUMBER;

        GameObject player = Managers.Game.GetPlayer();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata);


        SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
    }
}
