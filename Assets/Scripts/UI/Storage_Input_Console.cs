using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Storage_Input_Console : MonoBehaviour
{
    public TMP_InputField inputamounttext;
    public int inputamount = 0; // �ñ� ����
    private GameObject canvas; //��ũ��Ʈ�� ��ϵǾ��ִ� ĵ����
    public Slot[] slots; //�÷��̾� ���� ����
    public Item slot_item; // ���Կ� �ش��ϴ� ������ ����
    public int slot_number; // ������ ��ȣ ����
    public GameObject Input_Console; //�ش� ��ǲ�ܼ�
    void Awake()
    {
        canvas = GameObject.Find("GUI_User_Interface").gameObject;
        GameObject go = GameObject.Find("NewInvenUI").gameObject;
        slots = go.GetComponentsInChildren<Slot>();
        
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
    }
    public Item Get_Slotnum(int slotnum) //���Կ� �ִ� �������� �����޾� private Item ������ �����صΰ�, �� ������ �ѹ��� ����
    {
        slot_number = slotnum;     
        return slot_item = slots[slotnum].item;
    }

    public void ButtonInput()
    {
        Input_Console.SetActive(false);

        if (slot_item == null)
        {          
            return;
        }
     
        inputamount = int.Parse(inputamounttext.text);

        
        if (inputamount > slot_item.amount)
        {
            GameObject player = Managers.Game.GetPlayer();
            Print_Info_Text.Instance.PrintUserText("�ñ���� ������ ���� �������� ��ǰ ������ �����ϴ�.");
            return;
        }


        for(int i = 0; i< inputamount; i++) // Ŭ���Լ�(�������� ��������)�� �̿��Ͽ� �Էµ� ������ŭ â�� ����
        {
           
            PlayerStorage.Instance.AddItem(slot_item.Clone());           
        }
        for(int i = 0; i<inputamount; i++) // �Էµ� ������ŭ �κ��丮���� ����
        {
            
            PlayerInventory.Instance.RemoveItem(slot_number);         
        }


        inputamount = 0; // ����� ���� �ʱ�ȭ
        inputamounttext.text = " ";

        return;

    }
  
}
