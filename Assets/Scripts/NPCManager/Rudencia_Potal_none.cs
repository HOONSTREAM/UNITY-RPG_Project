using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rudencia_Potal_none : MonoBehaviour
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
