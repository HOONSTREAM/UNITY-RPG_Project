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
                summing_up_explaination.text = $"���彽���� : ({quest.monster_counter} / {Managers.Quest_Completion.Get_Slime_Hunting_Quest_Complete_amount()})";
               
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

                summing_up_explaination.text = $"�����Ӿ�ü : ({slime_etc_item_amount} / {Managers.Quest_Completion.Get_Slime_collecting_item_amount()})";
               

                UpdateSlotUI();

                break;

            case 3:

                reward_gold.text = QuestDatabase.instance.QuestDB[2].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[2].num_2.ToString();



                summing_up_explaination.text = $"��� ������ ã�� ��ȭ�� �Ѵ�.";

                UpdateSlotUI();


                break;

            case 4:
                reward_gold.text = QuestDatabase.instance.QuestDB[3].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[3].num_2.ToString();



                summing_up_explaination.text = $"���� ���͸� ã�ư� ��ȭ�� �Ѵ�.";

                UpdateSlotUI();

                break;

            case 5:

                reward_gold.text = QuestDatabase.instance.QuestDB[4].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[4].num_2.ToString();



                summing_up_explaination.text = $"���ð� ��Ű���� ã�ư� ��ȭ�� �Ѵ�.";

                UpdateSlotUI();

                break;

            case 6:

                reward_gold.text = QuestDatabase.instance.QuestDB[5].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[5].num_2.ToString();



                summing_up_explaination.text = $"��ġ�� : ({quest.monster_counter} / {Managers.Quest_Completion.Get_Punch_man_Hunting_Quest_Complete_amount()})\n";


                UpdateSlotUI();
                break;


            case 7:

                reward_gold.text = QuestDatabase.instance.QuestDB[6].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[6].num_2.ToString();



                summing_up_explaination.text = "���ð� ��Ű���� �ι�° ��ȭ";


                UpdateSlotUI();

                break;

            case 8:

                reward_gold.text = QuestDatabase.instance.QuestDB[7].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[7].num_2.ToString();

                summing_up_explaination.text = "[�Ѱ���, ����, �Ź�] 3���� ���� �����Ͽ� �����ϱ�";

                UpdateSlotUI();

                break;

            case 9:

                reward_gold.text = QuestDatabase.instance.QuestDB[8].num_1.ToString();
                reward_exp.text = QuestDatabase.instance.QuestDB[8].num_2.ToString();

                summing_up_explaination.text = "�������� 1�� �����Ƽ 5.00 �̻� �޼�";

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
            main_or_sub.text = "<��������Ʈ>";

        }
        else if (quest.questtype == QuestType.Sub)
        {
            main_or_sub.text = "<��������Ʈ>";

        }

        else if ((quest.questtype == QuestType.Repetition))
        {
            main_or_sub.text = "<�ݺ�����Ʈ>";
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
        quest_icon.gameObject.SetActive(false); //�ʱ�ȭ (������ ǥ�� ����)
        quest_name.text = "";
        main_or_sub.text = " ";
        explaination_quest.text = "";
        summing_up_explaination.text = " ";
        reward_gold.text = "0";
        reward_exp.text = "0";


        return;
    }

}
