using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInventory;

[System.Serializable]
public class EquipTypeItem : SerializableDictionary<EquipType, Item> { }

public class PlayerEquipment : MonoBehaviour
{
    private const int MAX_INVENTORY_COUNT = 20;

    public static PlayerEquipment Instance;

    [SerializeField]
    public EquipTypeItem player_equip; 

    private PlayerStat stat;

    public delegate void OnChangeEquip();
    public OnChangeEquip onChangeEquip;

    private void Awake()
    {
        Instance = this;
        stat = GetComponent<PlayerStat>(); //PrintUserText �Լ� ���
       
    }

 
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
