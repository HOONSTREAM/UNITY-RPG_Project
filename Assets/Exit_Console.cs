using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Exit_Console : MonoBehaviour
{
    public GameObject ExitConsole;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.Sound.Play("Coin");
            ExitConsole.gameObject.SetActive(true);
           
        }
    }
}
