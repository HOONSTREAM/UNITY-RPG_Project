using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static PlayerInventory;

public class EquipUI : MonoBehaviour
{
    [SerializeField]
    GameObject EquipPanel;
    PlayerEquipment equip; //Equipment 스크립트 참조
    PlayerStat stat; //플레이어 스텟 참조 
    PlayerInventory inven;
    public EquipSlot[] slots;
    public Slot[] Invenslots;
    public Transform slotHolder;
    public Transform InvenslotHolder;

    bool activeEquipment = false;

    void Start()
    {
        
        equip = PlayerEquipment.Instance;
        inven = PlayerInventory.Instance;
        Invenslots = InvenslotHolder.GetComponentsInChildren<Slot>();
        slots = slotHolder.GetComponentsInChildren<EquipSlot>();
        EquipPanel.SetActive(activeEquipment);

        equip.OnequipItem += RedrawSlotUI;


    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            activeEquipment = !activeEquipment;
            EquipPanel.SetActive(activeEquipment);
            
            Managers.Sound.Play("Inven_Open");

        }
    }

    int slotnum = 0;
    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
        }
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }

        
        for (int i = 0; i < inven.player_items.Count; i++)
        {

            slots[i].item = equip.player_equip[i];
            slots[i].UpdateSlotUI();
            
            

        }

       

    }

}
