using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Helken_NPC_Script : MonoBehaviour , IPointerClickHandler, TalkManager.Additional_Talking // 인터페이스
{

    [SerializeField]
    GameManager gamemanager;

    

    public void OnPointerClick(PointerEventData eventData)
    {
        gamemanager.SelectedNPC = gameObject;
        gamemanager.TalkAction();
    }

    public void Helken_Taking_end_button()
    {
        gamemanager.TalkPanel.SetActive(false);
        gamemanager.Helken_selection.SetActive(false);

        return;
    }

    public void Additional_Talk()
    {
        throw new System.NotImplementedException();
    }
}
