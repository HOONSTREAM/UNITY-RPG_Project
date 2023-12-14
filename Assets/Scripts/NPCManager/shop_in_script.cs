using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class shop_in_script : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("루덴시안 상점");
    }


}
