using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScene : MonoBehaviour
{

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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("�� �ε� �Ϸ�");

        // ���� �ε�� �� �÷��̾ Ȯ��
        yield return new WaitForSeconds(2.0f);

        CheckPlayer();

    }
    private void CheckPlayer()
    {
        if (Managers.Game.GetPlayer() == null) { return; }

        else
        {
            Managers.Save.LoadPlayerData(); // �÷��̾ �����ϸ�, ����� �����͸� �ε��մϴ�.  
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
