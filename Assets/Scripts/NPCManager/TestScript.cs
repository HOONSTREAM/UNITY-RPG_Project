using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField]
    GameManager gamemanager;
    private void OnTriggerEnter(Collider other)
    {
        gamemanager.SelectedNPC = gameObject;
        gamemanager.TalkAction();
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
} 
