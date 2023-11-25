using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuickSlot : MonoBehaviour
{
    public static PlayerQuickSlot Instance;

    PlayerStat stat;
    public List<Item> quick_slot_item;
    public Quick_Slot slot;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;


    private void Awake()
    {
        Instance = this;
        quick_slot_item = new List<Item>();
        stat = GetComponent<PlayerStat>();
        slot = GetComponent<Quick_Slot>();

    }


    public bool Quick_slot_AddItem(Item _item, int index = 0)
    {

      quick_slot_item.Add(_item);
      onChangeItem.Invoke();


      return true;
                        
    }

    public void Quick_slot_RemoveItem(int index) // 갯수 파악하여 스택 사용 후 1개 남으면 아이템 삭제
    {
        if (quick_slot_item[index] == null)
        {
            return;
        }

        quick_slot_item.RemoveAt(index);
        onChangeItem.Invoke();
        
    }

}
