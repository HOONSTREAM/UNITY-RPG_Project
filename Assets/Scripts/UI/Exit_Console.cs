using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Exit_Console : MonoBehaviour
{
    public GameObject ExitConsole;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.Sound.Play("Coin");
            if (ExitConsole.gameObject.activeSelf)
            {
                ExitConsole.gameObject.SetActive(false);
                return;
            }

            ExitConsole.gameObject.SetActive(true);
            
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void NotExitGame()
    {
        Managers.Sound.Play("Coin");
        ExitConsole.gameObject.SetActive(false);
    }
}
