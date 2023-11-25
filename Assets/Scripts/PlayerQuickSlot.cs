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


        quick_slot_item.RemoveAt(index);
        onChangeItem.Invoke();
        
        
    }

}
