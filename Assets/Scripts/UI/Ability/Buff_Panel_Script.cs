using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Panel_Script : MonoBehaviour
{
    PlayerBuff_Slot player_buff_slot; //플레이어 버프슬롯 참조
    PlayerStat stat; //플레이어 스텟 참조 (골드업데이트)


    public Buff_Slot[] buff_slot;
    public Transform buffslot_holder;
    public Ability_Slot[] Player_Ability_slots;
    public Transform player_Ability_slotHolder;



    void Start()
    {
        stat = GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
        player_buff_slot = PlayerBuff_Slot.Instance;
        buff_slot = buffslot_holder.GetComponentsInChildren<Buff_Slot>();
        Player_Ability_slots = player_Ability_slotHolder.GetComponentsInChildren<Ability_Slot>();

        player_buff_slot.onChangeBuff += RedrawSlotUI;  // Invoke 함수 등록 이벤트 발생마다 함수 호출

        RedrawSlotUI();

    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < buff_slot.Length; i++)
        {
            buff_slot[i].slotnum = i;
        }

        for (int i = 0; i < buff_slot.Length; i++) //싹 밀어버리고
        {
            buff_slot[i].RemoveSlot();
        }

        for (int i = 0; i < player_buff_slot.buff_slot.Count; i++) //리스트배열로 저장되어있는 인벤토리의 아이템정보를 받아와 다시 재정렬 
        {
            buff_slot[i].skill = player_buff_slot.buff_slot[i];
            buff_slot[i].UpdateSlotUI();

        }
    }
}

