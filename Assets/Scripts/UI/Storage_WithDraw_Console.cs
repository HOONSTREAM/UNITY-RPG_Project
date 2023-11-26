using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Storage_WithDraw_Console : MonoBehaviour
{
    public TMP_InputField inputamounttext;
    public int inputamount = 0; // �ñ� ����
    private GameObject canvas; //��ũ��Ʈ�� ��ϵǾ��ִ� ĵ����
    public Storage_Slots[] slots; //�÷��̾� ���� ����
    public Item slot_item; // ���Կ� �ش��ϴ� ������ ����
    public int slot_number; // ������ ��ȣ ����
    public GameObject Input_Console; //�ش� ��ǲ�ܼ�

    void Awake()
    {
        canvas = GameObject.Find("Storage CANVAS").gameObject;
        GameObject go = GameObject.Find("StorageUI").gameObject;
        slots = go.GetComponentsInChildren<Storage_Slots>();

    }

    public Item Get_Slotnum(int slotnum) //���Կ� �ִ� �������� �����޾� private Item ������ �����صΰ�, �� ������ �ѹ��� ����
    {
        slot_number = slotnum;
        Debug.Log($"������ ������ ���Գѹ�{slot_number}�̰�, �������� {slots[slotnum].item.itemname} �Դϴ�.");
        Debug.Log($"������ �������� amount : {slot_item.amount}");

        return slot_item = slots[slotnum].item;

    }

    public void ButtonInput()
    {
        Input_Console.SetActive(false);

        if (slot_item == null)
        {
            Debug.Log("slot_item�� null�� �Դϴ�.");
            return;
        }


        inputamount = int.Parse(inputamounttext.text);

        Debug.Log($"�ܼ�â�� �Էµ� ���� :{inputamount} ");

        if (inputamount > slot_item.amount)
        {
            GameObject go = GameObject.Find("UnityChan").gameObject;
            PlayerStat stat = go.GetComponent<PlayerStat>();
            stat.PrintUserText("ã������ ������ ���� �������� ��ǰ ������ �����ϴ�.");
            return;
        }

        //ó�� ������ ������ ������ �߰��Ǽ� ���� ���� ���� �Ϸ� (Clone �Լ� amount�� �״�� �������°��� �ƴ� ���纻�̹Ƿ� 1���� ����) 

        for (int i = 0; i < inputamount; i++) // Ŭ���Լ�(�������� ��������)�� �̿��Ͽ� �Էµ� ������ŭ â�� ����
        {           
            PlayerInventory.Instance.AddItem(slot_item.Clone());
        }
        for (int i = 0; i < inputamount; i++) // �Էµ� ������ŭ �κ��丮���� ����
        {          
            PlayerStorage.Instance.RemoveItem(slot_number);
        }


        inputamount = 0; // ����� ���� �ʱ�ȭ
        inputamounttext.text = " ";

        return;

    }
}
