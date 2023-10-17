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
    Shield,
    necklace,
    Ring,
    shoes,
    vehicle

}

/*유니티 에디터의 인스펙터에는 사용자가 정의한 클래스 또는 구조체의 정보가 인스펙터에 노출되지 않지만,
  System에서 제공하는 Serializable 키워드를 지정하여 인스펙터에 노출시킬 수 있음.*/

[System.Serializable] 
public class Item 
{
    public int ItemID;
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
    public int buyprice;
    public int sellprice;
    

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

    public bool IsStackable() 
    {

         switch (itemtype)
        {
            case ItemType.Equipment:
                return false;
                
            case ItemType.Consumables:
                return true;
                
            case ItemType.Etc:
                return true;

            default: return false;

        }

    }
}
