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
        stat = GetComponent<PlayerStat>(); //PrintUserText �Լ� ���
       
    }

 
    public bool EquipItem(Slot _item)
    {
        if (player_equip.Count < 20)  //������ �߰��Ҷ� ���Ժ��� �������� ������ �߰�
        {
            if (player_equip.TryGetValue(_item.item.equiptype, out Item item)) //�ش� Ÿ�� �̹� ���������� �˻� 
            {

                Managers.Game.PrintUserText("�ش� Ÿ���� �̹� �����Ǿ� �ֽ��ϴ�.");
                _item.item.Equip = false;
                return false;
            }

            else
            {
                player_equip.Add(_item.item.equiptype, _item.item); // �׷����ʴٸ� ���� 

                Managers.Game.PrintUserText("���� ����!");
    
                stat.SetEquipmentValue(stat.Level,_item.item); // ������� ���� �ݿ�
                _item.item.Equip = true; //���� bool ���� true�� ����                                       
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

        Managers.Game.PrintUserText("��� �����մϴ�.");       
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
