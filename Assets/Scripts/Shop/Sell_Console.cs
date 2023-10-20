using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Sell_Console : MonoBehaviour
{
    public GameObject SellConsole; // �Ǹ��� ������ Ȯ���ϴ� �ܼ�â
    public GameObject Amount_InputConsole; // ��� �Ǹ��� ������ Ȯ���ϴ� �ܼ�â
    public Slot[] slots; //�÷��̾� ���� ����
    public Item slot_item; // ���Կ� �ش��ϴ� ������ ����
    public int slot_number; // ������ ��ȣ ����

    public int amount = 0; //�Ǹ��� ���� 

    private void Awake()
    {
        GameObject go = GameObject.Find("NewInvenUI").gameObject;
        slots = go.GetComponentsInChildren<Slot>();
    }
    public Item Get_Slotnum(int slotnum) //���Կ� �ִ� �������� �����޾� private Item ������ �����صΰ�, �� ������ �ѹ��� ����
    {
        slot_number = slotnum;
        return slot_item = slots[slotnum].item;       
    }
   

    public void Sell_Item() //TODO : �Ҹ�ǰ�ΰ�� ������ �����.. (Inputfield Ȱ��)
    {
        SellConsole.SetActive(false);
        if(slot_item.itemtype == ItemType.Consumables && slot_item.amount >1)
        {
            Amount_InputConsole.SetActive(true);
       
            return;
        }
        //ĳ���� ������Ʈ�� ã�� �÷��̾� ���� ��ũ��Ʈ�� �����Ͽ� ��� ���� 
        GameObject go = GameObject.Find("UnityChan").gameObject;
        PlayerStat stat = go.GetComponent<PlayerStat>();
        stat.Gold += slot_item.sellprice;
        stat.PrintUserText($"������ �Ǹ��Ͽ�{slot_item.sellprice}��带 ������ϴ�.");
        PlayerInventory.Instance.RemoveItem(slot_number);

    }

    public void Not_Sell_Item()
    {     
        SellConsole.SetActive(false);
    }

}
