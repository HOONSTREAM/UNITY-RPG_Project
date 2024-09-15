using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 보스 맵으로 이동하는 스크립트로써, 제어해야할 부분은 여기서 제어해야합니다.
/// 퀘스트 완료여부, 저장 불가 여부를 택해야 합니다.
/// </summary>
public class Deep_Place_in_Script : MonoBehaviour
{
    public GameObject savedata;
    private readonly int KILL_KING_SLIME_QUEST_ID = 11;

    private void Start()
    {
        savedata = GameObject.Find("Save_Data").gameObject;
    }


    private void OnTriggerEnter(Collider other)
    {
        foreach(Quest quest in Player_Quest.Instance.PlayerQuest)
        {
            if(quest.Quest_ID == KILL_KING_SLIME_QUEST_ID)
            {
                LoadingScene.NEXT_SCENE_NUMBER = Managers.Scene_Number.Get_Deep_Place_Scene();

                GameObject player = Managers.Game.GetPlayer();

                SceneManager.LoadScene(Managers.Scene_Number.Get_loading_scene());
            }

            else if(quest.Quest_ID != KILL_KING_SLIME_QUEST_ID)
            {
                Print_Info_Text.Instance.PrintUserText("들어가기엔 아직 능력이 부족합니다.");
            }
        }

        
        return;
        
    }
}
