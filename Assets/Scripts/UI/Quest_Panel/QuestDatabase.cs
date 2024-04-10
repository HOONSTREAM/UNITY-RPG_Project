using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{
    public static QuestDatabase instance;

    public List<Quest> QuestDB = new List<Quest>();


    private readonly int SLIME_HUNTING_QUEST_COMPLETE_AMOUNT = 2;
    private readonly int COLLECTING_SLIME_ITEM_AMOUNT = 10;
    private readonly int SLIME_DROP_ETC_ITEM_ID = 10;

    
    private void Awake()
    {
        instance = this;
    }


   public void KillSlimeForQuest()
    {

        for (int i = 0; i < QuestDatabase.instance.QuestDB.Count; i++)
        {
            if (QuestDatabase.instance.QuestDB[i].Quest_ID == 1)
            {
                if (QuestDatabase.instance.QuestDB[i].is_complete == false)
                {
                    QuestDatabase.instance.QuestDB[i].monster_counter++;
                    Player_Quest.Instance.onChangequest.Invoke(); // ī���� ���� ��� �ݿ�
                }

                break;
            }
        }
    }

    #region ����Ʈ �Ϸ����� �޼���
    public void Kill_Slime_Quest_Conditions_for_completion()
    {
        for (int i = 0; i < Player_Quest.Instance.PlayerQuest.Count; i++)
        {
            if (Player_Quest.Instance.PlayerQuest[i].is_complete == false)
            {
                if (Player_Quest.Instance.PlayerQuest[i].monster_counter >= 2)
                {
                    Player_Quest.Instance.PlayerQuest[i].monster_counter = 0; //�ʱ�ȭ
                    Player_Quest.Instance.PlayerQuest[i].Quest_Clear();

                    Player_Quest.Instance.RemoveQuest(i);
                    Player_Quest.Instance.onChangequest.Invoke();
                    GameObject.Find("GUI_User_Interface").
                       gameObject.GetComponent<Print_Info_Text>().PrintUserText("����Ʈ �Ϸ�");
                    //���� ��������Ʈ �ڵ� �߰�
                    Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[1]);

                    break;
                }

                GameObject.Find("GUI_User_Interface").
                gameObject.GetComponent<Print_Info_Text>().PrintUserText("����Ʈ ������ �������� �ʾҽ��ϴ�.");


            }
        }
    }
    public void Slime_collecting_drop_item_quest_Conditions_for_completion()
    {

        for (int i = 0; i < Player_Quest.Instance.PlayerQuest.Count; i++)
        {
            for (int k = 0; k < PlayerInventory.Instance.player_items.Count; k++)
            {
                if (PlayerInventory.Instance.player_items[k].ItemID == SLIME_DROP_ETC_ITEM_ID && PlayerInventory.Instance.player_items[k].amount >= COLLECTING_SLIME_ITEM_AMOUNT) // ������ ��ü
                {
                    Player_Quest.Instance.PlayerQuest[i].Quest_Clear();

                    Player_Quest.Instance.RemoveQuest(i);
                    Player_Quest.Instance.onChangequest.Invoke();
                    GameObject.Find("GUI_User_Interface").
                       gameObject.GetComponent<Print_Info_Text>().PrintUserText("����Ʈ �Ϸ�");
                    //���� ��������Ʈ �ڵ� �߰�
                    Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[2]);

                    return;
                }
            }
        }

        GameObject.Find("GUI_User_Interface").
            gameObject.GetComponent<Print_Info_Text>().PrintUserText("����Ʈ ������ �������� �ʾҽ��ϴ�.");
    }
    public void HelKen_Meet_Quest_Conditions_for_completion()
    {
        for (int i = 0; i < Player_Quest.Instance.PlayerQuest.Count; i++)
        {
            if (Player_Quest.Instance.PlayerQuest[i].npc_meet == false)
            {
                GameObject.Find("GUI_User_Interface").
               gameObject.GetComponent<Print_Info_Text>().PrintUserText("����Ʈ ������ �������� �ʾҽ��ϴ�.");

                return;
            }

            Player_Quest.Instance.PlayerQuest[i].Quest_Clear();

            Player_Quest.Instance.RemoveQuest(i);
            Player_Quest.Instance.onChangequest.Invoke();
            GameObject.Find("GUI_User_Interface").
               gameObject.GetComponent<Print_Info_Text>().PrintUserText("����Ʈ �Ϸ�");

            //���� ��������Ʈ �ڵ� �߰� TODO
            //Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[2]);
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


}
