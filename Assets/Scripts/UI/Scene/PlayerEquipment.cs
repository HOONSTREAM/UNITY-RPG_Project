using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInventory;

public class PlayerEquipment : MonoBehaviour
{
   

    public static PlayerEquipment Instance;
    public Dictionary<EquipType, Item> player_equip;
    private PlayerStat stat;
      
   
    private void Awake()
    {
        Instance = this;
        player_equip = new Dictionary<EquipType, Item>();
        stat = GetComponent<PlayerStat>(); //PrintUserText �Լ� ���
        
    }

 
    public bool EquipItem(Slot _item)
    {
        if (player_equip.Count < 20)  //������ �߰��Ҷ� ���Ժ��� �������� ������ �߰�
        {
            if (player_equip.TryGetValue(_item.item.equiptype, out Item item)) //�ش� Ÿ�� �̹� ���������� �˻� 
            {
               
                stat.PrintUserText("�ش� Ÿ���� �̹� �����Ǿ� �ֽ��ϴ�.");
                _item.item.Equip = false;
                return false;
            }

            else
            {
                player_equip.Add(_item.item.equiptype, _item.item); // �׷����ʴٸ� ���� 
                
                stat.PrintUserText("���� ����!");
    
                stat.SetEquipmentValue(stat.Level); // ������� ���� �ݿ�

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
        
        stat.PrintUserText("��� �����մϴ�.");
        player_equip.Remove(_item.item.equiptype);
        _item.item.Equip = false;
        stat.SetEquipmentValue(stat.Level);
        stat.onchangestat.Invoke();
        return true;
    }

    public void RemoveItem(EquipType equiptype)
    {
        player_equip.Remove(equiptype);
      
    }

}
