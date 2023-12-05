using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//아이템별 정보에 관한 스크립트 


public enum ItemType
{
    Equipment,
    Consumables,
    Etc,
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

/*유니티 에디터의 인스펙터에는 사용자가 정의한 클래스 또는 구조체의 정보가 인스펙터에 노출되지 않지만,
  System에서 제공하는 Serializable 키워드를 지정하여 인스펙터에 노출시킬 수 있음.*/

[System.Serializable]
public class Item
{
    public int ItemID;
    public EquipType equiptype;
    public WeaponType weapontype;
    public ItemType itemtype;
    public string itemname;
    public string stat_1;
    public string stat_2;
    public int num_1;
    public int num_2;
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

    /*클래스는 기본적으로 참조 형태이기때문에 리스트에 다이렉트로 리스트정보를 추가하게 되면 참조주소가 같은 곳을 가리키게 된다. 클론함수로 해결 */
    public Item Clone()
    {
        Item item = new Item();
        item.ItemID = this.ItemID;
        item.equiptype = this.equiptype;
        item.weapontype = this.weapontype;
        item.itemtype = this.itemtype;
        item.itemname = this.itemname;
        item.stat_1 = this.stat_1;
        item.stat_2 = this.stat_2;
        item.num_1 = this.num_1;
        item.num_2 = this.num_2;
        item.Description = this.Description;
        item.itemImage = this.itemImage;
        item.Equip = this.Equip;
        item.amount = 1; //복사본은 1개만 복사한다.
        item.buyprice = this.buyprice;
        item.sellprice = this.sellprice;

        item.efts = this.efts;

        return item;
    }
}
