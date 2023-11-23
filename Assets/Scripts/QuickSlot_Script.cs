using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class QuickSlot_Script : MonoBehaviour
{
    PlayerQuickSlot quickslot; //플레이어 퀵슬롯 참조
    PlayerStat stat; //플레이어 스텟 참조 (골드업데이트)


    public Quick_Slot[] quick_slot;
    public Transform quickslot_holder;
    public Slot[] Player_slots;
    public Transform player_slotHolder;

   

    void Start()
    {
        stat = GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
        quickslot = PlayerQuickSlot.Instance;
        quick_slot = quickslot_holder.GetComponentsInChildren<Quick_Slot>();
        Player_slots = player_slotHolder.GetComponentsInChildren<Slot>();

        quickslot.onChangeItem += RedrawSlotUI;  // Invoke 함수 등록 이벤트 발생마다 함수 호출

        #region 슬롯버그방지 슬롯생성후 삭제
        quickslot.AddItem(ItemDataBase.instance.itemDB[0]);
        quickslot.RemoveItem(0);
        RedrawSlotUI();
        #endregion


    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < quick_slot.Length; i++)
        {
            quick_slot[i].slotnum = i;
        }

        for (int i = 0; i < quick_slot.Length; i++) //싹 밀어버리고
        {
            quick_slot[i].RemoveSlot();
        }

        for (int i = 0; i < quickslot.quick_slot_item.Count; i++) //리스트배열로 저장되어있는 인벤토리의 아이템정보를 받아와 다시 재정렬 
        {
            quick_slot[i].item = quickslot.quick_slot_item[i];
            quick_slot[i].UpdateSlotUI();

        }
    }
    void Update()
    {
        
    }
}
