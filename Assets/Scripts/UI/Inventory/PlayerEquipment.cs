using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static PlayerInventory;

[System.Serializable]
//public class EquipTypeItem : SerializableDictionary<EquipType, Item> { }

public class PlayerEquipment : MonoBehaviour
{

    #region 테스트코드 장비장착정보 저장
    [System.Serializable]
    public class EquipData
    {
        public Dictionary<EquipType, Item> equip_items; 

        public EquipData(Dictionary<EquipType, Item> items)
        {
            this.equip_items = items;
        }
    }
    #endregion

    private const int MAX_INVENTORY_COUNT = 20;

    public static PlayerEquipment Instance;

    [SerializeField]
    public Dictionary<EquipType, Item> player_equip; 

    private PlayerStat stat;

    public delegate void OnChangeEquip();
    public OnChangeEquip onChangeEquip;

    private void Awake()
    {
        Instance = this;
        player_equip = new Dictionary<EquipType, Item>();
        stat = GetComponent<PlayerStat>(); //PrintUserText 함수 사용
       
    }

    #region 테스트 메서드 장착정보 저장
    public void Save_Equipment()
    {
        EquipData data = new EquipData(player_equip);
        ES3.Save("Player_equipment", data);



        Debug.Log("Player_Equipment saved using EasySave3");
    }

    public void Load_Equipment()
    {
        if (ES3.KeyExists("Player_equipment"))
        {
            EquipData data = ES3.Load<EquipData>("Player_equipment");
            player_equip = data.equip_items;
            Debug.Log("Player_Equipment loaded using EasySave3");
        }
        else
        {
            Debug.Log("No inventory data found, creating a new one.");
        }
    }

    #endregion
    public bool EquipItem(Slot _item)
    {
        if (player_equip.Count <MAX_INVENTORY_COUNT)  //아이템 추가할때 슬롯보다 작을때만 아이템 추가
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
    
                stat.SetEquipmentValue(stat.LEVEL,_item.item); // 장착장비 스텟 반영
                _item.item.Equip = true; //장착 bool 변수 true로 변경                                       
                stat.onchangestat.Invoke();
                onChangeEquip.Invoke();
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
        stat.SetEquipmentValue(stat.LEVEL, _item.item);
        player_equip.Remove(_item.item.equiptype);
        _item.item.Equip = false;
        stat.onchangestat.Invoke();
        onChangeEquip.Invoke();
        return true;
    }

    public void RemoveItem(EquipType equiptype)
    {
        player_equip.Remove(equiptype);
      
    }

}
