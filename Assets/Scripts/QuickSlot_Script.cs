using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class QuickSlot_Script : MonoBehaviour
{
    PlayerQuickSlot quickslot; //�÷��̾� ������ ����
    PlayerStat stat; //�÷��̾� ���� ���� (��������Ʈ)


    public Quick_Slot[] quick_slot;
    public Transform quickslot_holder;
    public Slot[] Player_slots;
    public Transform player_slotHolder;

   

    void Start()
    {
        stat = GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
        quickslot = PlayerQuickSlot.Instance;
        quick_slot = quickslot_holder.GetComponentsInChildren<Quick_Slot>();
        Player_slots = player_slotHolder.GetComponentsInChildren<Slot>();

        quickslot.onChangeItem += RedrawSlotUI;  // Invoke �Լ� ��� �̺�Ʈ �߻����� �Լ� ȣ��

        #region ���Թ��׹��� ���Ի����� ����
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

        for (int i = 0; i < quick_slot.Length; i++) //�� �о������
        {
            quick_slot[i].RemoveSlot();
        }

        for (int i = 0; i < quickslot.quick_slot_item.Count; i++) //����Ʈ�迭�� ����Ǿ��ִ� �κ��丮�� ������������ �޾ƿ� �ٽ� ������ 
        {
            quick_slot[i].item = quickslot.quick_slot_item[i];
            quick_slot[i].UpdateSlotUI();

        }
    }
    void Update()
    {
        
    }
}
