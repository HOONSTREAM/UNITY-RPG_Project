using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Slot_Equip_Drop : MonoBehaviour
{

    public GameObject Equip_Drop_Selection;
    public Item slot_item; // ���Կ� �ش��ϴ� ������ ����
    public int slot_number; // ������ ��ȣ ����
    public Slot[] slots; //�÷��̾� ���� ����

    void Start()
    {
        GameObject go = GameObject.Find("NewInvenUI").gameObject;
        slots = go.GetComponentsInChildren<Slot>();
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
}

