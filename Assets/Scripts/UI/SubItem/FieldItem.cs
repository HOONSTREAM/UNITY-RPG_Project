using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


// 필드아이템 관련 스크립트 
public class FieldItem : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;
    
    public void SetItem(Item _item)
    {

        item.itemname = _item.itemname;
        item.stat_1 = _item.stat_1;
        item.stat_2 = _item.stat_2;
        item.Description = _item.Description;
        item.num_1 = _item.num_1;
        item.num_2 = _item.num_2;
        item.itemImage = _item.itemImage;
        item.equiptype = _item.equiptype;
        item.itemtype = _item.itemtype;
        item.efts = _item.efts;
        item.Equip = _item.Equip;
        image.sprite = _item.itemImage;
        

    }

    public Item GetItem()
    {

        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
