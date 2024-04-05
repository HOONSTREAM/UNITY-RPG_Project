using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inn_NPC_Script_Rudencian : MonoBehaviour, IPointerClickHandler, TalkManager.Additional_Talking // 인터페이스
{
    [SerializeField]
    GameManager gamemanager;

    void TalkManager.Additional_Talking.Additional_Talk()
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gamemanager.SelectedNPC = gameObject;
        gamemanager.TalkAction();


    }
}
