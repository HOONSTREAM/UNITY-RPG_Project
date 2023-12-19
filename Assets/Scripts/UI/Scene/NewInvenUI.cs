using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewInvenUI : MonoBehaviour
{
    
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private GameObject Inventory_canvas;
    private PlayerInventory inven; //플레이어 인벤토리 참조
    private PlayerStat stat; //플레이어 스텟 참조 (골드업데이트)
    public TextMeshProUGUI inven_amount_text; // 인벤토리 칸 텍스트
    public int inven_amount; //인벤토리 칸 참조


    public GameObject Equip_Drop_Selection; // 장비품 클릭시 뜨는 콘솔 참조 (장비창 종료시 같이 초기화 목적)
    public GameObject Consumable_use_Drop_Selection; // 소모품 클릭시 뜨는 콘솔 참조 (장비창 종료시 같이 초기화 목적)


    public Slot[] slots;
    public Transform slotHolder;

    bool activeInventory = false;

    private void Start()
    {
        
        stat = GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
        inven = PlayerInventory.Instance;       
        slots= slotHolder.GetComponentsInChildren<Slot>();    
        inventoryPanel.SetActive(activeInventory);
        inven.onChangeItem += RedrawSlotUI;
        Managers.UI.SetCanvas(Inventory_canvas, true);
        //인벤토리 드래그 가능하도록 하는 이벤트
        UI_Base.BindEvent(inventoryPanel, (PointerEventData data) => { inventoryPanel.transform.position = data.position; }, Define.UIEvent.Drag);

        //TEST

        

    }

    private void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.I))
        {
            
            activeInventory = !activeInventory;           
            inventoryPanel.SetActive(activeInventory);
            Managers.Sound.Play("Inven_Open");

            if(inventoryPanel.activeSelf == false)
            {
                Equip_Drop_Selection.gameObject.SetActive(false);
                Consumable_use_Drop_Selection.gameObject.SetActive(false);
                
            }
            
        }
       
    }

    void RedrawSlotUI()
    {
        
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
        }

        for (int i = 0; i < slots.Length; i++) //싹 밀어버리고
        {
            slots[i].RemoveSlot();
        }

        for (int i = 0; i < inven.player_items.Count; i++) //리스트배열로 저장되어있는 인벤토리의 아이템정보를 받아와 다시 재정렬 
        {
            slots[i].item = inven.player_items[i];
            slots[i].UpdateSlotUI();
            
        }

        inven_amount = inven.player_items.Count;
        inven_amount_text.text = $"갯수 :  {inven_amount.ToString()}/20"; //인벤토리 갯수 업데이트
    }




}


