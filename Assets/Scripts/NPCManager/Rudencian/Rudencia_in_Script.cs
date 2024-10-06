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

                player.transform.position = new Vector3(12.6f, 0, 18.3f);
                pc.State = Define.State.Idle;      
                
                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
                break;

            case "루덴시안 남쪽":
               
                player.transform.position = new Vector3(24.3f, 0, 6.67f);               
                pc.State = Define.State.Idle;
            
                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
                break;

            case "루덴시안 상점":
            
                player.transform.position = new Vector3(29.52f, 0, 23.68f);              
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
                break;

            case "루덴시안 은행":
               
                player.transform.position = new Vector3(21.86f, 0, 21.58f);            
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
                break;

            case "루덴시안 여관":

             
                player.transform.position = new Vector3(25.403f, 0, 31.73f);
                player.transform.rotation = new Quaternion(0, 180f, 0, 0);
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());

                break;

            case "촌장 월터의 집":

                player.transform.position = new Vector3(37.93f, 0, 27.75f);
                player.transform.rotation = new Quaternion(0, 180f, 0, 0);
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());

                break;

          
            case "루키스의 집":

                player.transform.position = new Vector3(19.0f, 0, 18.6f);
                player.transform.rotation = new Quaternion(0, 85f, 0, 0);
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());

                break;


        }
        

    }
}
