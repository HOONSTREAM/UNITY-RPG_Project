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

        if (storage_item.Count < 20)  //������ �߰��Ҷ� ���Ժ��� �������� ������ �߰�
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
                            onChangeItem.Invoke(); //�Ҹ�ǰ���� ���� ������Ʈ                          
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
        Managers.Game.PrintUserText("â�� ����á���ϴ�.");
        return false;
    }

    public void RemoveItem(int index) // ���� �ľ��Ͽ� ���� ��� �� 1�� ������ ������ ����
    {
        if (storage_item[index].itemtype == ItemType.Consumables)
        {
            if (storage_item[index].amount > 1)
            {
                storage_item[index].amount -= 1; // ��� �� 1�� ���� 
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
