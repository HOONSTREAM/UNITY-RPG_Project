using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Helken_NPC_Script : MonoBehaviour , IPointerClickHandler, TalkManager.Additional_Talking // 인터페이스
{

    [SerializeField]
    GameManager gamemanager;

    public bool is_Additional_Talk_open = false; // 대화하기 버튼을 눌렀는지에 대한 불리언 검사 

    public void OnPointerClick(PointerEventData eventData)
    {
        gamemanager.SelectedNPC = gameObject;
        gamemanager.TalkAction();
    }

    public void Helken_Taking_end_button()
    {

        gamemanager.TalkPanel.SetActive(false);
        gamemanager.selection.SetActive(false);
        GameObject.Find("@TalkManager").gameObject.GetComponent<TalkManager>().Reset_TalkData(); // 대화연장내용 리셋

        return;
    }

    public void Additional_Talk()
    {
        if (QuestDatabase.instance.QuestDB[1].is_complete == false) { return; } // 슬라임액체모으기 퀘스트 미완료일경우 대화진행안함
        if (QuestDatabase.instance.QuestDB[2].npc_meet == true) { return; }

        is_Additional_Talk_open = true; // 대화하기 버튼 승인 

        GameObject.Find("@TalkManager").gameObject.GetComponent<TalkManager>().Additional_Talk_Helken(); // 대화내용 수정
        gamemanager.selection.SetActive(false);
        gamemanager.SelectedNPC = gameObject;
        gamemanager.TalkAction();

        Managers.Quest_Completion.Quest_Complete_Alarm();

        return;
    }

    public void Helken_Quest()
    {

    }


}
