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
        quest.onChangequest += RedrawSlotUI;  // Invoke �Լ� ��� �̺�Ʈ �߻����� �Լ� ȣ��
        Managers.UI.SetCanvas(Quest_CANVAS, true);
        //�κ��丮 �巡�� �����ϵ��� �ϴ� �̺�Ʈ
        UI_Base.BindEvent(Quest_Panel, (PointerEventData data) => { Quest_Panel.transform.position = data.position; }, Define.UIEvent.Drag);
        RedrawSlotUI();

        //TEST
        Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[0]);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            
            activequestpanel = !activequestpanel;
            Quest_Panel.SetActive(activequestpanel);
            Managers.UI.SetCanvas(Quest_CANVAS, true);
            Managers.Sound.Play("Inven_Open");

        }

        Onupdate_Summary_Quest_Panel();
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < quest_slots.Length; i++)
        {
            quest_slots[i].slotnum = i;
        }

        for (int i = 0; i < quest_slots.Length; i++) //�� �о������
        {
            quest_slots[i].RemoveSlot();
        }

        for (int i = 0; i < quest.PlayerQuest.Count; i++) //����Ʈ�迭�� ����Ǿ��ִ� �κ��丮�� ������������ �޾ƿ� �ٽ� ������ 
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
                case 1: // �� ���迡 ó������ ���� ���!

                    if (Player_Quest.Instance.PlayerQuest[i].is_complete == false)
                    {
                        if(Player_Quest.Instance.PlayerQuest[i].monster_counter >= 2)
                        {
                            Player_Quest.Instance.PlayerQuest[i].monster_counter = 0; //�ʱ�ȭ
                            Player_Quest.Instance.PlayerQuest[i].Quest_Clear();
                            Player_Quest.Instance.PlayerQuest[i].is_complete = true;
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

                    break;

                case 2: // 2. ����ǰ�� ȹ���غ��� !

                    for(int k = 0; k< PlayerInventory.Instance.player_items.Count; k++)
                    {
                        if (PlayerInventory.Instance.player_items[k].ItemID == 15 && PlayerInventory.Instance.player_items[k].amount == 10) // ������ ��ü
                        {
                            Player_Quest.Instance.PlayerQuest[i].Quest_Clear();
                            Player_Quest.Instance.PlayerQuest[i].is_complete = true;
                            Player_Quest.Instance.RemoveQuest(i);
                            Player_Quest.Instance.onChangequest.Invoke();
                            GameObject.Find("GUI_User_Interface").
                               gameObject.GetComponent<Print_Info_Text>().PrintUserText("����Ʈ �Ϸ�");
                            //���� ��������Ʈ �ڵ� �߰�
                            Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[2]);

                            return;
                        }
                    }

                    GameObject.Find("GUI_User_Interface").
                        gameObject.GetComponent<Print_Info_Text>().PrintUserText("����Ʈ ������ �������� �ʾҽ��ϴ�.");

                    break;

                case 3: //���˰� ��ȭ���� 

                    if(Player_Quest.Instance.PlayerQuest[i].npc_meet == false)
                    {
                        GameObject.Find("GUI_User_Interface").
                       gameObject.GetComponent<Print_Info_Text>().PrintUserText("����Ʈ ������ �������� �ʾҽ��ϴ�.");

                        return;
                    }

                    Player_Quest.Instance.PlayerQuest[i].Quest_Clear();
                    Player_Quest.Instance.PlayerQuest[i].is_complete = true;
                    Player_Quest.Instance.RemoveQuest(i);
                    Player_Quest.Instance.onChangequest.Invoke();
                    GameObject.Find("GUI_User_Interface").
                       gameObject.GetComponent<Print_Info_Text>().PrintUserText("����Ʈ �Ϸ�");
                    //���� ��������Ʈ �ڵ� �߰�
                    //Player_Quest.Instance.AddQuest(QuestDatabase.instance.QuestDB[2]);

                    break;


            }
        }
    }



    public void Button_Function()
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


    private void Onupdate_Summary_Quest_Panel()
    {
        if(Player_Quest.Instance.PlayerQuest.Count == 0)
        {
            Quest_summary_name.text = "����Ʈ ����";
            Quest_summary_type.text = "";
            Quest_summary.text = "";

        }

        for(int i = 0; i<Player_Quest.Instance.PlayerQuest.Count; i++)
        {
            if(Player_Quest.Instance.PlayerQuest[i].questtype == QuestType.Main)
            {
                Quest_summary_name.text = Player_Quest.Instance.PlayerQuest[i].quest_name;
                Quest_summary_type.text = Player_Quest.Instance.PlayerQuest[i].questtype.ToString();
                Quest_summary.text = Player_Quest.Instance.PlayerQuest[i].summing_up_Description;

                break;

            }
        }

    }


}
