using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bank_in_script : MonoBehaviour
{
    public GameObject savedata;
   

    private const int RUDENCIAN_BANK_SCENE_NUMBER = 5;

    private void Start()
    {
        savedata = GameObject.Find("Save_Data").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = RUDENCIAN_BANK_SCENE_NUMBER;

        GameObject player = Managers.Game.GetPlayer();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata);
 

        SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
    }


}
