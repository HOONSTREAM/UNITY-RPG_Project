using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Storage_Script : MonoBehaviour
{
    [SerializeField]
    private GameObject StoragePanel;

    PlayerStorage storage; //플레이어 인벤토리 참조
    PlayerStat stat; //플레이어 스텟 참조 (골드업데이트)

    public Slot[] slots;
    public Transform slotHolder;

    bool activestorage = false;



    void Start()
    {
        stat = GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
        storage = PlayerStorage.Instance; 
        slots = slotHolder.GetComponentsInChildren<Slot>();

        StoragePanel.SetActive(activestorage);
        storage.onChangeItem += RedrawSlotUI;  

        //인벤토리 드래그 가능하도록 하는 이벤트
        UI_Base.BindEvent(StoragePanel, (PointerEventData data) => { StoragePanel.transform.position = data.position; }, Define.UIEvent.Drag);

        #region 슬롯버그방지 슬롯생성후 삭제
        storage.AddItem(ItemDataBase.instance.itemDB[0]);  // PlayerStorage 스크립트 만들어야함
        storage.RemoveItem(0);  // PlayerStorage 스크립트 만들어야함
        RedrawSlotUI(); 
        #endregion

    }


    void Update()
    {
        //TEST

        if (Input.GetKeyDown(KeyCode.S))
        {
            activestorage = !activestorage;
            StoragePanel.SetActive(activestorage);
            Managers.Sound.Play("Inven_Open");
        }
    }

   
    public void PanelExit()
    {
        if (StoragePanel.activeSelf)
        {
            StoragePanel.SetActive(false);
            Managers.Sound.Play("Inven_Open");
        }

        return;
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

        for (int i = 0; i < storage.storage_item.Count; i++) //리스트배열로 저장되어있는 인벤토리의 아이템정보를 받아와 다시 재정렬 
        {
            slots[i].item = storage.storage_item[i];
            slots[i].UpdateSlotUI();

        }
    }



}
