using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Epilenia_in_Script : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.EpileniaMainScene;

        GameObject player = Managers.Game.GetPlayer();
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();

        switch (SceneManager.GetActiveScene().name)
        {

            case "에필레니아 은행":

                player.transform.position = new Vector3(384.76f, 9.1659f, 399.11f);
                pc.State = Define.State.Idle;

                SceneManager.LoadScene(Managers.Scene_Number.LoadingScene);
                break;


        }


    }
}
