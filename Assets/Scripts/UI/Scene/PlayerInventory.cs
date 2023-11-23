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
        
            if (player_items.Count < 20)  //아이템 추가할때 슬롯보다 작을때만 아이템 추가
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
                            onChangeItem.Invoke(); //소모품스택 갯수 업데이트                          
                        }
                    }
              
                }

                

                if (!ItemAlreadyInInventory)
                {
                    player_items.Add(_item);

                    if (onChangeItem != null)
                    {
                        onChangeItem.Invoke();
                        return true;
                    }
                }
             
            }

            else
            {
                player_items.Add(_item);

                if (onChangeItem != null)
                {
                    onChangeItem.Invoke();
                    return true;
                }

            }

            return true;
            }

            Managers.Sound.Play("Coin");
            stat.PrintUserText("가방이 가득찼습니다.");
            return false;
     }
     
    private void OnTriggerEnter(Collider collision)
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
    }

    public void RemoveItem(int index) // 갯수 파악하여 스택 사용 후 1개 남으면 아이템 삭제
    {
        if (player_items[index].itemtype == ItemType.Consumables)
        {
            if (player_items[index].amount > 1)
            {
                player_items[index].amount -= 1; // 사용 시 1개 감소 

                
                onChangeItem.Invoke();
               
            }

            else if(player_items[index].amount == 1)
            {
                

                player_items.RemoveAt(index);
                onChangeItem.Invoke();
               
            }

        }
        else
        {
            
            player_items.RemoveAt(index);
            onChangeItem.Invoke();
            
        }
       
       
    }

   
}
