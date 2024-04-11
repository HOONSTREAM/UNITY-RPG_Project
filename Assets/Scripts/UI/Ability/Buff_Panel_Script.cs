using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Panel_Script : MonoBehaviour
{
    PlayerBuff_Slot player_buff_slot; //�÷��̾� �������� ����
    PlayerStat stat; //�÷��̾� ���� ���� (��������Ʈ)


    public Buff_Slot[] buff_slot;
    public Transform buffslot_holder;
    public Ability_Slot[] Player_Ability_slots;
    public Transform player_Ability_slotHolder;



    void Start()
    {
        stat = GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
        player_buff_slot = PlayerBuff_Slot.Instance;
        buff_slot = buffslot_holder.GetComponentsInChildren<Buff_Slot>();
        Player_Ability_slots = player_Ability_slotHolder.GetComponentsInChildren<Ability_Slot>();

        player_buff_slot.onChangeBuff += RedrawSlotUI;  // Invoke �Լ� ��� �̺�Ʈ �߻����� �Լ� ȣ��

        RedrawSlotUI();

    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < buff_slot.Length; i++)
        {
            buff_slot[i].slotnum = i;
        }

        for (int i = 0; i < buff_slot.Length; i++) //�� �о������
        {
            buff_slot[i].RemoveSlot();
        }

        for (int i = 0; i < player_buff_slot.buff_slot.Count; i++) //����Ʈ�迭�� ����Ǿ��ִ� �κ��丮�� ������������ �޾ƿ� �ٽ� ������ 
        {
            buff_slot[i].skill = player_buff_slot.buff_slot[i];
            buff_slot[i].UpdateSlotUI();

        }
    }
}

