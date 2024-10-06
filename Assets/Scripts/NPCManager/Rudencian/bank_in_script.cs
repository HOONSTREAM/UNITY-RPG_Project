using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bank_in_script : MonoBehaviour
{
    public GameObject savedata;
   
    private void Start()
    {
        savedata = GameObject.Find("Save_Data").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.RudencianBankScene;

        GameObject player = Managers.Game.GetPlayer();
       

        SceneManager.LoadScene(Managers.Scene_Number.LoadingScene);
    }


}
