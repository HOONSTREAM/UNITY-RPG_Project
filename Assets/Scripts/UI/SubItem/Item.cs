using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//아이템별 정보에 관한 스크립트 


public enum ItemType
{
    Equipment,
    Consumables,
    GoldBag,
    HpPotion,
    Etc,

}

public enum EquipType
{
    None,
    Head,
    Chest,
    Weapon,
}

[System.Serializable]
public class Item 
{
    public EquipType equiptype;
    public ItemType itemtype;
    public string itemname;
    public string stat_1;
    public string stat_2;
    public int num_1;
    public int num_2;
    public string Description;
    public Sprite itemImage;
    public bool Equip = false;
    public int amount = 0;
    

    public List<ItemEffect> efts;

   
    public virtual bool Use()
    {
        bool isUsed = false;
        
        foreach (ItemEffect effect in efts) 
        {
            isUsed = effect.ExecuteRole(itemtype);
        }
        return isUsed;
    }

    public bool IsStackable() //TODO 확장성 고려해서 소모품스택 확장 필요 (지금으로는 안됨)
    {

         switch (itemtype)
        {
            case ItemType.Equipment:
                return false;
                
            case ItemType.Consumables:
                return true;
            case ItemType.GoldBag:
                return true;
            case ItemType.HpPotion:
                return true;

            case ItemType.Etc:
                return true;

            default: return false;

        }

    }
}
