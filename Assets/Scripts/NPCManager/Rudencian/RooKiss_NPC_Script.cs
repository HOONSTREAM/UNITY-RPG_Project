using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RooKiss_NPC_Script : MonoBehaviour, IPointerClickHandler, TalkManager.Additional_Talking // �������̽�
{
    [SerializeField]
    GameManager gamemanager;

    public bool is_Additional_Talk_open = false; // ��ȭ�ϱ� ��ư�� ���������� ���� �Ҹ��� �˻� 

    public void OnPointerClick(PointerEventData eventData)
    {
        gamemanager.SelectedNPC = gameObject;
        gamemanager.TalkAction();
    }

    public void Taking_end_button()
    {

        gamemanager.TalkPanel.SetActive(false);
        gamemanager.selection.SetActive(false);
        GameObject.Find("@TalkManager").gameObject.GetComponent<TalkManager>().Reset_TalkData(); // ��ȭ���峻�� ����

        return;
    }

    public void Additional_Talk()
    {
        if (QuestDatabase.instance.QuestDB[3].is_complete == false) { return; }
        if (QuestDatabase.instance.QuestDB[4].npc_meet == true) { return; }
        if (QuestDatabase.instance.QuestDB[4].is_complete == true) { return; } // ��ġ�� ��� ����Ʈ ����

        is_Additional_Talk_open = true; // ��ȭ�ϱ� ��ư ���� 

        GameObject.Find("@TalkManager").gameObject.GetComponent<TalkManager>().Addtional_Talk_Rudencian_training_officer_RooKiss(); // ��ȭ���� ����
        gamemanager.selection.SetActive(false);
        gamemanager.SelectedNPC = gameObject;
        gamemanager.TalkAction();

        return;
    }

    public void RooKiss_Quest()
    {

    }


}
