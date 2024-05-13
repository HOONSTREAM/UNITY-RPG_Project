using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static CartoonHeroes.SetCharacter;
using static UnityEditor.Progress;

public class Slot_Equip_Drop : MonoBehaviour
{
    #region Class ����
    public GameObject Equip_Drop_Selection;
    public GameObject Consumable_use_Drop_Selection;
    public GameObject ETC_Drop_Selection;
    public Item slot_item; // ���Կ� �ش��ϴ� ������ ����
    public int slot_number; // ������ ��ȣ ����
    public Slot[] slots; //�÷��̾� ���� ����
    public Quick_Slot[] quick_slot; //�÷��̾��� ������ ����
    public Transform quickslot_holder;
    public PlayerStat stat;
    public GameObject Drop_Input_console;
    public int amount; //�Ҹ�ǰ �Ǹ� ����
    private int temp_Get_Slotnum_item_amount; // Get_Slotnum �Լ��� ���� �޾ƿ��� �������� ������ ������ (for�����⿡ �̿�)
    #endregion

    void Start()
    {
        GameObject go = GameObject.Find("NewInvenUI").gameObject; 
        slots = go.GetComponentsInChildren<Slot>();
        stat = Managers.Game.GetPlayer().gameObject.GetComponent<PlayerStat>();
        quick_slot = quickslot_holder.GetComponentsInChildren<Quick_Slot>();
    }

    public Item Get_Slotnum(int slotnum) //���Կ� �ִ� �������� �����޾� private Item ������ �����صΰ�, �� ������ �ѹ��� ����
    {
        slot_number = slotnum;
        temp_Get_Slotnum_item_amount = slots[slotnum].item.amount;
        return slot_item = slots[slotnum].item;
    }

    public void Equip()
    {
        Equip_Drop_Selection.SetActive(false);

        bool isUse = slot_item.Use();

        if (isUse)
        {
            if (slot_item.itemtype == ItemType.Equipment)
            {
                if (slot_item.Equip) //�̹� �������ΰ�� �ٽ� ������� ��������
                {
                    PlayerEquipment.Instance.UnEquipItem(slots[slot_number]);
                    slots[slot_number].equiped_image.gameObject.SetActive(false); //�������� �� üũǥ�� ����

                    return;
                }
               
                {
                    PlayerEquipment.Instance.EquipItem(slots[slot_number]); //������ ���� �Լ�
                    if (slot_item.Equip)
                    {
                        slots[slot_number].equiped_image.gameObject.SetActive(true); //üũǥ��

                    }
                    else //������ �� ������� (������ ��ĥ���)
                    {
                        slots[slot_number].equiped_image.gameObject.SetActive(false); //üũǥ�� ����
                    }

                }

            }
                      

        }

    }

    public void Consumable_Use()
    {
        Consumable_use_Drop_Selection.SetActive(false);

        bool isUse = slot_item.Use();

        if (isUse)
        {
            if (slot_item == null)
            {
                return;
            }

            PlayerInventory.Instance.RemoveItem(slot_number);
        
        }
    }

    public void Drop()
    {
        if (slot_item.itemtype == ItemType.Equipment)
        {
            if (slot_item.Equip) //�̹� �������ΰ�� ���� �� ����.
            {
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("�������� �������� ���� �� �����ϴ�.");
                Equip_Drop_Selection.SetActive(false);
                return;
            }

            else
            {
                PlayerInventory.Instance.RemoveItem(slot_number);
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("�������� �����ϴ�.");
                Equip_Drop_Selection.SetActive(false);
            }
        }

        else if(slot_item.itemtype == ItemType.Consumables || slot_item.itemtype == ItemType.SkillBook)
        {
            if (slot_item.amount > 1) //������ 1������ ������� ���� ���� ����
            {
                
                Consumable_use_Drop_Selection.SetActive(false);
                Drop_Input_console.SetActive(true); // ���������� �Է¹޴� â ����

            }
            else
            {
                PlayerInventory.Instance.RemoveItem(slot_number);
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("�Һ� �������� �����ϴ�.");
                Consumable_use_Drop_Selection.SetActive(false);
            }
        }

        else // ��Ÿ�������ΰ�� 
        {
            if (slot_item.amount > 1) //������ 1������ ������� ���� ���� ����
            {

                ETC_Drop_Selection.SetActive(false);
                Drop_Input_console.SetActive(true); // ���������� �Է¹޴� â ����

            }
            else
            {
                PlayerInventory.Instance.RemoveItem(slot_number);
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("��Ÿ �������� �����ϴ�.");              
                ETC_Drop_Selection.SetActive(false);
            }
        }


               
        
    }

    public void RegisterQuickSlot() // ������ ��� �Լ� 
    {
        Consumable_use_Drop_Selection.gameObject.SetActive(false);
        Managers.Sound.Play("Coin");

      
        for (int i = 0; i < quick_slot.Length; i++) //�����Կ� �̹� �ش�������� �ִ��� �˻�
        {
            if (quick_slot[i].item == slot_item)
            {
                for(int j = 0; j < temp_Get_Slotnum_item_amount; j++)  // �� �ش������ �����͸� ���� �����ϰ�
                {
                    PlayerQuickSlot.Instance.Quick_slot_RemoveItem(quick_slot[i].slotnum);
                }

               slot_item.amount = temp_Get_Slotnum_item_amount;
               PlayerQuickSlot.Instance.Quick_slot_AddItem(slot_item); //���� �����Ͽ� ���
                
                return; // �̹� �ִ� �������� �缳���Ͽ����� ���̻� for���� ���� �ʰ� ����
               
            }

     
         }


        PlayerQuickSlot.Instance.Quick_slot_AddItem(slot_item);

        return;


    }

    public void consoleExit()
    {
        Equip_Drop_Selection.gameObject.SetActive(false);
        Consumable_use_Drop_Selection.gameObject.SetActive(false);
        Drop_Input_console.gameObject.SetActive(false);
        ETC_Drop_Selection.gameObject.SetActive(false);

        return;

    }
}

