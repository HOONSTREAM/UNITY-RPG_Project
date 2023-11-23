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

        if (quick_slot_item.Count < 4)  //������ �߰��Ҷ� ���Ժ��� �������� ������ �߰�
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
                            onChangeItem.Invoke(); //�Ҹ�ǰ���� ���� ������Ʈ                          
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
        stat.PrintUserText("�������� ����á���ϴ�.");
        return false;

    }

    public void RemoveItem(int index) // ���� �ľ��Ͽ� ���� ��� �� 1�� ������ ������ ����
    {
        if (quick_slot_item[index].itemtype == ItemType.Consumables)
        {
            if (quick_slot_item[index].amount > 1)
            {
                quick_slot_item[index].amount -= 1; // ��� �� 1�� ���� 
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
