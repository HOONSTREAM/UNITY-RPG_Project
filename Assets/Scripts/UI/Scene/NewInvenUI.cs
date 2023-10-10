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
    PlayerInventory inven; //플레이어 인벤토리 참조
    PlayerStat stat; //플레이어 스텟 참조 (골드업데이트)
    

    public Slot[] slots;
    public Transform slotHolder;

    bool activeInventory = false;

    private void Start()
    {
        stat= GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
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

        for (int i = 0; i < slots.Length; i++) //싹 밀어버리고
        {
            slots[i].RemoveSlot();
        }

        for (int i = 0; i < inven.player_items.Count; i++) //재정렬 //TODO 재정렬하는 방식을 없애자 
        {
            slots[i].item = inven.player_items[i];
            slots[i].UpdateSlotUI();
            
        }
    }




}


