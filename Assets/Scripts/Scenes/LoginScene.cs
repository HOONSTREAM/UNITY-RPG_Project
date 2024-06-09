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
    private const float _loading_canvas_destroy_time = 3.0f;
    private const float _player_data_load_wait_time = 2.0f;

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
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Rudencian_scene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Managers.Scene_Number.Get_loading_scene());
        _loading_canvas = Managers.Resources.Instantiate("LOADING_CANVAS").gameObject;
        DontDestroyOnLoad(_loading_canvas);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // ���� �ε�� �� �÷��̾ Ȯ��
        yield return new WaitForSeconds(_player_data_load_wait_time);

        CheckPlayer();

    }
    private void CheckPlayer()
    {
        if (Managers.Game.GetPlayer() == null) { return; }

        else
        {
            Managers.Save.LoadPlayerData(); // �÷��̾ �����ϸ�, ����� �����͸� �ε��մϴ�.
                                             
            Destroy(_loading_canvas, _loading_canvas_destroy_time);
        }

    }


    public void Game_Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }


}
