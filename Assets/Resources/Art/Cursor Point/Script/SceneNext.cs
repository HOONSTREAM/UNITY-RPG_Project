using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNext : MonoBehaviour {

    public void GoToLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }   
	

}