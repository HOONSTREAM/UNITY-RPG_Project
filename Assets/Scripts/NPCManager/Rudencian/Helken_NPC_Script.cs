using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Helken_NPC_Script : MonoBehaviour , IPointerClickHandler, TalkManager.Additional_Talking // �������̽�
{

    [SerializeField]
    GameManager gamemanager;

    public bool is_Additional_Talk_open = false; // ��ȭ�ϱ� ��ư�� ���������� ���� �Ҹ��� �˻� 

    public void OnPointerClick(PointerEventData eventData)
    {
        gamemanager.SelectedNPC = gameObject;
        gamemanager.TalkAction();
    }

    public void Helken_Taking_end_button()
    {

        gamemanager.TalkPanel.SetActive(false);
        gamemanager.selection.SetActive(false);
        GameObject.Find("@TalkManager").gameObject.GetComponent<TalkManager>().Reset_TalkData(); // ��ȭ���峻�� ����

        return;
    }

    public void Additional_Talk()
    {
        if (QuestDatabase.instance.QuestDB[1].is_complete == false) { return; } // �����Ӿ�ü������ ����Ʈ �̿Ϸ��ϰ�� ��ȭ�������
        if (QuestDatabase.instance.QuestDB[2].npc_meet == true) { return; }

        is_Additional_Talk_open = true; // ��ȭ�ϱ� ��ư ���� 

        GameObject.Find("@TalkManager").gameObject.GetComponent<TalkManager>().Additional_Talk_Helken(); // ��ȭ���� ����
        gamemanager.selection.SetActive(false);
        gamemanager.SelectedNPC = gameObject;
        gamemanager.TalkAction();

        return;
    }

    public void Helken_Quest()
    {

    }


}
