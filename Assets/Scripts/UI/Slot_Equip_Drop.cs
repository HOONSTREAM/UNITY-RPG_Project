using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Slot_Equip_Drop : MonoBehaviour
{

    public GameObject Equip_Drop_Selection;
    public GameObject Consumable_use_Drop_Selection;
    public Item slot_item; // ���Կ� �ش��ϴ� ������ ����
    public int slot_number; // ������ ��ȣ ����
    public Slot[] slots; //�÷��̾� ���� ����
    public Quick_Slot[] quick_slot; //�÷��̾��� ������ ����
    public Transform quickslot_holder;
    public PlayerStat stat;
    public GameObject Drop_Input_console;
    public int amount; //�Ҹ�ǰ �Ǹ� ����

    void Start()
    {
        GameObject go = GameObject.Find("NewInvenUI").gameObject;
        slots = go.GetComponentsInChildren<Slot>();
        stat = GameObject.Find("UnityChan").gameObject.GetComponent<PlayerStat>();
        quick_slot = quickslot_holder.GetComponentsInChildren<Quick_Slot>();
    }


    void Update()
    {

    }
    public Item Get_Slotnum(int slotnum) //���Կ� �ִ� �������� �����޾� private Item ������ �����صΰ�, �� ������ �ѹ��� ����
    {
        slot_number = slotnum;
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

                else
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
            PlayerInventory.Instance.RemoveItem(slot_number);

            if (slot_item == null)
            {
                return;
            }
        }
    }

    public void Drop()
    {
        if (slot_item.itemtype == ItemType.Equipment)
        {
            if (slot_item.Equip) //�̹� �������ΰ�� ���� �� ����.
            {
                stat.PrintUserText("�������� �������� ���� �� �����ϴ�.");
                Equip_Drop_Selection.SetActive(false);
                return;
            }

            else
            {
                PlayerInventory.Instance.RemoveItem(slot_number);
                stat.PrintUserText("�������� �����ϴ�.");
                Equip_Drop_Selection.SetActive(false);
            }

        }

        else if(slot_item.itemtype == ItemType.Consumables)
        {
            if (slot_item.amount > 1) //������ 1������ ������� ���� ���� ����
            {
                
                Consumable_use_Drop_Selection.SetActive(false);
                Drop_Input_console.SetActive(true); // ���������� �Է¹޴� â ����

            }
            else
            {
                PlayerInventory.Instance.RemoveItem(slot_number);
                stat.PrintUserText("�Һ� �������� �����ϴ�.");
                Consumable_use_Drop_Selection.SetActive(false);
            }
        }


               
        
    }

    public void RegisterQuickSlot()
    {
        Consumable_use_Drop_Selection.gameObject.SetActive(false);
        Managers.Sound.Play("Coin");

        //TODO : �κ��丮�� ���� 

        for(int i = 0; i < quick_slot.Length; i++) //�����Կ� �̹� �ش�������� �ִ��� �˻�
        {
            if (quick_slot[i].item == slot_item)
            {
                PlayerQuickSlot.Instance.Quick_slot_RemoveItem(quick_slot[i].slotnum); // �� �ش������ �����͸� ���� �����ϰ�
            }
        }
             
        PlayerQuickSlot.Instance.Quick_slot_AddItem(slot_item); //���� �����Ͽ� ���
        
    }

    public void consoleExit()
    {
        Equip_Drop_Selection.gameObject.SetActive(false);
        Consumable_use_Drop_Selection.gameObject.SetActive(false);
        Drop_Input_console.gameObject.SetActive(false);

        return;

    }
}

