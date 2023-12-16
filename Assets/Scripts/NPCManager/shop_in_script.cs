using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shop_in_script : MonoBehaviour
{
    public GameObject savedata_1;
    public GameObject savedata_2;
    public GameObject savedata_3;
    public GameObject savedata_4;
    public GameObject savedata_5;


    private void OnTriggerEnter(Collider other)
    {
        GameObject player = Managers.Game.GetPlayer();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata_1);
        DontDestroyOnLoad(savedata_2);
        DontDestroyOnLoad(savedata_3);
        DontDestroyOnLoad(savedata_4);
        DontDestroyOnLoad(savedata_5);
        SceneManager.LoadScene("루덴시안 상점");
    }


}
