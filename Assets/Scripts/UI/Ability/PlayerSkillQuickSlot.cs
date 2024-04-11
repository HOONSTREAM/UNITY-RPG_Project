using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillQuickSlot : MonoBehaviour
{
    public static PlayerSkillQuickSlot Instance;

    PlayerStat stat;
    public List<Skill> quick_slot_skill;
    public Skill_Quick_Slot slot;

    public delegate void OnChangeSkillQuickslot();
    public OnChangeSkillQuickslot onChangeskill_quickslot;


    private void Awake()
    {
        Instance = this;
        quick_slot_skill = new List<Skill>();
        stat = GetComponent<PlayerStat>();
        slot = GetComponent<Skill_Quick_Slot>();

    }


    public bool Quick_slot_AddSkill(Skill _skill, int index = 0)
    {

        if (quick_slot_skill.Count == 4)
        {
            quick_slot_skill.RemoveAt(0); //�� �տ��ִ� ������ �о��.
            PlayerSkillQuickSlot.Instance.onChangeskill_quickslot.Invoke();
        }
        quick_slot_skill.Add(_skill); //clone �Լ� �����ʰ� ���� �������� �����ؾ��Ѵ�. (Clone�Լ� �����������)
        onChangeskill_quickslot.Invoke();

        return true;

    }

    public void Quick_slot_RemoveSkill(int index)
    {

        if (quick_slot_skill[index] == null)
        {
            return;
        }


        quick_slot_skill.RemoveAt(index);
        onChangeskill_quickslot.Invoke();


        return;
    }
}
