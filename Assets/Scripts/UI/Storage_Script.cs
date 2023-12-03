using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Storage_Script : MonoBehaviour
{
    [SerializeField]
    private GameObject StoragePanel;
    [SerializeField]
    private GameObject Deposit_amount_panel; //맡기거나, 찾을때 골드 입력
    [SerializeField]
    private GameObject WithDraw_amount_panel;
    [SerializeField]
    private GameObject Storage_Canvas;

    PlayerStorage storage; //플레이어 인벤토리 참조
    PlayerStat stat; //플레이어 스텟 참조 (골드업데이트)



    public Storage_Slots[] storage_slots;
    public Transform storage_slotHolder;
    public Slot[] Player_slots;
    public Transform player_slotHolder;

    bool activestorage = false;



    void Start()
    {
        stat = GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
        storage = PlayerStorage.Instance;
        storage_slots = storage_slotHolder.GetComponentsInChildren<Storage_Slots>();
        Player_slots = player_slotHolder.GetComponentsInChildren<Slot>() ;

        StoragePanel.SetActive(activestorage);
        storage.onChangeItem += RedrawSlotUI;  // Invoke 함수 등록 이벤트 발생마다 함수 호출
        Managers.UI.SetCanvas(Storage_Canvas, true);
        //인벤토리 드래그 가능하도록 하는 이벤트
        UI_Base.BindEvent(StoragePanel, (PointerEventData data) => { StoragePanel.transform.position = data.position; }, Define.UIEvent.Drag);

        #region 슬롯버그방지 슬롯생성후 삭제
        storage.AddItem(ItemDataBase.instance.itemDB[0]);  
        storage.RemoveItem(0);  
        RedrawSlotUI(); 
        #endregion

    }


    public void Enter() 
    {
        activestorage = !activestorage;
        StoragePanel.SetActive(activestorage);
        Managers.Sound.Play("Inven_Open");

        
        for (int i = 0; i < Player_slots.Length; i++)
        {
                Player_slots[i].isStorageMode = activestorage;
        }
        

        return;
    }
    public void PanelExit()
    {
        if (StoragePanel.activeSelf)
        {
            activestorage = !activestorage;
            StoragePanel.SetActive(activestorage);
            Managers.Sound.Play("Inven_Open");

        }

        for (int i = 0; i < Player_slots.Length; i++)
        {
            Player_slots[i].isStorageMode = false;
        }

        return;
    }


    void RedrawSlotUI()
    {
        for (int i = 0; i < storage_slots.Length; i++)
        {
            storage_slots[i].slotnum = i;
        }

        for (int i = 0; i < storage_slots.Length; i++) //싹 밀어버리고
        {
            storage_slots[i].RemoveSlot();
        }

        for (int i = 0; i < storage.storage_item.Count; i++) //리스트배열로 저장되어있는 인벤토리의 아이템정보를 받아와 다시 재정렬 
        {
            storage_slots[i].item = storage.storage_item[i];
            storage_slots[i].UpdateSlotUI();

        }
    }


    public void Deposit()
    {
        Managers.Sound.Play("Coin");

        if(Deposit_amount_panel.activeSelf == false)
        {
            Deposit_amount_panel.SetActive(true);
        }
        
    }

    public void WithDraw()
    {
        Managers.Sound.Play("Coin");

        if (WithDraw_amount_panel.activeSelf == false)
        {
            WithDraw_amount_panel.SetActive(true);
        }

    }



}
