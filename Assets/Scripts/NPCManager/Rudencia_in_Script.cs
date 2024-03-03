using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Rudencia_in_Script : MonoBehaviour
{
    public GameObject Rudencia_NPC_Folder;
    public GameObject Rudencia_shop_NPC_Folder;


    private void Start()
    {
        Rudencia_NPC_Folder = GameObject.Find("NPC Folder_Rudencia").gameObject;     
        Rudencia_NPC_Folder.gameObject.SetActive(false); // �絧�þ� ���� NPC Off
    }
    private void OnTriggerEnter(Collider other)
    {
        Rudencia_NPC_Folder.gameObject.SetActive(true);

        
        if(SceneManager.GetActiveScene().name == "�絧�þ� ����")
        {
            GameObject player = Managers.Game.GetPlayer();
            player.transform.position = new Vector3(0.9320679f, 0, -47.52995f);

            PlayerController pc = player.gameObject.GetComponent<PlayerController>();
            pc.State = Define.State.Idle;

            SceneManager.LoadScene("�絧�þ�");
        }

        else if(SceneManager.GetActiveScene().name == "�絧�þ� ����")
        {
            GameObject.Find("NPC Folder_Rudencia_shop").gameObject.SetActive(false);
            GameObject player = Managers.Game.GetPlayer();
            player.transform.position = new Vector3(8.4f, 0, -43.34f);

            PlayerController pc = player.gameObject.GetComponent<PlayerController>();
            pc.State = Define.State.Idle;

            SceneManager.LoadScene("�絧�þ�");
        }
       

    }
}
