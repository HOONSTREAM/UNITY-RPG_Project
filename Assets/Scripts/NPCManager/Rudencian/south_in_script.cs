using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class south_in_script : MonoBehaviour
{
    public GameObject savedata;
    
    private void Start()
    {
        savedata = GameObject.Find("Save_Data").gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_south();

        GameObject player = Managers.Game.GetPlayer();
        

        switch (SceneManager.GetActiveScene().name)
        {
            case "�絧�þ�":
                
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_south();
                player.transform.position = new Vector3(-1.1499f, 0, -33.6f);
                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
                break;

            case "�絧�þ� ����2":
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_south();
                player.transform.position = new Vector3(37f, 0, 30f);            
                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
                break;

        }
    }
}
