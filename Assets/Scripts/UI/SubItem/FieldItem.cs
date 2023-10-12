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
        item.ItemID = _item.ItemID; //아이템 코드 아이디
        item.itemname = _item.itemname; //아이템 이름
        item.stat_1 = _item.stat_1; // ATK
        item.stat_2 = _item.stat_2; // DEF
        item.Description = _item.Description; // 아이템 설명
        item.num_1 = _item.num_1; // ATK 수치
        item.num_2 = _item.num_2; // DEF 수치
        item.itemImage = _item.itemImage; // 아이템 스프라이트 이미지
        item.equiptype = _item.equiptype; // 장비아이템 장착부위 
        item.itemtype = _item.itemtype; // 아이템 타입 (장비,소비,기타)
        item.efts = _item.efts; // 아이템 사용 시 발생하는 이펙트
        item.Equip = _item.Equip; // 아이템 장착여부 변수
        image.sprite = _item.itemImage; //아이템 드랍시 필드이미지
        item.amount = _item.amount;  //아이템 갯수 (소비,기타재만)
        
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
