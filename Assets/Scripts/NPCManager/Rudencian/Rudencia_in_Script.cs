using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Rudencia_in_Script : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_scene();

        GameObject player = Managers.Game.GetPlayer();
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();

        switch (SceneManager.GetActiveScene().name)
        {

            case "루덴시안 보석상":

                player.transform.position = new Vector3(-15.118f, 0, -10.432f);
                pc.State = Define.State.Idle;      
                
                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
                break;

            case "루덴시안 남쪽":
               
                player.transform.position = new Vector3(23.25f, 0, -41.45f);               
                pc.State = Define.State.Idle;
            
                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
                break;

            case "루덴시안 상점":
            
                player.transform.position = new Vector3(23.25f, 0, -33.15f);              
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
                break;

            case "루덴시안 은행":
               
                player.transform.position = new Vector3(-4.93f, 0, -30.87f);            
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
                break;

            case "루덴시안 여관":

             
                player.transform.position = new Vector3(-1.06f, 0, -10.24f);
                player.transform.rotation = new Quaternion(0, 180f, 0, 0);
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());

                break;

            case "촌장 월터의 집":

                player.transform.position = new Vector3(-9.819f, 0, -0.763f);
                player.transform.rotation = new Quaternion(0, 180f, 0, 0);
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());

                break;

            case "에반의 집":

                player.transform.position = new Vector3(-23.0f, 0, -20.0f);
                player.transform.rotation = new Quaternion(0, 180f, 0, 0);
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());

                break;

            case "루키스의 집":

                player.transform.position = new Vector3(-21.9f, 0, -20.5f);
                player.transform.rotation = new Quaternion(0, 85f, 0, 0);
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());

                break;


        }
        

    }
}
