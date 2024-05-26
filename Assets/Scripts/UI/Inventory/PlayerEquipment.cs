using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static PlayerInventory;

[System.Serializable]
//public class EquipTypeItem : SerializableDictionary<EquipType, Item> { }

public class PlayerEquipment : MonoBehaviour
{

    #region �׽�Ʈ�ڵ� ����������� ����
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
        stat = GetComponent<PlayerStat>(); //PrintUserText �Լ� ���
       
    }

    #region �׽�Ʈ �޼��� �������� ����
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
        if (player_equip.Count <MAX_INVENTORY_COUNT)  //������ �߰��Ҷ� ���Ժ��� �������� ������ �߰�
        {
            if (player_equip.TryGetValue(_item.item.equiptype, out Item item)) //�ش� Ÿ�� �̹� ���������� �˻� 
            {

                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("�ش� Ÿ���� �̹� �����Ǿ� �ֽ��ϴ�.");
                _item.item.Equip = false;
                return false;
            }

            else
            {
                player_equip.Add(_item.item.equiptype, _item.item); // �׷����ʴٸ� ���� 

                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("���� ����!");
    
                stat.SetEquipmentValue(stat.LEVEL,_item.item); // ������� ���� �ݿ�
                _item.item.Equip = true; //���� bool ���� true�� ����                                       
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

        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("��� �����մϴ�.");       
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
