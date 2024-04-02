using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;


// 필드아이템 관련 스크립트 
public class FieldItem : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;
    public GameObject fielditemPrefab;
    public Vector3 pos;



    public void SetItem(Item _item)
    {
        item.ItemID = _item.ItemID; //아이템 코드 아이디
        item.itemrank = _item.itemrank; // 아이템 등급
        item.itemname = _item.itemname; //아이템 이름
        item.stat_1 = _item.stat_1; // 무기 : ATK , 방어구 : DEF
        item.stat_2 = _item.stat_2; // STR/DEX
        item.stat_3 = _item.stat_3;
        item.stat_4 = _item.stat_4;
        item.Description = _item.Description; // 아이템 설명
        item.num_1 = _item.num_1; // 무기 : ATK , 방어구 DEF
        item.num_2 = _item.num_2; // STR/DEX
        item.num_3 = _item.num_3;
        item.num_4 = _item.num_4;
        item.itemImage = _item.itemImage; // 아이템 스프라이트 이미지
        item.equiptype = _item.equiptype; // 장비아이템 장착부위 
        item.weapontype = _item.weapontype; //무기의 타입
        item.itemtype = _item.itemtype; // 아이템 타입 (장비,소비,기타)
        item.efts = _item.efts; // 아이템 사용 시 발생하는 이펙트
        item.Equip = _item.Equip; // 아이템 장착여부 변수
        image.sprite = _item.itemImage; //아이템 드랍시 필드이미지
        item.amount = _item.amount;  //아이템 갯수 (소비,기타재만)
        item.buyprice = _item.buyprice; //구매가격
        item.sellprice = _item.sellprice; //판매가격
        
        
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
        
    }

    public GameObject SlimeDropFieldItem() //필드에 아이템 생성
    {
        GameObject go = Instantiate(fielditemPrefab, pos, Quaternion.identity); //Quaternion.identity는 회전없음을 나타내는 쿼터니언    
        go.GetComponent<FieldItem>().SetItem(ItemDataBase.instance.GetAllItems()["Etcs"][Random.Range(0,0)]);
       
        return go;

    }

    public GameObject PunchmanDropFieldItem() //필드에 아이템 생성
    {
        GameObject go = Instantiate(fielditemPrefab, pos, Quaternion.identity); //Quaternion.identity는 회전없음을 나타내는 쿼터니언    
        go.GetComponent<FieldItem>().SetItem(ItemDataBase.instance.GetAllItems()["Etcs"][1]);

        return go;

    }


}
