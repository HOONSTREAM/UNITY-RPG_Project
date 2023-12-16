using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Rudencia_in_Script : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject player = Managers.Game.GetPlayer();
        player.transform.position = new Vector3(8.4f, 0, -43.34f);
       
        SceneManager.LoadScene("·çµ§½Ã¾È");
    }
}
