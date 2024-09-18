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

    Player_Quest quest; //플레이어 퀘스트창 참조
    PlayerStat stat; //플레이어 스텟 참조 (골드업데이트)



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
        stat = GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
        quest = Player_Quest.Instance;
        quest_slots = quest_slotHolder.GetComponentsInChildren<Quest_Slot>();
        Player_slots = player_slotHolder.GetComponentsInChildren<Slot>();

        Quest_Panel.SetActive(activequestpanel);
        Player_Quest.Instance.onChangequest += RedrawSlotUI;  // Invoke 함수 등록 이벤트 발생마다 함수 호출
        Managers.UI.SetCanvas(Quest_CANVAS, true);       
        UI_Base.BindEvent(Quest_Panel, (PointerEventData data) => { Quest_Panel.transform.position = data.position; }, Define.UIEvent.Drag);  //인벤토리 드래그 가능하도록 하는 이벤트
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
        Managers.UI.SetCanvas(Quest_CANVAS, true); // 캔버스 SortOrder 순서를 열릴때 마다 정의함. (제일 마지막에 열린것이 가장 위로)
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
            Quest_summary_name.text = "퀘스트 없음";
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
                        Quest_summary.text = $"레드슬라임 : ({quest.monster_counter} / {Managers.Quest_Completion.Get_Slime_Hunting_Quest_Complete_amount})";

                        // 퀘스트 완료 조건이 충족되지 않았을 때만 알람 실행
                        if (quest.monster_counter >= Managers.Quest_Completion.Get_Slime_Hunting_Quest_Complete_amount && !quest.is_achievement_of_conditions)
                        {
                            Managers.Quest_Completion.Quest_Complete_Alarm();
                            quest.is_achievement_of_conditions = true;
                        }
                        break;

                    case 2:
                        int slime_etc_item_amount = 0;

                        // 인벤토리의 아이템을 순회하여 슬라임 드롭 아이템의 수량을 찾음

                        foreach (var item in PlayerInventory.Instance.player_items)
                        {
                            if (item.ItemID == Managers.Quest_Completion.Get_Slime_Drop_item_ID)
                            {
                                slime_etc_item_amount = item.amount;

                                break;
                            }
                        }

                        Quest_summary.text = $"슬라임액체 : ({slime_etc_item_amount} / {Managers.Quest_Completion.Get_Slime_collecting_item_amount})";

                        // 퀘스트 완료 조건이 충족되지 않았을 때만 알람 실행
                        if (slime_etc_item_amount >= Managers.Quest_Completion.Get_Slime_collecting_item_amount && !quest.is_achievement_of_conditions)
                        {
                            Managers.Quest_Completion.Quest_Complete_Alarm();
                            quest.is_achievement_of_conditions = true;
                        }
                        break;

                    case 3:
                        Quest_summary.text = $"기사 헬켄을 찾아 대화를 한다.";
                        break;

                    case 4:
                        Quest_summary.text = $"촌장 월터를 찾아가 대화를 한다.";
                        break;

                    case 6:
                        Quest_summary.text = $"펀치맨 : ({quest.monster_counter} / {Managers.Quest_Completion.Get_Punch_man_Hunting_Quest_Complete_amount})";

                        // 퀘스트 완료 조건이 충족되지 않았을 때만 알람 실행
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
                        Quest_summary.text = $"수련관 루키스를 찾아가 대화를 한다.";
                        break;

                    case 8:
                        Quest_summary.text = $"3부위 방어구를 구매하여 장비를 장착한다.";
                        break;
                    case 9:
                        Quest_summary.text = $"무기 종류 어빌 5.00 이상 달성";
                        break;
                    case 11:
                        Quest_summary.text = $"킹슬라임: ({quest.monster_counter} / {Managers.Quest_Completion.Get_King_slime_Hunting_Quest_Complete_amount})";
                        
                        break;

                }

                break;
            
            }

           
        }

    }


}
