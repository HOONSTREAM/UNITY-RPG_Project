using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���� ������ �̵��ϴ� ��ũ��Ʈ�ν�, �����ؾ��� �κ��� ���⼭ �����ؾ��մϴ�.
/// ����Ʈ �ϷῩ��, ���� �Ұ� ���θ� ���ؾ� �մϴ�.
/// </summary>
public class Deep_Place_in_Script : MonoBehaviour
{
    public GameObject savedata;

    private void Start()
    {
        savedata = GameObject.Find("Save_Data").gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Deep_Place_Scene();

        GameObject player = Managers.Game.GetPlayer();
       
        SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
    }
}
