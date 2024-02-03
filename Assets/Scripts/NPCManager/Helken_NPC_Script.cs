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
        gamemanager.Helken_selection.SetActive(false);
        GameObject.Find("@TalkManager").gameObject.GetComponent<TalkManager>().Reset_TalkData(); // 대화연장내용 리셋

        return;
    }

    public void Additional_Talk()
    {
       
        is_Additional_Talk_open = true; // 대화하기 버튼 승인 

        GameObject.Find("@TalkManager").gameObject.GetComponent<TalkManager>().Additional_Talk_Helken(); // 대화내용 수정
        gamemanager.Helken_selection.SetActive(false);
        gamemanager.SelectedNPC = gameObject;
        gamemanager.TalkAction();

        return;
    }

    public void Helken_Quest()
    {

    }


}
