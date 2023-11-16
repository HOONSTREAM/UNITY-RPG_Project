using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Storage_Input_Console : MonoBehaviour
{
    public TMP_InputField inputamounttext;
    public int inputamount; // �ñ� ����
    private GameObject canvas; //��ũ��Ʈ�� ��ϵǾ��ִ� ĵ����
    public Slot[] slots; //�÷��̾� ���� ����
    public Item slot_item; // ���Կ� �ش��ϴ� ������ ����
    public int slot_number; // ������ ��ȣ ����
    public GameObject Input_Console; //�ش� ��ǲ�ܼ�
    void Awake()
    {
        canvas = GameObject.Find("GUI").gameObject;
        GameObject go = GameObject.Find("NewInvenUI").gameObject;
        slots = go.GetComponentsInChildren<Slot>();
        
    }

    public Item Get_Slotnum(int slotnum) //���Կ� �ִ� �������� �����޾� private Item ������ �����صΰ�, �� ������ �ѹ��� ����
    {
        slot_number = slotnum;
        Debug.Log($"������ ������ ���Գѹ�{slot_number}�̰�, �������� {slots[slotnum].item.itemname} �Դϴ�.");
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
        inputamounttext.text = " ";

        if (inputamount > slot_item.amount)
        {
            Debug.Log("�ñ���� �������� �����ִ� ������ �����ϴ�.");
            return;
        }

        Debug.Log("�ð���ϴ�.");

        return;
    }
  
}
