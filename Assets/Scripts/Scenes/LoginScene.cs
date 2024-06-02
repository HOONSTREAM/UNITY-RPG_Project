using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScene : MonoBehaviour
{

    private void Start()
    {
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Start_Scene();
        GameObject Init_player = Managers.Game.GetPlayer();
        Camera.main.gameObject.GetAddComponent<CameraController>().SetPlayer(Init_player);
        gameObject.GetAddComponent<CursorController>();
       
    }

    public void New_Game_Start()
    {
        
        SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
        
    }


    public void Load_Game()
    {
        
        SceneManager.LoadScene(1);
    }

   
    public void Game_Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }


}
