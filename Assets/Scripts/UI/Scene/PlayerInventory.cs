using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    PlayerStat stat;
    public List<Item> player_items;
    public Slot slot;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    private void Awake()
    {
        Instance = this; 
        player_items = new List<Item>();
        stat = GetComponent<PlayerStat>();
        slot = GetComponent<Slot>();

    }

    public bool AddItem(Item _item)
    {
        

          if (player_items.Count <= 20) 
        
          { 
            if (_item.IsStackable())
            {
                bool ItemAlreadyInInventory = false;
                foreach(Item InventoryItem in player_items)
                {
                    if(InventoryItem.ItemID == _item.ItemID)
                    {
                        InventoryItem.amount++;
                        ItemAlreadyInInventory = true;

                        if (onChangeItem != null)
                        {
                            onChangeItem.Invoke(); //�Ҹ�ǰ���� ���� ������Ʈ
                            PlayerQuickSlot.Instance.onChangeItem.Invoke();
                        }
                    }
              
                }

                
                if (!ItemAlreadyInInventory)
                {
                    if(player_items.Count == 20)
                    {
                        Managers.Game.PrintUserText("������ ����á���ϴ�.");
                        return false;
                    }

                    player_items.Add(_item);

                    if (onChangeItem != null)
                    {
                        onChangeItem.Invoke();
                        PlayerQuickSlot.Instance.onChangeItem.Invoke();
                        return true;
                    }
                }
             
            }

            else // ���������ΰ��
            {
                if(player_items.Count == 20)
                {
                    Managers.Sound.Play("Coin");
                    Managers.Game.PrintUserText("������ ����á���ϴ�.");
                    return false;
                }

                player_items.Add(_item); // ���� ĭ�� 20 �̸��̶�� �������� �߰� 

                if (onChangeItem != null)
                {
                    onChangeItem.Invoke();                  
                    return true;
                }

            }

          
           }


        return true;
     }
     
    private void OnTriggerEnter(Collider collision) // ������ ���� �Լ� 
    {
        
        if (collision.CompareTag("FieldItem"))
        {
            
            FieldItem fielditems = collision.GetComponent<FieldItem>();
            
            if (AddItem(fielditems.GetItem()))
            {
               fielditems.DestroyItem();
               Managers.Sound.Play("Coin");
               
            }            
        }

        onChangeItem.Invoke();

        return;
    }

    public void RemoveItem(int index) // ���� �ľ��Ͽ� ���� ��� �� 1�� ������ ������ ����
    {
        if (player_items[index].itemtype == ItemType.Consumables)
        {
            if (player_items[index].amount > 1)
            {
                player_items[index].amount -= 1; // ��� �� 1�� ���� 

                
                onChangeItem.Invoke();
                PlayerQuickSlot.Instance.onChangeItem.Invoke();

                return;
            }

            else if(player_items[index].amount == 1)
            {

                for (int i = 0; i < PlayerQuickSlot.Instance.quick_slot_item.Count; i++)
                {

                    if (player_items[index].ItemID == PlayerQuickSlot.Instance.quick_slot_item[i].ItemID) //�����Կ� ��������ϴ� �������� �����ϰ� �����ϸ� �����Ե� ���� ����
                    {
                        PlayerQuickSlot.Instance.Quick_slot_RemoveItem(i);
                        PlayerQuickSlot.Instance.onChangeItem.Invoke();
                    }

                }

                player_items.RemoveAt(index);
                onChangeItem.Invoke();

                return;
            }

        }

        else //�Ҹ�ǰ�� �ƴѰ��
        {
            
            player_items.RemoveAt(index);
            onChangeItem.Invoke();

            return;
        }
       
       
    }

   
}
