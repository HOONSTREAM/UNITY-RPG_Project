using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEditor.Progress;

public class QuickSlot_Script : MonoBehaviour
{
    PlayerQuickSlot quickslot; //플레이어 퀵슬롯 참조
    PlayerStat stat; //플레이어 스텟 참조 (골드업데이트)

    public Item item;
    public Quick_Slot[] quick_slot;
    public Transform quickslot_holder;
    public Slot[] Player_slots;
    public Transform player_slotHolder;

   

    void Start()
    {
        stat = GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
        quickslot = PlayerQuickSlot.Instance;
        quick_slot = quickslot_holder.GetComponentsInChildren<Quick_Slot>();
        Player_slots = player_slotHolder.GetComponentsInChildren<Slot>();

        quickslot.onChangeItem += RedrawSlotUI;  // Invoke 함수 등록 이벤트 발생마다 함수 호출

        RedrawSlotUI();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (PlayerQuickSlot.Instance.quick_slot_item.Count == 0)
            {
                GameObject player = Managers.Game.GetPlayer();
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("퀵슬롯에 아이템이 없습니다.");
                return;
            }

            item = PlayerQuickSlot.Instance.quick_slot_item[0];
            
            Item_Use(item);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (PlayerQuickSlot.Instance.quick_slot_item.Count == 0)
            {
                GameObject player = Managers.Game.GetPlayer();
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("퀵슬롯에 아이템이 없습니다.");
                return;
            }

            item = PlayerQuickSlot.Instance.quick_slot_item[1];

            Item_Use(item);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (PlayerQuickSlot.Instance.quick_slot_item.Count == 0)
            {
                GameObject player = Managers.Game.GetPlayer();
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("퀵슬롯에 아이템이 없습니다.");
                return;
            }

            item = PlayerQuickSlot.Instance.quick_slot_item[2];

            Item_Use(item);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (PlayerQuickSlot.Instance.quick_slot_item.Count == 0)
            {
                GameObject player = Managers.Game.GetPlayer();
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("퀵슬롯에 아이템이 없습니다.");
                return;
            }

            item = PlayerQuickSlot.Instance.quick_slot_item[3];

            Item_Use(item);
        }


    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < quick_slot.Length; i++)
        {
            quick_slot[i].slotnum = i;
        }

        for (int i = 0; i < quick_slot.Length; i++) //싹 밀어버리고
        {
            quick_slot[i].RemoveSlot();
        }

        for (int i = 0; i < quickslot.quick_slot_item.Count; i++) //리스트배열로 저장되어있는 인벤토리의 아이템정보를 받아와 다시 재정렬 
        {
            quick_slot[i].item = quickslot.quick_slot_item[i];
            quick_slot[i].UpdateSlotUI();

        }
    }

    public void Item_Use(Item item)
    {
        
        if (item.itemtype == ItemType.Consumables)
        {
            bool isUsed = this.item.Use();

            if (isUsed)

            {
                for (int i = 0; i < Player_slots.Length; i++)
                {
                    if (Player_slots[i].item == this.item) //인벤토리에 같은 아이템이 있는지 검사하고 그 같은아이템도 삭제(invoke 포함됨)
                    {
                        PlayerInventory.Instance.RemoveItem(Player_slots[i].slotnum);
                        break; //한번 만족했으면 반복문을 빠져나가야 한다. (선택된 퀵슬롯 기준 뒷 퀵슬롯 전부 사라지는 문제 해결)
                    }

                }
                //PlayerQuickSlot.Instance.Quick_slot_RemoveItem(this.slotnum);
                PlayerQuickSlot.Instance.onChangeItem.Invoke();

            }

            return;
        }

        else
        {
            return;
        }
    }

}
