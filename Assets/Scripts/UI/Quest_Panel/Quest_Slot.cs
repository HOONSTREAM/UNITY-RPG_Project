using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class Quest_Slot : MonoBehaviour , IPointerUpHandler
{
    public int slotnum;

    [SerializeField]
    public Quest quest;
    [SerializeField]
    private Image quest_icon;
    [SerializeField]
    private TextMeshProUGUI quest_name;
    [SerializeField]
    private TextMeshProUGUI main_or_sub;
    [SerializeField]
    private TextMeshProUGUI explaination_quest;
    [SerializeField]
    private TextMeshProUGUI summing_up_explaination;
    [SerializeField]
    private TextMeshProUGUI reward_gold;
    [SerializeField]
    private TextMeshProUGUI reward_exp;



    private void Start()
    {
      
    }
    public void OnPointerUp(PointerEventData eventData)
    {

        explaination_quest.text = quest.Description;
        summing_up_explaination.text = quest.summing_up_Description;
        OnUpdate_Quest_Progress();
    }

    private void OnUpdate_Quest_Progress()
    {

        switch (quest.Quest_ID)
        {
            case 1:
                reward_gold.text = QuestDatabase.instance.QuestDB[0].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[0].num_2.ToString();
                summing_up_explaination.text = $"레드슬라임 : ({quest.monster_counter} / {Managers.Quest_Completion.Get_Slime_Hunting_Quest_Complete_amount()})";
               
                UpdateSlotUI();

                break;

            case 2:
                int slime_etc_item_amount = 0;

                reward_gold.text = QuestDatabase.instance.QuestDB[1].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[1].num_2.ToString();
                for (int i = 0; i < PlayerInventory.Instance.player_items.Count; i++)
                {
                    if (PlayerInventory.Instance.player_items[i].ItemID == Managers.Quest_Completion.Get_Slime_Drop_item_ID())
                    {
                        slime_etc_item_amount = PlayerInventory.Instance.player_items[i].amount;
                    }
                }

                summing_up_explaination.text = $"슬라임액체 : ({slime_etc_item_amount} / {Managers.Quest_Completion.Get_Slime_collecting_item_amount()})";
               

                UpdateSlotUI();

                break;

            case 3:

                reward_gold.text = QuestDatabase.instance.QuestDB[2].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[2].num_2.ToString();



                summing_up_explaination.text = $"기사 헬켄을 찾아 대화를 한다.";

                UpdateSlotUI();


                break;

            case 4:
                reward_gold.text = QuestDatabase.instance.QuestDB[3].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[3].num_2.ToString();



                summing_up_explaination.text = $"촌장 월터를 찾아가 대화를 한다.";

                UpdateSlotUI();

                break;

            case 5:

                reward_gold.text = QuestDatabase.instance.QuestDB[4].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[4].num_2.ToString();



                summing_up_explaination.text = $"수련관 루키스를 찾아가 대화를 한다.";

                UpdateSlotUI();

                break;

            case 6:

                reward_gold.text = QuestDatabase.instance.QuestDB[5].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[5].num_2.ToString();



                summing_up_explaination.text = $"펀치맨 : ({quest.monster_counter} / {Managers.Quest_Completion.Get_Punch_man_Hunting_Quest_Complete_amount()})\n";


                UpdateSlotUI();
                break;


            case 7:

                reward_gold.text = QuestDatabase.instance.QuestDB[6].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[6].num_2.ToString();



                summing_up_explaination.text = "수련관 루키스와 두번째 대화";


                UpdateSlotUI();

                break;

            case 8:

                reward_gold.text = QuestDatabase.instance.QuestDB[7].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[7].num_2.ToString();

                summing_up_explaination.text = "[겉갑옷, 투구, 신발] 3개의 방어구를 구입하여 장착하기";

                UpdateSlotUI();

                break;

            case 9:

                reward_gold.text = QuestDatabase.instance.QuestDB[8].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[8].num_2.ToString();

                summing_up_explaination.text = "무기종류 1개 어빌리티 5.00 이상 달성";

                UpdateSlotUI();

                break;

        }
    }

    public void UpdateSlotUI()
    {
        quest_icon.sprite = quest.quest_image;
        quest_name.text = quest.quest_name;
        quest_icon.gameObject.SetActive(true);

        if (quest.questtype == QuestType.Main)
        {
            main_or_sub.text = "<메인퀘스트>";

        }
        else if (quest.questtype == QuestType.Sub)
        {
            main_or_sub.text = "<서브퀘스트>";

        }

        else if ((quest.questtype == QuestType.Repetition))
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

}
