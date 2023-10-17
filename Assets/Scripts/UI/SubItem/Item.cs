using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//�����ۺ� ������ ���� ��ũ��Ʈ 


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

/*����Ƽ �������� �ν����Ϳ��� ����ڰ� ������ Ŭ���� �Ǵ� ����ü�� ������ �ν����Ϳ� ������� ������,
  System���� �����ϴ� Serializable Ű���带 �����Ͽ� �ν����Ϳ� �����ų �� ����.*/

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
