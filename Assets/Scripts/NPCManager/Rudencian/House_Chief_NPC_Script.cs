using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class House_Chief_NPC_Script : MonoBehaviour, IPointerClickHandler, TalkManager.Additional_Talking // 인터페이스
{

    [SerializeField]
    GameManager gamemanager;

    public bool is_Additional_Talk_open = false; // 대화하기 버튼을 눌렀는지에 대한 불리언 검사 

    public void OnPointerClick(PointerEventData eventData)
    {
        gamemanager.SelectedNPC = gameObject;
        gamemanager.TalkAction();
    }

    public void Taking_end_button()
    {

        gamemanager.TalkPanel.SetActive(false);
        gamemanager.selection.SetActive(false);
        GameObject.Find("@TalkManager").gameObject.GetComponent<TalkManager>().Reset_TalkData(); // 대화연장내용 리셋

        return;
    }

    public void Additional_Talk()
    {
        if (QuestDatabase.instance.QuestDB[3].npc_meet == true) { return; }

        is_Additional_Talk_open = true; // 대화하기 버튼 승인 

        GameObject.Find("@TalkManager").gameObject.GetComponent<TalkManager>().Additional_Talk_Rudencian_House_Chief(); // 대화내용 수정
        gamemanager.selection.SetActive(false);
        gamemanager.SelectedNPC = gameObject;
        gamemanager.TalkAction();

        return;
    }

   


}
