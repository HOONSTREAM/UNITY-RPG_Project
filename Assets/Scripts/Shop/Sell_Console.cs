using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Sell_Console : MonoBehaviour
{
    public GameObject SellConsole; // 판매할 것인지 확인하는 콘솔창
    public GameObject Amount_InputConsole; // 몇개를 판매할 것인지 확인하는 콘솔창
    public Slot[] slots; //플레이어 슬롯 참조
    public Item slot_item; // 슬롯에 해당하는 아이템 참조
    public int slot_number; // 슬롯의 번호 참조

    public int amount = 0; //판매할 갯수 

    private void Awake()
    {
        GameObject go = GameObject.Find("NewInvenUI").gameObject;
        slots = go.GetComponentsInChildren<Slot>();
    }
    public Item Get_Slotnum(int slotnum) //슬롯에 있는 아이템을 참조받아 private Item 변수에 저장해두고, 그 슬롯의 넘버도 보관
    {
        slot_number = slotnum;
        return slot_item = slots[slotnum].item;       
    }
   

    public void Sell_Item() //TODO : 소모품인경우 갯수를 물어보고.. (Inputfield 활용)
    {
        SellConsole.SetActive(false);
        if(slot_item.itemtype == ItemType.Consumables && slot_item.amount >1)
        {
            Amount_InputConsole.SetActive(true);
       
            return;
        }
        //캐릭터 오브젝트를 찾아 플레이어 스텟 스크립트를 참조하여 골드 증가 
        GameObject go = GameObject.Find("UnityChan").gameObject;
        PlayerStat stat = go.GetComponent<PlayerStat>();
        stat.Gold += slot_item.sellprice;
        stat.PrintUserText($"상점에 판매하여{slot_item.sellprice}골드를 얻었습니다.");
        PlayerInventory.Instance.RemoveItem(slot_number);

    }

    public void Not_Sell_Item()
    {     
        SellConsole.SetActive(false);
    }

}
