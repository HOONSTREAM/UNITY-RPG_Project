using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jewel_shop_in_Script : MonoBehaviour
{
    public GameObject savedata;

    private const int RUDENCIAN_JEWEL_SHOP_SCENE_NUMBER = 7;

    private void Start()
    {
        savedata = GameObject.Find("Save_Data").gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = RUDENCIAN_JEWEL_SHOP_SCENE_NUMBER;

        GameObject player = Managers.Game.GetPlayer();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata);

        SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
    }
}
