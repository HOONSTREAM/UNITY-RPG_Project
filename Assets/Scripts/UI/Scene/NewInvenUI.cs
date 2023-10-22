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
        

        stat = GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
        inven = PlayerInventory.Instance;       
        slots= slotHolder.GetComponentsInChildren<Slot>();
        
        inventoryPanel.SetActive(activeInventory);
        inven.onChangeItem += RedrawSlotUI;

        //�κ��丮 �巡���̺�Ʈ 
        UI_Base.BindEvent(inventoryPanel, (PointerEventData data) => { inventoryPanel.transform.position = data.position; }, Define.UIEvent.Drag);

        #region ���Թ��׹��� ���Ի���
        inven.AddItem(ItemDataBase.instance.itemDB[0]);
        inven.RemoveItem(0);
        RedrawSlotUI();
        #endregion



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

        for (int i = 0; i < inven.player_items.Count; i++) //����Ʈ�迭�� ����Ǿ��ִ� �κ��丮�� ������������ �޾ƿ� �ٽ� ������ 
        {
            slots[i].item = inven.player_items[i];
            slots[i].UpdateSlotUI();
            
        }
    }




}


