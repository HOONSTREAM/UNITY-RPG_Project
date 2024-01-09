using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInventory;

[System.Serializable]
public class EquipTypeItem : SerializableDictionary<EquipType, Item> { }

public class PlayerEquipment : MonoBehaviour
{
   
    public static PlayerEquipment Instance;

    [SerializeField]
    public EquipTypeItem player_equip; 

    private PlayerStat stat;
      
   
    private void Awake()
    {
        Instance = this;
        stat = GetComponent<PlayerStat>(); //PrintUserText 함수 사용
       
    }

 
    public bool EquipItem(Slot _item)
    {
        if (player_equip.Count < 20)  //아이템 추가할때 슬롯보다 작을때만 아이템 추가
        {
            if (player_equip.TryGetValue(_item.item.equiptype, out Item item)) //해당 타입 이미 장착중인지 검사 
            {

                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("해당 타입은 이미 장착되어 있습니다.");
                _item.item.Equip = false;
                return false;
            }

            else
            {
                player_equip.Add(_item.item.equiptype, _item.item); // 그렇지않다면 장착 

                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("장착 성공!");
    
                stat.SetEquipmentValue(stat.Level,_item.item); // 장착장비 스텟 반영
                _item.item.Equip = true; //장착 bool 변수 true로 변경                                       
                stat.onchangestat.Invoke();
                return true;
            }
             
        }

        else
        {
            return false;
        }
            
    }

    public bool UnEquipItem(Slot _item)
    {

        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("장비를 해제합니다.");       
        stat.SetEquipmentValue(stat.Level, _item.item);
        player_equip.Remove(_item.item.equiptype);
        _item.item.Equip = false;
        stat.onchangestat.Invoke();
        return true;
    }

    public void RemoveItem(EquipType equiptype)
    {
        player_equip.Remove(equiptype);
      
    }

}
