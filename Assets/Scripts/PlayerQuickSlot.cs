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


    public bool AddItem(Item _item)
    {

        if (quick_slot_item.Count < 4)  //아이템 추가할때 슬롯보다 작을때만 아이템 추가
        {

            if (_item.IsStackable())
            {
                bool ItemAlreadyInInventory = false;
                foreach (Item InventoryItem in quick_slot_item)
                {
                    if (InventoryItem.ItemID == _item.ItemID)
                    {
                        InventoryItem.amount++;
                        ItemAlreadyInInventory = true;

                        if (onChangeItem != null)
                        {
                            onChangeItem.Invoke(); //소모품스택 갯수 업데이트                          
                        }
                    }

                }

                if (!ItemAlreadyInInventory)
                {
                    quick_slot_item.Add(_item);

                    if (onChangeItem != null)
                    {
                        onChangeItem.Invoke();
                        return true;
                    }
                }

            }

            else
            {
                quick_slot_item.Add(_item);

                if (onChangeItem != null)
                {
                    onChangeItem.Invoke();
                    return true;
                }

            }

            return true;
        }

        Managers.Sound.Play("Coin");
        stat.PrintUserText("퀵슬롯이 가득찼습니다.");
        return false;

    }

    public void RemoveItem(int index) // 갯수 파악하여 스택 사용 후 1개 남으면 아이템 삭제
    {
        if (quick_slot_item[index].itemtype == ItemType.Consumables)
        {
            if (quick_slot_item[index].amount > 1)
            {
                quick_slot_item[index].amount -= 1; // 사용 시 1개 감소 
                onChangeItem.Invoke();
            }

            else if (quick_slot_item[index].amount == 1)
            {
                quick_slot_item.RemoveAt(index);
                onChangeItem.Invoke();
            }

        }
        else
        {
            quick_slot_item.RemoveAt(index);
            onChangeItem.Invoke();
        }


    }

}
