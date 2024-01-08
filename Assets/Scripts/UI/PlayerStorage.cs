using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
    public static PlayerStorage Instance;

    PlayerStat stat;
    public List<Item> storage_item;
    public Slot slot;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

   

    private void Awake()
    {
        Instance = this;
        storage_item = new List<Item>();
        stat = GetComponent<PlayerStat>();
        slot = GetComponent<Slot>();
    }

    public bool AddItem(Item _item)
    {

        if (storage_item.Count < 20)  //아이템 추가할때 슬롯보다 작을때만 아이템 추가
        {

            if (_item.IsStackable())
            {
                bool ItemAlreadyInInventory = false;
                foreach (Item InventoryItem in storage_item)
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
                    storage_item.Add(_item);

                    if (onChangeItem != null)
                    {
                        onChangeItem.Invoke();
                        return true;
                    }
                }

            }

            else
            {
                storage_item.Add(_item);

                if (onChangeItem != null)
                {
                    onChangeItem.Invoke();
                    return true;
                }

            }

            return true;
        }

        Managers.Sound.Play("Coin");
        Managers.Game.PrintUserText("창고가 가득찼습니다.");
        return false;
    }

    public void RemoveItem(int index) // 갯수 파악하여 스택 사용 후 1개 남으면 아이템 삭제
    {
        if (storage_item[index].itemtype == ItemType.Consumables)
        {
            if (storage_item[index].amount > 1)
            {
                storage_item[index].amount -= 1; // 사용 시 1개 감소 
                onChangeItem.Invoke();
            }

            else if (storage_item[index].amount == 1)
            {
                storage_item.RemoveAt(index);
                onChangeItem.Invoke();
            }

        }
        else
        {
            storage_item.RemoveAt(index);
            onChangeItem.Invoke();
        }


    }
}
