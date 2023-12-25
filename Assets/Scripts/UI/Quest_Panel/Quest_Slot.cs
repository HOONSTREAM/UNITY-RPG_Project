using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class Quest_Slot : MonoBehaviour , IPointerUpHandler
{
    public int slotnum;

    public Quest quest;
    public Image quest_icon;
    public TextMeshProUGUI quest_name;
    public TextMeshProUGUI main_or_sub;
    public TextMeshProUGUI explaination_quest;
    
   

    public void UpdateSlotUI()
    {
        quest_icon.sprite = quest.quest_image;
        quest_name.text = quest.quest_name;       
        quest_icon.gameObject.SetActive(true);

        if(quest.questtype == QuestType.Main)
        {
            main_or_sub.text = "<��������Ʈ>";

        }
        else if (quest.questtype == QuestType.Sub)
        {
            main_or_sub.text = "<��������Ʈ>";

        }

        else
        {
            main_or_sub.text = "<�ݺ�����Ʈ>";
        }
        return;
    }
    public void RemoveSlot()
    {
        quest = null;
        quest_icon.gameObject.SetActive(false); //�ʱ�ȭ (������ ǥ�� ����)
        quest_name.text = "";

        return;
    }


    public void OnPointerUp(PointerEventData eventData)
    {

        //TODO : ��ư ������ �߻��ϴ� �� ����
        Debug.Log("��ư�� �������ϴ�.");
        explaination_quest.text = quest.Description;
    }
}
