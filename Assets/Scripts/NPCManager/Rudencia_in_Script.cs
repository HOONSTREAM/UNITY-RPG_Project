using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Rudencia_in_Script : MonoBehaviour
{
    //�絧�þ� ���� NPC �� SetActive�� ���⼭ ���� �����մϴ�.

    public GameObject Rudencian_NPC_Folder;


    private void Start()
    {
        Rudencian_NPC_Folder = GameObject.Find("NPC Folder_Rudencia").gameObject;
        Rudencian_NPC_Folder.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        Rudencian_NPC_Folder.gameObject.SetActive(true); //�絧�þ� ����, �絧�þ�NPC ON

        GameObject player = Managers.Game.GetPlayer();
        PlayerController pc = player.gameObject.GetComponent<PlayerController>();

        switch (SceneManager.GetActiveScene().name)
        {
            case "�絧�þ� ����":
               
                player.transform.position = new Vector3(0.9320679f, 0, -47.52995f);               
                pc.State = Define.State.Idle;
                GameObject.Find("NPC Folder_Rudencia_south").gameObject.SetActive(false);// �絧�þȳ��� NPC Off
                SceneManager.LoadScene("�絧�þ�");
                break;

            case "�絧�þ� ����":
                GameObject.Find("NPC Folder_Rudencia_shop").gameObject.SetActive(false);// �絧�þȻ��� NPC Off
                player.transform.position = new Vector3(8.4f, 0, -43.34f);              
                pc.State = Define.State.Idle;

                SceneManager.LoadScene("�絧�þ�");
                break;

            case "�絧�þ� ����":
                GameObject.Find("NPC Folder_Rudencia_bank").gameObject.SetActive(false); //�絧�þ����� NPC Off
                player.transform.position = new Vector3(-8.585f, 0, -20.935f);            
                pc.State = Define.State.Idle;

                SceneManager.LoadScene("�絧�þ�");
                break;

            case "�絧�þ� ����":

                GameObject.Find("NPC Folder_Rudencia_Inn").gameObject.SetActive(false); //�絧�þȿ��� NPC Off
                player.transform.position = new Vector3(7.54f, 0, -10.74f);
                player.transform.rotation = new Quaternion(0, 274.886f, 0, 0);
                pc.State = Define.State.Idle;

                SceneManager.LoadScene("�絧�þ�");

                break;

        }
        

    }
}
