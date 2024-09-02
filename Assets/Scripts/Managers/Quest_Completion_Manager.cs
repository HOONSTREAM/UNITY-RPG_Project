using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Completion_Manager : MonoBehaviour
{
    private const int KILL_SLIME_MAIN_QUEST_ID = 1;
    private const int KILL_PUNCH_MAN_MAIN_QUEST_ID = 6;
    private const int SECOND_MEET_ROOKISS_QUEST_ID = 7;
    private const int EQUIPMENT_WEAR_QUEST_ID = 8;

    private readonly int SLIME_HUNTING_QUEST_COMPLETE_AMOUNT = 2;
    private readonly int COLLECTING_SLIME_ITEM_AMOUNT = 10;
    private readonly int SLIME_DROP_ETC_ITEM_ID = 10;

    private const int PUNCHMAN_HUNTING_QUEST_COMPLETE_AMOUNT = 20;

    public void Kill_Slime_For_Main_Quest()
    {

        for (int i = 0; i < QuestDatabase.instance.QuestDB.Count; i++)
        {
            if (QuestDatabase.instance.QuestDB[i].Quest_ID == KILL_SLIME_MAIN_QUEST_ID)
            {
                if (QuestDatabase.instance.QuestDB[i].is_complete == false)
                {
                    QuestDatabase.instance.QuestDB[i].monster_counter++;
                    Player_Quest.Instance.onChangequest.Invoke(); // 카운터 증가 즉시 반영
                    
                }

                break;
            }
        }
    }

    public void Kill_Punch_man_For_Main_Quest()
    {
        for (int i = 0; i < QuestDatabase.instance.QuestDB.Count; i++)
        {
            if (QuestDatabase.instance.QuestDB[i].Quest_ID == KILL_PUNCH_MAN_MAIN_QUEST_ID)
            {
                if (QuestDatabase.instance.QuestDB[i].is_complete == false)
                {
                    QuestDatabase.instance.QuestDB[i].monster_counter++;
                    Player_Quest.Instance.onChangequest.Invoke(); // 카운터 증가 즉시 반영
                    
                }

                break;
            }
        }
    }

    #region 퀘스트 완료조건 메서드
    public void Kill_Slime_Quest_Conditions_for_completion()
    {
        for (int i = 0; i < Player_Quest.Instance.PlayerQuest.Count; i++)
        {
            if (Player_Quest.Instance.PlayerQuest[i].Quest_ID != KILL_SLIME_MAIN_QUEST_ID) { continue; }

            if (Player_Quest.Instance.PlayerQuest[i].is_complete == false)
            {
                if (Player_Quest.Instance.PlayerQuest[i].monster_counter >= SLIME_HUNTING_QUEST_COMPLETE_AMOUNT)
                {
                    Player_Quest.Instance.PlayerQuest[i].monster_counter = 0; //초기화
                    Player_Quest.Instance.PlayerQuest[i].Quest_Clear();

                    Player_Quest.Instance.RemoveQuest(i);
                    Player_Quest.Instance.onChangequest.Invoke();
                    Print_Info_Text.Instance.PrintUserText("퀘스트 완료");
                    //다음 메인퀘스트 자동 추가
                    Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[1]);

                    break;
                }

                Print_Info_Text.Instance.PrintUserText("퀘스트 조건이 충족되지 않았습니다.");


            }
        }
    }
    public void Slime_collecting_drop_item_quest_Conditions_for_completion()
    {

        for (int i = 0; i < Player_Quest.Instance.PlayerQuest.Count; i++)
        {
            if (Player_Quest.Instance.PlayerQuest[i].Quest_ID != 2) { continue; }

            for (int k = 0; k < PlayerInventory.Instance.player_items.Count; k++)
            {
                if (PlayerInventory.Instance.player_items[k].ItemID == SLIME_DROP_ETC_ITEM_ID && PlayerInventory.Instance.player_items[k].amount >= COLLECTING_SLIME_ITEM_AMOUNT) // 슬라임 액체
                {
                    Player_Quest.Instance.PlayerQuest[i].Quest_Clear();

                    Player_Quest.Instance.RemoveQuest(i);
                    Player_Quest.Instance.onChangequest.Invoke();
                    Print_Info_Text.Instance.PrintUserText("퀘스트 완료");
                    //다음 메인퀘스트 자동 추가
                    Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[2]);

                    return;
                }
            }
        }

        Print_Info_Text.Instance.PrintUserText("퀘스트 조건이 충족되지 않았습니다.");
    }
    public void HelKen_Meet_Quest_Conditions_for_completion()
    {

        for (int i = 0; i < Player_Quest.Instance.PlayerQuest.Count; i++)
        {
            if (Player_Quest.Instance.PlayerQuest[i].Quest_ID != 3) { continue; }

            if (Player_Quest.Instance.PlayerQuest[i].npc_meet == false)
            {
                Print_Info_Text.Instance.PrintUserText("퀘스트 조건이 충족되지 않았습니다.");

                return;
            }

            Player_Quest.Instance.PlayerQuest[i].Quest_Clear();

            Player_Quest.Instance.RemoveQuest(i);
            Player_Quest.Instance.onChangequest.Invoke();
            Print_Info_Text.Instance.PrintUserText("퀘스트 완료");

            //다음 메인퀘스트 자동 추가
            Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[3]);
        }
    }
    public void Rudencian_Chief_Meet_Quest_Conditions_for_completion()
    {

        for (int i = 0; i < Player_Quest.Instance.PlayerQuest.Count; i++)
        {
            if (Player_Quest.Instance.PlayerQuest[i].Quest_ID != 4) { continue; }

            if (Player_Quest.Instance.PlayerQuest[i].npc_meet == false)
            {
                Print_Info_Text.Instance.PrintUserText("퀘스트 조건이 충족되지 않았습니다.");

                return;
            }

            Player_Quest.Instance.PlayerQuest[i].Quest_Clear();

            Player_Quest.Instance.RemoveQuest(i);
            Player_Quest.Instance.onChangequest.Invoke();
            Print_Info_Text.Instance.PrintUserText("퀘스트 완료");

            //다음 메인퀘스트 자동 추가
            Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[4]);
        }
    }
    public void Rudencian_Training_officer_Rookiss_NPC_Meet_Quest_Conditions_for_completion()
    {
        for (int i = 0; i < Player_Quest.Instance.PlayerQuest.Count; i++)
        {
            if (Player_Quest.Instance.PlayerQuest[i].Quest_ID != 5) { continue; }

            if (Player_Quest.Instance.PlayerQuest[i].npc_meet == false)
            {
                Print_Info_Text.Instance.PrintUserText("퀘스트 조건이 충족되지 않았습니다.");

                return;
            }

            Player_Quest.Instance.PlayerQuest[i].Quest_Clear();

            Player_Quest.Instance.RemoveQuest(i);
            Player_Quest.Instance.onChangequest.Invoke();
            Print_Info_Text.Instance.PrintUserText("퀘스트 완료");

            //다음 메인퀘스트 자동 추가
            Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[5]);

        }
    }
    public void Kill_Punch_man_Quest_Conditions_for_completion()
    {
        for (int i = 0; i < Player_Quest.Instance.PlayerQuest.Count; i++)
        {
            if (Player_Quest.Instance.PlayerQuest[i].Quest_ID != KILL_PUNCH_MAN_MAIN_QUEST_ID) { continue; }

            if (Player_Quest.Instance.PlayerQuest[i].is_complete == false)
            {
                if (Player_Quest.Instance.PlayerQuest[i].monster_counter >= PUNCHMAN_HUNTING_QUEST_COMPLETE_AMOUNT)
                {
                    Player_Quest.Instance.PlayerQuest[i].monster_counter = 0; //초기화
                    Player_Quest.Instance.PlayerQuest[i].Quest_Clear();

                    Player_Quest.Instance.RemoveQuest(i);
                    Player_Quest.Instance.onChangequest.Invoke();
                    Print_Info_Text.Instance.PrintUserText("퀘스트 완료");
                    //다음 메인퀘스트 자동 추가
                    Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[6]);

                    break;
                }

                Print_Info_Text.Instance.PrintUserText("퀘스트 조건이 충족되지 않았습니다.");


            }
        }
    }


    public void Rookiss_NPC_Meet_Quest_Conditions_for_completion_Second()
    {
        for (int i = 0; i < Player_Quest.Instance.PlayerQuest.Count; i++)
        {
            if (Player_Quest.Instance.PlayerQuest[i].Quest_ID != SECOND_MEET_ROOKISS_QUEST_ID) { continue; }

            if (Player_Quest.Instance.PlayerQuest[i].npc_meet == false)
            {
                Print_Info_Text.Instance.PrintUserText("퀘스트 조건이 충족되지 않았습니다.");

                return;
            }

            Player_Quest.Instance.PlayerQuest[i].Quest_Clear();

            Player_Quest.Instance.RemoveQuest(i);
            Player_Quest.Instance.onChangequest.Invoke();
            Print_Info_Text.Instance.PrintUserText("퀘스트 완료");

            //다음 메인퀘스트 자동 추가
            Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[7]);
        }
    }

    public void First_Equipment_Wear_Quest()
    {
        for (int i = 0; i < Player_Quest.Instance.PlayerQuest.Count; i++)
        {
            if (Player_Quest.Instance.PlayerQuest[i].Quest_ID != EQUIPMENT_WEAR_QUEST_ID) { continue; }

            if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Head, out Item item) && 
                PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.outter_plate, out Item item_2)&& 
                PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.shoes, out Item item_3))
            {
                Player_Quest.Instance.PlayerQuest[i].Quest_Clear();

                Player_Quest.Instance.RemoveQuest(i);
                Player_Quest.Instance.onChangequest.Invoke();
                Print_Info_Text.Instance.PrintUserText("퀘스트 완료");

                //다음 메인퀘스트 자동 추가
                Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[7]);
            }

            else
            {
                Print_Info_Text.Instance.PrintUserText("퀘스트 조건이 충족되지 않았습니다.");
            }
           
        }
    }

    #endregion


    public int Get_Slime_Hunting_Quest_Complete_amount()
    {
        return SLIME_HUNTING_QUEST_COMPLETE_AMOUNT;
    }

    public int Get_Slime_collecting_item_amount()
    {
        return COLLECTING_SLIME_ITEM_AMOUNT;
    }

    public int Get_Slime_Drop_item_ID()
    {
        return SLIME_DROP_ETC_ITEM_ID;
    }

    public int Get_Punch_man_Hunting_Quest_Complete_amount()
    {
        return PUNCHMAN_HUNTING_QUEST_COMPLETE_AMOUNT;
    }

    public void Quest_Complete_Alarm()
    {
        GameObject go = Managers.Resources.Instantiate("Quest_Complete").gameObject;
        Managers.Sound.Play("로딩배경 효과음");
        Destroy(go, 5.0f);
    }
}
