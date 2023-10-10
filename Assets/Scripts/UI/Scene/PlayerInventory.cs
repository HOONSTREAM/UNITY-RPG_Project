using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance = new PlayerInventory();
    PlayerStat stat;
    public List<Item> player_items = new List<Item>();

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    private void Awake()
    {
        Instance = this; 
        stat = GetComponent<PlayerStat>();
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
                    if(InventoryItem.itemtype == _item.itemtype)
                    {
                        InventoryItem.amount++;
                        ItemAlreadyInInventory = true;
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

    public void RemoveItem(int index)
    {
        player_items.RemoveAt(index);
        onChangeItem.Invoke();
        //TODO . 아이템 정리하되 장비된 것은 체크표시 유지되도록,
    }

}
