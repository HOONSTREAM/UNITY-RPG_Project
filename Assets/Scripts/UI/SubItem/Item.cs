using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//�����ۺ� ������ ���� ��ũ��Ʈ 


public enum ItemType
{
    Equipment,
    Consumables,
    Etc,
}

public enum ItemRank
{
    Common,
    UnCommon,
    Rare,
    Unique,
    Legend,

}

public enum WeaponType
{
    One_Hand,
    Two_Hand,
    Bow,
    Axe,
    Kanata,
    No_Weapon
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
    public ItemRank itemrank;
    public EquipType equiptype;
    public WeaponType weapontype;
    public ItemType itemtype;
    public string itemname;
    public string stat_1; // ATK Ȥ�� DEF
    public string stat_2; // STR Ȥ�� DEX
    public int num_1;
    public int num_2;
    public string stat_3; // VIT
    public string stat_4; // AGI
    public int num_3;
    public int num_4;
    public string Description;
    public Sprite itemImage;
    public bool Equip = false;
    public int amount = 1;
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

    /*Ŭ������ �⺻������ ���� �����̱⶧���� ����Ʈ�� ���̷�Ʈ�� ����Ʈ������ �߰��ϰ� �Ǹ� �����ּҰ� ���� ���� ����Ű�� �ȴ�. Ŭ���Լ��� �ذ� */
    public Item Clone()
    {
        Item item = new Item();
        item.ItemID = this.ItemID;
        item.equiptype = this.equiptype;
        item.itemrank = this.itemrank;
        item.weapontype = this.weapontype;
        item.itemtype = this.itemtype;
        item.itemname = this.itemname;
        item.stat_1 = this.stat_1; // ATK Ȥ�� DEF
        item.stat_2 = this.stat_2; // STR 
        item.stat_3 = this.stat_3;
        item.stat_4 = this.stat_4;
        item.num_1 = this.num_1;
        item.num_2 = this.num_2;
        item.num_3 = this.num_3;
        item.num_4 = this.num_4;
        item.Description = this.Description;
        item.itemImage = this.itemImage;
        item.Equip = this.Equip;
        item.amount = 1; //���纻�� 1���� �����Ѵ�.
        item.buyprice = this.buyprice;
        item.sellprice = this.sellprice;
        item.efts = this.efts;

        return item;
    }
}
