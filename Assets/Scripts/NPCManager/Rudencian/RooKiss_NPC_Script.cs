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
        
        if (QuestDatabase.instance.QuestDB[5].is_complete == true && !QuestDatabase.instance.QuestDB[6].is_complete)
        {
            is_Additional_Talk_open = true; // ��ȭ�ϱ� ��ư ���� 

            GameObject.Find("@TalkManager").gameObject.GetComponent<TalkManager>().Additional_Talk_Rudencian_training_officer_RooKiss_Quest2(); // ��ȭ���� ����
            gamemanager.selection.SetActive(false);            
            gamemanager.SelectedNPC = gameObject;
            gamemanager.TalkAction();

            Managers.Quest_Completion.Quest_Complete_Alarm();
        }

        else if (QuestDatabase.instance.QuestDB[8].is_complete == true && !QuestDatabase.instance.QuestDB[9].is_complete)
        {
            is_Additional_Talk_open = true; // ��ȭ�ϱ� ��ư ���� 

            GameObject.Find("@TalkManager").gameObject.GetComponent<TalkManager>().Additional_Talk_Rudencian_training_officer_RooKiss_Quest3(); // ��ȭ���� ����
            gamemanager.selection.SetActive(false);
            gamemanager.SelectedNPC = gameObject;
            gamemanager.TalkAction();

            Managers.Quest_Completion.Quest_Complete_Alarm();
        }

        else if (QuestDatabase.instance.QuestDB[10].is_complete == true && !QuestDatabase.instance.QuestDB[11].is_complete)
        {
            is_Additional_Talk_open = true; // ��ȭ�ϱ� ��ư ���� 

            GameObject.Find("@TalkManager").gameObject.GetComponent<TalkManager>().Additional_Talk_Rudencian_training_officer_RooKiss_Quest_Last(); // ��ȭ���� ����
            gamemanager.selection.SetActive(false);
            gamemanager.SelectedNPC = gameObject;
            gamemanager.TalkAction();

            Managers.Quest_Completion.Quest_Complete_Alarm();
        }

        else
        {
            is_Additional_Talk_open = true; // ��ȭ�ϱ� ��ư ���� 

            GameObject.Find("@TalkManager").gameObject.GetComponent<TalkManager>().Additional_Talk_Rudencian_training_officer_RooKiss(); // ��ȭ���� ����
            gamemanager.selection.SetActive(false);
            gamemanager.SelectedNPC = gameObject;
            gamemanager.TalkAction();

            Managers.Quest_Completion.Quest_Complete_Alarm();
        }
       
        return;

    }


}
