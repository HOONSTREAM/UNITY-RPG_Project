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
                            onChangeItem.Invoke(); //소모품스택 갯수 업데이트
                            PlayerQuickSlot.Instance.onChangeItem.Invoke();
                        }
                    }
              
                }

                
                if (!ItemAlreadyInInventory)
                {
                    if(player_items.Count == 20)
                    {
                        Managers.Game.PrintUserText("가방이 가득찼습니다.");
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

            else // 장비아이템인경우
            {
                if(player_items.Count == 20)
                {
                    Managers.Sound.Play("Coin");
                    Managers.Game.PrintUserText("가방이 가득찼습니다.");
                    return false;
                }

                player_items.Add(_item); // 가방 칸이 20 미만이라면 장비아이템 추가 

                if (onChangeItem != null)
                {
                    onChangeItem.Invoke();                  
                    return true;
                }

            }

          
           }


        return true;
     }
     
    private void OnTriggerEnter(Collider collision) // 아이템 습득 함수 
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

    public void RemoveItem(int index) // 갯수 파악하여 스택 사용 후 1개 남으면 아이템 삭제
    {
        if (player_items[index].itemtype == ItemType.Consumables)
        {
            if (player_items[index].amount > 1)
            {
                player_items[index].amount -= 1; // 사용 시 1개 감소 

                
                onChangeItem.Invoke();
                PlayerQuickSlot.Instance.onChangeItem.Invoke();

                return;
            }

            else if(player_items[index].amount == 1)
            {

                for (int i = 0; i < PlayerQuickSlot.Instance.quick_slot_item.Count; i++)
                {

                    if (player_items[index].ItemID == PlayerQuickSlot.Instance.quick_slot_item[i].ItemID) //퀵슬롯에 지우고자하는 아이템이 동일하게 존재하면 퀵슬롯도 같이 삭제
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

        else //소모품이 아닌경우
        {
            
            player_items.RemoveAt(index);
            onChangeItem.Invoke();

            return;
        }
       
       
    }

   
}
