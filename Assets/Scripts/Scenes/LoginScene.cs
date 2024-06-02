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
        gameObject.GetAddComponent<CursorController>();
       
    }

    public void New_Game_Start()
    {
        
        SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
        
    }


    public void Load_Game()
    {       
        SceneManager.LoadScene(1);

        StartCoroutine(wait_Load_PlayerData());
    }

   
    IEnumerator wait_Load_PlayerData()
    {
        yield return new WaitForSeconds(0.5f);

        if (Managers.Game.GetPlayer() == null)
        {
            Debug.Log("�÷��̾ ����");
        }
        else
        {
            Debug.Log("�÷��̾� �ε� ������ �޼��� ���� ��");
            Managers.Save.LoadPlayerData();
            Debug.Log("�÷��̾� �ε� ������ �޼��� ȣ�� �Ϸ�");
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
