using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Slot_Equip_Drop : MonoBehaviour
{

    public GameObject Equip_Drop_Selection;
    public Item slot_item; // 슬롯에 해당하는 아이템 참조
    public int slot_number; // 슬롯의 번호 참조
    public Slot[] slots; //플레이어 슬롯 참조

    void Start()
    {
        GameObject go = GameObject.Find("NewInvenUI").gameObject;
        slots = go.GetComponentsInChildren<Slot>();
    }


    void Update()
    {

    }
    public Item Get_Slotnum(int slotnum) //슬롯에 있는 아이템을 참조받아 private Item 변수에 저장해두고, 그 슬롯의 넘버도 보관
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
                if (slot_item.Equip) //이미 장착중인경우 다시 누를경우 장착해제
                {
                    PlayerEquipment.Instance.UnEquipItem(slots[slot_number]);
                    slots[slot_number].equiped_image.gameObject.SetActive(false); //장착해제 후 체크표시 해제

                    return;
                }

                else
                {
                    PlayerEquipment.Instance.EquipItem(slots[slot_number]); //아이템 장착 함수
                    if (slot_item.Equip)
                    {
                        slots[slot_number].equiped_image.gameObject.SetActive(true); //체크표시

                    }
                    else //장착할 수 없을경우 (부위가 겹칠경우)
                    {
                        slots[slot_number].equiped_image.gameObject.SetActive(false); //체크표시 안함
                    }

                }

            }         

        }

    }
}

