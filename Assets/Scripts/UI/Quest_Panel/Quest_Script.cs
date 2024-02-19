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

    Player_Quest quest; //플레이어 퀘스트창 참조
    PlayerStat stat; //플레이어 스텟 참조 (골드업데이트)



    public Quest_Slot[] quest_slots;
    public Transform quest_slotHolder;
    public Slot[] Player_slots;
    public Transform player_slotHolder;

    public bool activequestpanel = false;


    void Start()
    {
        stat = GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
        quest = Player_Quest.Instance;
        quest_slots = quest_slotHolder.GetComponentsInChildren<Quest_Slot>();
        Player_slots = player_slotHolder.GetComponentsInChildren<Slot>();

        Quest_Panel.SetActive(activequestpanel);
        quest.onChangequest += RedrawSlotUI;  // Invoke 함수 등록 이벤트 발생마다 함수 호출
        Managers.UI.SetCanvas(Quest_CANVAS, true);
        //인벤토리 드래그 가능하도록 하는 이벤트
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

        for (int i = 0; i < quest_slots.Length; i++) //싹 밀어버리고
        {
            quest_slots[i].RemoveSlot();
        }

        for (int i = 0; i < quest.PlayerQuest.Count; i++) //리스트배열로 저장되어있는 인벤토리의 아이템정보를 받아와 다시 재정렬 
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
                case 1: // 이 세계에 처음으로 발을 딛다!

                    if (Player_Quest.Instance.PlayerQuest[i].is_complete == false)
                    {
                        if(Player_Quest.Instance.PlayerQuest[i].monster_counter == 2)
                        {
                            Player_Quest.Instance.PlayerQuest[i].monster_counter = 0; //초기화
                            Player_Quest.Instance.PlayerQuest[i].Quest_Clear();
                            Player_Quest.Instance.RemoveQuest(i);
                            Player_Quest.Instance.onChangequest.Invoke();
                            GameObject.Find("GUI_User_Interface").
                               gameObject.GetComponent<Print_Info_Text>().PrintUserText("퀘스트 완료");

                            break;
                        }

                        GameObject.Find("GUI_User_Interface").
                        gameObject.GetComponent<Print_Info_Text>().PrintUserText("퀘스트 조건이 충족되지 않았습니다.");

                        
                    }

                    break;

            }
        }
    }



    public void Button_Function()
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

        return;

    }


}
