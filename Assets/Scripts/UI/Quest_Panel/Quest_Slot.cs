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
    public TextMeshProUGUI summing_up_explaination;

    public TextMeshProUGUI reward_gold;
    public TextMeshProUGUI reward_exp;
    
   

    public void UpdateSlotUI()
    {
        quest_icon.sprite = quest.quest_image;
        quest_name.text = quest.quest_name;       
        quest_icon.gameObject.SetActive(true);

        if(quest.questtype == QuestType.Main)
        {
            main_or_sub.text = "<메인퀘스트>";

        }
        else if (quest.questtype == QuestType.Sub)
        {
            main_or_sub.text = "<서브퀘스트>";

        }

        else if((quest.questtype == QuestType.Repetition))
        {
            main_or_sub.text = "<반복퀘스트>";
        }

        else 
        {
            main_or_sub.text = " ";
        }

    
        return;
    }
    public void RemoveSlot()
    {
        quest = null;
        quest_icon.gameObject.SetActive(false); //초기화 (아이콘 표시 안함)
        quest_name.text = "";
        main_or_sub.text = " ";
        explaination_quest.text = "";
        summing_up_explaination.text = " ";
        reward_gold.text = "0";
        reward_exp.text = "0";


        return;
    }


    public void OnPointerUp(PointerEventData eventData)
    {

        //TODO : 버튼 누르면 퀘스트 상세내용 나타내기 
        Debug.Log("버튼을 눌렀습니다.");
        explaination_quest.text = quest.Description;
        summing_up_explaination.text = quest.summing_up_Description;
        

        switch (quest.Quest_ID)
        {
            case 1:
                reward_gold.text = QuestDatabase.instance.QuestDB[0].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[0].num_2.ToString();
                summing_up_explaination.text = $"레드슬라임 : ({quest.monster_counter} / 2)";
                UpdateSlotUI();

                break;

        }

    }

}
