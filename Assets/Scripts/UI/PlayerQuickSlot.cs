using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerSkillQuickSlot;

public class PlayerQuickSlot : MonoBehaviour
{

    #region 플레이어 아이템 퀵슬롯 정보 저장
    [System.Serializable]
    public class Item_Quick_Slot_Data
    {
        public List<Item> items;

        public Item_Quick_Slot_Data(List<Item> player_item_quick_slot)
        {
            this.items = player_item_quick_slot;
        }
    }
    #endregion


    public static PlayerQuickSlot Instance;

    PlayerStat stat;
    public List<Item> quick_slot_item;
    public Quick_Slot slot;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    #region 메서드 아이템 퀵슬롯 정보 저장
    public void Save_Item_Quickslot_Info()
    {
        Item_Quick_Slot_Data data = new Item_Quick_Slot_Data(quick_slot_item);
        ES3.Save("Player_item_quickslot", data);



        Debug.Log("Player_item_quickslot saved using EasySave3");
    }

    public void Load_Item_Quickslot_Info()
    {
        if (ES3.KeyExists("Player_item_quickslot"))
        {
            Item_Quick_Slot_Data data = ES3.Load<Item_Quick_Slot_Data>("Player_item_quickslot");
            quick_slot_item = data.items;
            Debug.Log("Player_item_quickslot loaded using EasySave3");
        }
        else
        {
            Debug.Log("No Player_item_quickslot data found, creating a new one.");
        }
    }
    #endregion
    private void Awake()
    {
        Instance = this;
        quick_slot_item = new List<Item>();
        stat = GetComponent<PlayerStat>();
        slot = GetComponent<Quick_Slot>();

    }


    public bool Quick_slot_AddItem(Item _item, int index = 0)
    {

        if (quick_slot_item.Count == 4) 
        {
            quick_slot_item.RemoveAt(0); //맨 앞에있는 슬롯을 밀어낸다.
            PlayerQuickSlot.Instance.onChangeItem.Invoke();
        }
      quick_slot_item.Add(_item); //clone 함수 쓰지않고 같은 아이템을 참조해야한다. (Clone함수 사용하지않음)
      onChangeItem.Invoke();

      return true;
                        
    }

    public void Quick_slot_RemoveItem(int index)
    {

        if (quick_slot_item[index] == null)
        {
            return;
        }

        if (quick_slot_item[index].amount > 1)
        {
            quick_slot_item[index].amount -= 1; // 사용 시 1개 감소 

            onChangeItem.Invoke();

            return;
        }

        else if (quick_slot_item[index].amount == 1)
        {
            quick_slot_item.RemoveAt(index);
            onChangeItem.Invoke();
            return;

        }

      
                      
    }

}
