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
    private readonly int KILL_KING_SLIME_QUEST_ID = 10;

    private void Start()
    {
        savedata = GameObject.Find("Save_Data").gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        foreach(Quest quest in Player_Quest.Instance.PlayerQuest)
        {
            if(quest.Quest_ID != KILL_KING_SLIME_QUEST_ID)
            {
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Deep_Place_Scene();

                GameObject player = Managers.Game.GetPlayer();

                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
            }
        }

        Print_Info_Text.Instance.PrintUserText("���⿣ ���� �ɷ��� �����մϴ�.");
        
    }
}
