using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Quickslot_Script : MonoBehaviour
{
    PlayerSkillQuickSlot skill_quickslot; //�÷��̾� ��ų ������ ����
    


    public Skill_Quick_Slot[] quick_slot;
    public Transform skill_quickslot_holder;
    public Abillity_Slot[] abillity_slots;
    public Transform abillity_slotHolder;


    void Start()
    {
        skill_quickslot = PlayerSkillQuickSlot.Instance;
        quick_slot = skill_quickslot_holder.GetComponentsInChildren<Skill_Quick_Slot>();
        abillity_slots = abillity_slotHolder.GetComponentsInChildren<Abillity_Slot>();

        skill_quickslot.onChangeskill_quickslot += RedrawSlotUI;  // Invoke �Լ� ��� �̺�Ʈ �߻����� �Լ� ȣ��

        RedrawSlotUI();
    }

   

    void RedrawSlotUI()
    {
        for (int i = 0; i < quick_slot.Length; i++)
        {
            quick_slot[i].slotnum = i;
        }

        for (int i = 0; i < quick_slot.Length; i++) 
        {
            quick_slot[i].RemoveSlot();
        }

        for (int i = 0; i < skill_quickslot.quick_slot_skill.Count; i++)
        {
            quick_slot[i].skill = skill_quickslot.quick_slot_skill[i];
            quick_slot[i].UpdateSlotUI();

        }
    }
}
