using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class south_2_in_Script : MonoBehaviour
{
    public GameObject savedata;
    private void Start()
    {
        savedata = GameObject.Find("Save_Data").gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_South2_Scene();

        GameObject player = Managers.Game.GetPlayer();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata);


        SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
    }
}
