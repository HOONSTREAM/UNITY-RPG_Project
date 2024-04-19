using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Rudencia_in_Script : MonoBehaviour
{
    
    private const int RUDENCIAN_SCENE_NUMBER = 1;


    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = RUDENCIAN_SCENE_NUMBER;

        GameObject player = Managers.Game.GetPlayer();
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();

        switch (SceneManager.GetActiveScene().name)
        {

            case "루덴시안 보석상":

                player.transform.position = new Vector3(-1.043379f, 0, -2.187184f);
                pc.State = Define.State.Idle;      
                
                SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
                break;

            case "루덴시안 남쪽":
               
                player.transform.position = new Vector3(0.9320679f, 0, -47.52995f);               
                pc.State = Define.State.Idle;
            
                SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
                break;

            case "루덴시안 상점":
            
                player.transform.position = new Vector3(8.4f, 0, -43.34f);              
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
                break;

            case "루덴시안 은행":
               
                player.transform.position = new Vector3(-8.585f, 0, -20.935f);            
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
                break;

            case "루덴시안 여관":

             
                player.transform.position = new Vector3(7.54f, 0, -10.74f);
                player.transform.rotation = new Quaternion(0, 274.886f, 0, 0);
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);

                break;

        }
        

    }
}
