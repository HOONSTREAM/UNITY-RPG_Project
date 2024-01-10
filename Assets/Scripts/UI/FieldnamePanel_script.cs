using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FieldnamePanel_script : MonoBehaviour
{
    private Scene scene;
    public TextMeshProUGUI mapname;

   
    void Awake()
    {
       scene = SceneManager.GetActiveScene();
       OnupdateScenename(); 
    }
    
   
   private void OnupdateScenename()
    {
        mapname.text = scene.name;

        return;
    }
}
