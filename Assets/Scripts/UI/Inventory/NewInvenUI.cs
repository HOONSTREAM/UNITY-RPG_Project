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

    public bool activeInventory = false;

    private void Start()
    {

        stat = GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
        inven = PlayerInventory.Instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inven.onChangeItem += RedrawSlotUI;
        Managers.UI.SetCanvas(Inventory_canvas, true);
        //인벤토리 드래그 가능하도록 하는 이벤트
        UI_Base.BindEvent(inventoryPanel, (PointerEventData data) => { inventoryPanel.transform.position = data.position; }, Define.UIEvent.Drag);


        TraverseChildrenRecursively(inventoryPanel.transform);


        inventoryPanel.SetActive(activeInventory);
        
        
    }

   private void TraverseChildrenRecursively(Transform parent)
    {
        foreach (Transform child in parent)
        {
            
            TraverseChildrenRecursively(child);    // 재귀적으로 현재 자식의 자식들도 순회
          
            if(child.name == "Content")
            {
                PlayerInventory.Instance._player_Inven_Content = child.gameObject;
            }
        }
    }
        private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.I))
        {

            Inventory_Button_Open();

            if (inventoryPanel.activeSelf == false)
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
        inven_amount_text.text = $"인벤토리 :  {inven_amount.ToString()}/30"; //인벤토리 갯수 업데이트

        
    }

    
    public void Inventory_Button_Open()
    {
        activeInventory = !activeInventory;
        inventoryPanel.SetActive(activeInventory);

        StartCoroutine(After_1second_Update_Inven_Slot());
        Managers.UI.SetCanvas(Inventory_canvas, true);
        RedrawSlotUI();
        Managers.Sound.Play("Inven_Open");
    }
    public void Xbutton_Exit()
    {
        if (inventoryPanel != null && inventoryPanel.activeSelf)
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
            Managers.Sound.Play("Inven_Open");           
            Equip_Drop_Selection.gameObject.SetActive(false);
            Consumable_use_Drop_Selection.gameObject.SetActive(false);

        }

        return;
    }

    /// <summary>
    /// 인스턴스화 시간차에 의하여 아이템 갯수나 장착정보 등이 적용되지 않는현상을 해결하기 위해 1초 대기 후 델리게이트 함수들을 호출합니다.
    /// </summary>
    /// <returns></returns>
    IEnumerator After_1second_Update_Inven_Slot()
    {
        yield return new WaitForSeconds(0.2f);
        PlayerInventory.Instance.onChangeItem.Invoke();
    }
}


