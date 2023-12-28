using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Panel_Script : MonoBehaviour
{
    PlayerBuff_Slot player_buff_slot; //�÷��̾� �������� ����
    PlayerStat stat; //�÷��̾� ���� ���� (��������Ʈ)


    public Buff_Slot[] buff_slot;
    public Transform buffslot_holder;
    public Abillity_Slot[] Player_Abillity_slots;
    public Transform player_abillity_slotHolder;



    void Start()
    {
        stat = GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
        player_buff_slot = PlayerBuff_Slot.Instance;
        buff_slot = buffslot_holder.GetComponentsInChildren<Buff_Slot>();
        Player_Abillity_slots = player_abillity_slotHolder.GetComponentsInChildren<Abillity_Slot>();

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

