using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerAbility;

public class PlayerSkillQuickSlot : MonoBehaviour
{

    #region �÷��̾� ��ų ������ ���� ����
    [System.Serializable]
    public class Skill_Quick_Slot_Data
    {
        public List<Skill> skills;

        public Skill_Quick_Slot_Data(List<Skill> player_skill_quick_slot)
        {
            this.skills = player_skill_quick_slot;
        }
    }
    #endregion

   
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

    #region �޼��� ��ų ������ ���� ����
    public void Save_Skill_Quickslot_Info()
    {
        Skill_Quick_Slot_Data data = new Skill_Quick_Slot_Data(quick_slot_skill);
        ES3.Save("Player_skill_quickslot", data);



        Debug.Log("Player_skill_quickslot saved using EasySave3");
    }

    public void Load_Skill_Quickslot_Info()
    {
        if (ES3.KeyExists("Player_skill_quickslot"))
        {
            Skill_Quick_Slot_Data data = ES3.Load<Skill_Quick_Slot_Data>("Player_skill_quickslot");
            quick_slot_skill = data.skills;
            Debug.Log("Player_skill_quickslot loaded using EasySave3");
        }
        else
        {
            Debug.Log("No Player_skill_quickslot data found, creating a new one.");
        }
    }
    #endregion
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
