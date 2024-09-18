using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Quest_Script : MonoBehaviour
{
    [SerializeField]
    private GameObject Quest_Panel;
    [SerializeField]
    private GameObject Quest_CANVAS;

    Player_Quest quest; //�÷��̾� ����Ʈâ ����
    PlayerStat stat; //�÷��̾� ���� ���� (��������Ʈ)



    public Quest_Slot[] quest_slots;
    public Transform quest_slotHolder;
    public Slot[] Player_slots;
    public Transform player_slotHolder;
    

    public bool activequestpanel = false;

    public GameObject summary_panel_quest;
    public TextMeshProUGUI Quest_summary_name;
    public TextMeshProUGUI Quest_summary_type;
    public TextMeshProUGUI Quest_summary;
   

    void Start()
    {
        stat = GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
        quest = Player_Quest.Instance;
        quest_slots = quest_slotHolder.GetComponentsInChildren<Quest_Slot>();
        Player_slots = player_slotHolder.GetComponentsInChildren<Slot>();

        Quest_Panel.SetActive(activequestpanel);
        Player_Quest.Instance.onChangequest += RedrawSlotUI;  // Invoke �Լ� ��� �̺�Ʈ �߻����� �Լ� ȣ��
        Managers.UI.SetCanvas(Quest_CANVAS, true);       
        UI_Base.BindEvent(Quest_Panel, (PointerEventData data) => { Quest_Panel.transform.position = data.position; }, Define.UIEvent.Drag);  //�κ��丮 �巡�� �����ϵ��� �ϴ� �̺�Ʈ
        RedrawSlotUI();


        Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[0]);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Quest_Panel_Open_Method();
        }

        Onupdate_UserInterface_Summary_Quest_Panel();
    }
    void RedrawSlotUI()
    {
        for (int i = 0; i < quest_slots.Length; i++)
        {
            quest_slots[i].slotnum = i;
        }

        for (int i = 0; i < quest_slots.Length; i++) 
        {
            quest_slots[i].RemoveSlot();
        }

        for (int i = 0; i < quest.PlayerQuest.Count; i++)  
        {
            quest_slots[i].quest = quest.PlayerQuest[i];
            quest_slots[i].UpdateSlotUI();

        }
    }
    public void Quest_Complete_Button_Func()
    {
        for (int i = 0; i < Player_Quest.Instance.PlayerQuest.Count; i++)
        {
            switch (Player_Quest.Instance.PlayerQuest[i].Quest_ID)
            {
                case 1:
                    Managers.Quest_Completion.Kill_Slime_Quest_Conditions_for_completion();
                    break;
                case 2: 
                    Managers.Quest_Completion.Slime_collecting_drop_item_quest_Conditions_for_completion();                
                  break;
                case 3: 
                    Managers.Quest_Completion.HelKen_Meet_Quest_Conditions_for_completion();
                    break;
                case 4:
                    Managers.Quest_Completion.Rudencian_Chief_Meet_Quest_Conditions_for_completion();
                    break;
                case 5:
                    Managers.Quest_Completion.Rudencian_Training_officer_Rookiss_NPC_Meet_Quest_Conditions_for_completion();
                    break;
                case 6:
                    Managers.Quest_Completion.Kill_Punch_man_Quest_Conditions_for_completion();
                    break;
                case 7:
                    Managers.Quest_Completion.Rookiss_NPC_Meet_Quest_Conditions_for_completion_Second();
                    break;
                case 8:
                    Managers.Quest_Completion.First_Equipment_Wear_Quest();
                    break;
                case 9:
                    Managers.Quest_Completion.First_Ability_Training_Quest();
                    break;
                case 10:
                    Managers.Quest_Completion.Rookiss_NPC_Meet_Quest_Conditions_for_completion_Third();
                    break;
                case 11:
                    Managers.Quest_Completion.Kill_king_slime_condition_for_completion();
                    break;
                case 12:
                    Managers.Quest_Completion.Rookiss_NPC_Meet_Quest_Conditions_for_completion_Last();
                    break;

            }
        }
    }

    public void Quest_Panel_Open_Method()
    {
        activequestpanel = !activequestpanel;
        Quest_Panel.SetActive(activequestpanel);
        Managers.UI.SetCanvas(Quest_CANVAS, true); // ĵ���� SortOrder ������ ������ ���� ������. (���� �������� �������� ���� ����)
        Managers.Sound.Play("Inven_Open");

        return;

    }
    public void Exit_Button()
    {
        if (Quest_Panel.activeSelf)
        {
            Quest_Panel.SetActive(false);
            Managers.Sound.Play("Inven_Open");

        }

        Player_Quest.Instance.onChangequest.Invoke();


        return;

    }

    private void Onupdate_UserInterface_Summary_Quest_Panel()
    {
       
        if (Player_Quest.Instance.PlayerQuest.Count == 0)
        {
            Quest_summary_name.text = "����Ʈ ����";
            Quest_summary_type.text = "";
            Quest_summary.text = "";
        }

        foreach (var quest in Player_Quest.Instance.PlayerQuest)
        {
            if (quest.questtype == QuestType.Main)
            {
                Quest_summary_name.text = quest.quest_name;
                Quest_summary_type.text = quest.questtype.ToString();

                switch (quest.Quest_ID)
                {
                    case 1:
                        Quest_summary.text = $"���彽���� : ({quest.monster_counter} / {Managers.Quest_Completion.Get_Slime_Hunting_Quest_Complete_amount})";

                        // ����Ʈ �Ϸ� ������ �������� �ʾ��� ���� �˶� ����
                        if (quest.monster_counter >= Managers.Quest_Completion.Get_Slime_Hunting_Quest_Complete_amount && !quest.is_achievement_of_conditions)
                        {
                            Managers.Quest_Completion.Quest_Complete_Alarm();
                            quest.is_achievement_of_conditions = true;
                        }
                        break;

                    case 2:
                        int slime_etc_item_amount = 0;

                        // �κ��丮�� �������� ��ȸ�Ͽ� ������ ��� �������� ������ ã��

                        foreach (var item in PlayerInventory.Instance.player_items)
                        {
                            if (item.ItemID == Managers.Quest_Completion.Get_Slime_Drop_item_ID)
                            {
                                slime_etc_item_amount = item.amount;

                                break;
                            }
                        }

                        Quest_summary.text = $"�����Ӿ�ü : ({slime_etc_item_amount} / {Managers.Quest_Completion.Get_Slime_collecting_item_amount})";

                        // ����Ʈ �Ϸ� ������ �������� �ʾ��� ���� �˶� ����
                        if (slime_etc_item_amount >= Managers.Quest_Completion.Get_Slime_collecting_item_amount && !quest.is_achievement_of_conditions)
                        {
                            Managers.Quest_Completion.Quest_Complete_Alarm();
                            quest.is_achievement_of_conditions = true;
                        }
                        break;

                    case 3:
                        Quest_summary.text = $"��� ������ ã�� ��ȭ�� �Ѵ�.";
                        break;

                    case 4:
                        Quest_summary.text = $"���� ���͸� ã�ư� ��ȭ�� �Ѵ�.";
                        break;

                    case 6:
                        Quest_summary.text = $"��ġ�� : ({quest.monster_counter} / {Managers.Quest_Completion.Get_Punch_man_Hunting_Quest_Complete_amount})";

                        // ����Ʈ �Ϸ� ������ �������� �ʾ��� ���� �˶� ����
                        if (quest.monster_counter >= Managers.Quest_Completion.Get_Punch_man_Hunting_Quest_Complete_amount && !quest.is_achievement_of_conditions)
                        {
                            Managers.Quest_Completion.Quest_Complete_Alarm();
                            quest.is_achievement_of_conditions = true;
                        }
                        break;

                    case 5:
                    case 7:
                    case 10:
                    case 12:
                        Quest_summary.text = $"���ð� ��Ű���� ã�ư� ��ȭ�� �Ѵ�.";
                        break;

                    case 8:
                        Quest_summary.text = $"3���� ���� �����Ͽ� ��� �����Ѵ�.";
                        break;
                    case 9:
                        Quest_summary.text = $"���� ���� ��� 5.00 �̻� �޼�";
                        break;
                    case 11:
                        Quest_summary.text = $"ŷ������: ({quest.monster_counter} / {Managers.Quest_Completion.Get_King_slime_Hunting_Quest_Complete_amount})";
                        
                        break;

                }

                break;
            
            }

           
        }

    }


}
