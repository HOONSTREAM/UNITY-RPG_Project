using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shop_in_script : MonoBehaviour
{
    public GameObject savedata;
    public GameObject Rudencia_shop_NPC_Folder;
    private void Start()
    {
        Rudencia_shop_NPC_Folder = GameObject.Find("NPC Folder_Rudencia_shop").gameObject;
        Rudencia_shop_NPC_Folder.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {

        GameObject player = Managers.Game.GetPlayer();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata);
        Rudencia_shop_NPC_Folder.gameObject.SetActive(true);


        SceneManager.LoadScene("루덴시안 상점");
    }


}
