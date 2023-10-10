using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewInvenUI : MonoBehaviour
{
    
    [SerializeField]
    GameObject inventoryPanel;
    PlayerInventory inven; //�÷��̾� �κ��丮 ����
    PlayerStat stat; //�÷��̾� ���� ���� (��������Ʈ)
    

    public Slot[] slots;
    public Transform slotHolder;

    bool activeInventory = false;

    private void Start()
    {
        stat= GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
        inven = PlayerInventory.Instance;
       
        slots= slotHolder.GetComponentsInChildren<Slot>();
        
        inventoryPanel.SetActive(activeInventory);
        
        inven.onChangeItem += RedrawSlotUI;

        

    }

   


    private void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
            Managers.Sound.Play("Inven_Open");

        }
       
    }

    void RedrawSlotUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
        }

        for (int i = 0; i < slots.Length; i++) //�� �о������
        {
            slots[i].RemoveSlot();
        }

        for (int i = 0; i < inven.player_items.Count; i++) //������ //TODO �������ϴ� ����� ������ 
        {
            slots[i].item = inven.player_items[i];
            slots[i].UpdateSlotUI();
            
        }
    }




}


