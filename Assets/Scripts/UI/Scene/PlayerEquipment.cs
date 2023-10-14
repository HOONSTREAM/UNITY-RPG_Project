using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInventory;

public class PlayerEquipment : MonoBehaviour
{
    //TODO : ���������κ�, ���� �� �������� , �Ҹ�ǰ���� , ������ ������ , ��������/�Ǹ� , �������� ��������Ʈ ��뿩�� 

    public static PlayerEquipment Instance;
    public Dictionary<EquipType, Item> player_equip;
    PlayerStat stat;
    

    //public delegate void OnEquipItem();
    //public OnEquipItem OnequipItem;
   
    private void Awake()
    {
        Instance = this;
        player_equip = new Dictionary<EquipType, Item>();
        stat = GetComponent<PlayerStat>(); //PrintUserText �Լ� ��� 
    }

 
    public bool EquipItem(Item _item)
    {
        if (player_equip.Count < 20)  //������ �߰��Ҷ� ���Ժ��� �������� ������ �߰�
        {
            if (player_equip.TryGetValue(_item.equiptype, out Item item)) //�ش� Ÿ�� �̹� ���������� �˻� 
            {
               
                stat.PrintUserText("�ش� Ÿ���� �̹� �����Ǿ� �ֽ��ϴ�.");
                _item.Equip = false;
                return false;
            }

            else
            {
                player_equip.Add(_item.equiptype, _item); // �׷����ʴٸ� ���� 
                
                stat.PrintUserText("���� ����!");
    
                stat.SetEquipmentValue(stat.Level); // ������� ���� �ݿ�

                _item.Equip = true; //���� bool ���� true�� ���� 
                return true;
            }
             
        }

        else
        {
            return false;
        }
            
    }

    public bool UnEquipItem(Item _item)
    {
        
        stat.PrintUserText("��� �����մϴ�.");
        player_equip.Remove(_item.equiptype);
        _item.Equip = false;
        stat.SetEquipmentValue(stat.Level);
        return true;
    }

    public void RemoveItem(EquipType equiptype)
    {
        player_equip.Remove(equiptype);
       // OnequipItem.Invoke();
    }

}
