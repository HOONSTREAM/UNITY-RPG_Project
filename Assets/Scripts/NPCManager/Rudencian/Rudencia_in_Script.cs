using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Rudencia_in_Script : MonoBehaviour
{
    //루덴시안 마을 NPC 의 SetActive는 여기서 전부 제어합니다.

    public GameObject Rudencian_NPC_Folder;

    private const int RUDENCIAN_SCENE_NUMBER = 1;


    private void Start()
    {
        //문제점, 루덴시안에서 넘어오면 적용이되지만 루덴시안 2에서 넘어올때 적용이 안된다.
        Rudencian_NPC_Folder = GameObject.Find("NPC Folder_Rudencia").gameObject;
        Rudencian_NPC_Folder.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = RUDENCIAN_SCENE_NUMBER;

        Rudencian_NPC_Folder.gameObject.SetActive(true); //루덴시안 복귀, 루덴시안NPC ON

        GameObject player = Managers.Game.GetPlayer();
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();

        switch (SceneManager.GetActiveScene().name)
        {

            case "루덴시안 보석상":

                player.transform.position = new Vector3(-1.043379f, 0, -2.187184f);
                pc.State = Define.State.Idle;
                GameObject.Find("NPC Folder_Rudencia_jewel").gameObject.SetActive(false);// 루덴시안남쪽 NPC Off
                SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
                break;

            case "루덴시안 남쪽":
               
                player.transform.position = new Vector3(0.9320679f, 0, -47.52995f);               
                pc.State = Define.State.Idle;
                GameObject.Find("NPC Folder_Rudencia_south").gameObject.SetActive(false);// 루덴시안남쪽 NPC Off
                SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
                break;

            case "루덴시안 상점":
                GameObject.Find("NPC Folder_Rudencia_shop").gameObject.SetActive(false);// 루덴시안상점 NPC Off
                player.transform.position = new Vector3(8.4f, 0, -43.34f);              
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
                break;

            case "루덴시안 은행":
                GameObject.Find("NPC Folder_Rudencia_bank").gameObject.SetActive(false); //루덴시안은행 NPC Off
                player.transform.position = new Vector3(-8.585f, 0, -20.935f);            
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);
                break;

            case "루덴시안 여관":

                GameObject.Find("NPC Folder_Rudencia_Inn").gameObject.SetActive(false); //루덴시안여관 NPC Off
                player.transform.position = new Vector3(7.54f, 0, -10.74f);
                player.transform.rotation = new Quaternion(0, 274.886f, 0, 0);
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(LoadingScene.LOADING_SCENE_NUMBER);

                break;

        }
        

    }
}
