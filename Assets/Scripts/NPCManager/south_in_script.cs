using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class south_in_script : MonoBehaviour
{
    public GameObject savedata;
    
    private void Start()
    {
     
    }
    private void OnTriggerEnter(Collider other)
    {

        GameObject player = Managers.Game.GetPlayer();
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(savedata);
       


        SceneManager.LoadScene("�絧�þ� ����");
    }
}
