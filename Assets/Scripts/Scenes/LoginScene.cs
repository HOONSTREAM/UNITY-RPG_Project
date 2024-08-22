using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScene : MonoBehaviour
{
    [SerializeField]
    private GameObject _loading_canvas;

    private const float LOADING_CANVAS_DESTROY_TIME = 3.0f;
    private const float PLAYER_DATA_LOAD_WAIT_TIME = 2.0f;
    private const string LOADING_CANVAS_PREPAB_PATH = "LOADING_CANVAS";

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        InitializeScene();
    }

    private void InitializeScene()
    {
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Start_Scene();
        gameObject.AddComponent<CursorController>();
    }

    public void New_Game_Start()
    {      
      SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());       
    }


    public void Load_Game()
    {

    StartCoroutine(LoadScene_And_CheckPlayer());

    }
    private IEnumerator LoadScene_And_CheckPlayer()
    {
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_RooKiss_Room_Scene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Managers.Scene_Number.Get_loading_scene());
        _loading_canvas = Managers.Resources.Instantiate(LOADING_CANVAS_PREPAB_PATH).gameObject;
        DontDestroyOnLoad(_loading_canvas);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 씬이 로드된 후 플레이어를 확인
        yield return new WaitForSeconds(PLAYER_DATA_LOAD_WAIT_TIME);

        CheckPlayer();

        yield return new WaitForSeconds(LOADING_CANVAS_DESTROY_TIME);
       
        Managers.Save.ShowLoadCompleteAlarm();

    }
    private void CheckPlayer()
    {
        if (Managers.Game.GetPlayer() == null) { return; }

        else
        {
            Managers.Save.LoadPlayerData(); // 플레이어가 존재하면, 저장된 데이터를 로드합니다.
                                             
            Destroy(_loading_canvas, LOADING_CANVAS_DESTROY_TIME);
            
        }

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
