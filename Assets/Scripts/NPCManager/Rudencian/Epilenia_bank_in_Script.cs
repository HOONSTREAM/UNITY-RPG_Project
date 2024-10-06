using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Epilenia_bank_in_Script : MonoBehaviour
{
    public GameObject savedata;

    private void Start()
    {
        savedata = GameObject.Find("Save_Data").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.EpileniaBankScene;

        GameObject player = Managers.Game.GetPlayer();


        SceneManager.LoadScene(Managers.Scene_Number.LoadingScene);
    }


}
