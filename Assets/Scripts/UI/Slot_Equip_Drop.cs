using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static CartoonHeroes.SetCharacter;
using static UnityEditor.Progress;

public class Slot_Equip_Drop : MonoBehaviour
{
    #region Class 변수
    public GameObject Equip_Drop_Selection;
    public GameObject Consumable_use_Drop_Selection;
    public GameObject ETC_Drop_Selection;
    public Item slot_item; // 슬롯에 해당하는 아이템 참조
    public int slot_number; // 슬롯의 번호 참조
    public Slot[] slots; //플레이어 슬롯 참조
    public Quick_Slot[] quick_slot; //플레이어의 퀵슬롯 참조
    public Transform quickslot_holder;
    public PlayerStat stat;
    public GameObject Drop_Input_console;
    public int amount; //소모품 판매 갯수
    private int temp_Get_Slotnum_item_amount; // Get_Slotnum 함수를 통해 받아오는 아이템의 갯수를 저장함 (for문돌기에 이용)
    #endregion

    void Start()
    {
        GameObject go = GameObject.Find("NewInvenUI").gameObject; 
        slots = go.GetComponentsInChildren<Slot>();
        stat = Managers.Game.GetPlayer().gameObject.GetComponent<PlayerStat>();
        quick_slot = quickslot_holder.GetComponentsInChildren<Quick_Slot>();
    }

    public Item Get_Slotnum(int slotnum) //슬롯에 있는 아이템을 참조받아 private Item 변수에 저장해두고, 그 슬롯의 넘버도 보관
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
                if (slot_item.Equip) //이미 장착중인경우 다시 누를경우 장착해제
                {
                    PlayerEquipment.Instance.UnEquipItem(slots[slot_number]);
                    slots[slot_number].equiped_image.gameObject.SetActive(false); //장착해제 후 체크표시 해제

                    return;
                }
               
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
            if (slot_item.Equip) //이미 장착중인경우 버릴 수 없음.
            {
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("장착중인 아이템은 버릴 수 없습니다.");
                Equip_Drop_Selection.SetActive(false);
                return;
            }

            else
            {
                PlayerInventory.Instance.RemoveItem(slot_number);
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("아이템을 버립니다.");
                Equip_Drop_Selection.SetActive(false);
            }
        }

        else if(slot_item.itemtype == ItemType.Consumables || slot_item.itemtype == ItemType.SkillBook)
        {
            if (slot_item.amount > 1) //갯수가 1개보다 많은경우 버릴 갯수 조사
            {
                
                Consumable_use_Drop_Selection.SetActive(false);
                Drop_Input_console.SetActive(true); // 버릴갯수를 입력받는 창 열기

            }
            else
            {
                PlayerInventory.Instance.RemoveItem(slot_number);
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("소비 아이템을 버립니다.");
                Consumable_use_Drop_Selection.SetActive(false);
            }
        }

        else // 기타아이템인경우 
        {
            if (slot_item.amount > 1) //갯수가 1개보다 많은경우 버릴 갯수 조사
            {

                ETC_Drop_Selection.SetActive(false);
                Drop_Input_console.SetActive(true); // 버릴갯수를 입력받는 창 열기

            }
            else
            {
                PlayerInventory.Instance.RemoveItem(slot_number);
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("기타 아이템을 버립니다.");              
                ETC_Drop_Selection.SetActive(false);
            }
        }


               
        
    }

    public void RegisterQuickSlot() // 퀵슬롯 등록 함수 
    {
        Consumable_use_Drop_Selection.gameObject.SetActive(false);
        Managers.Sound.Play("Coin");

      
        for (int i = 0; i < quick_slot.Length; i++) //퀵슬롯에 이미 해당아이템이 있는지 검사
        {
            if (quick_slot[i].item == slot_item)
            {
                for(int j = 0; j < temp_Get_Slotnum_item_amount; j++)  // 그 해당아이템 데이터를 전부 삭제하고
                {
                    PlayerQuickSlot.Instance.Quick_slot_RemoveItem(quick_slot[i].slotnum);
                }

               slot_item.amount = temp_Get_Slotnum_item_amount;
               PlayerQuickSlot.Instance.Quick_slot_AddItem(slot_item); //새로 갱신하여 등록
                
                return; // 이미 있는 아이템을 재설정하였으면 더이상 for문을 돌지 않게 설계
               
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

