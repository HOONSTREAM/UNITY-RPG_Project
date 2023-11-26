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
       OnupdateScenename(); //TODO :  맵이동이 이뤄지면 델리게이트로 이름 변경하자
    }
    
   
   private void OnupdateScenename()
    {
        mapname.text = scene.name;

        return;
    }
}
