using System.Collections;
using System.Collections.Generic;
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
                        if(Player_Quest.Instance.PlayerQuest[i].monster_counter == 2)
                        {
                            Player_Quest.Instance.PlayerQuest[i].monster_counter = 0; //�ʱ�ȭ
                            Player_Quest.Instance.PlayerQuest[i].Quest_Clear();
                            Player_Quest.Instance.RemoveQuest(i);
                            Player_Quest.Instance.onChangequest.Invoke();
                            GameObject.Find("GUI_User_Interface").
                               gameObject.GetComponent<Print_Info_Text>().PrintUserText("����Ʈ �Ϸ�");

                            break;
                        }

                        GameObject.Find("GUI_User_Interface").
                        gameObject.GetComponent<Print_Info_Text>().PrintUserText("����Ʈ ������ �������� �ʾҽ��ϴ�.");

                        
                    }

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

        return;

    }


}
