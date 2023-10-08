using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//아이템별 정보에 관한 스크립트 


public enum ItemType
{
    Equipment,
    Consumables,
    Etc,

}

public enum EquipType
{
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
}
