using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class Skill_QuickSlot_Register : MonoBehaviour
{
    public GameObject Register_selection;//������ ���â
    
    public Skill skill_info; // ��ų���Կ� �ش��ϴ� ��ų ����
    public int slot_number; // ��ų�� ���Թ�ȣ ����
    public Abillity_Slot[] slots; //�÷��̾� �����Ƽ â ����
    public Skill_Quick_Slot[] skill_quick_slot; //�÷��̾��� ��ų ������ ����
    public Transform skill_quickslot_holder;
    public PlayerStat stat;

  
    public Skill Get_Slotnum(int slotnum) //���Կ� �ִ� ��ų�� �����޾� ������ �����صΰ�, �� ������ �ѹ��� ����
    {
        slot_number = slotnum;
        return skill_info = slots[slotnum].skill;
    }


    void Start()
    {
        GameObject go = GameObject.Find("Skill_Slot_UI").gameObject;
        slots = go.GetComponentsInChildren<Abillity_Slot>();
        stat = Managers.Game.GetPlayer().gameObject.GetComponent<PlayerStat>();
        skill_quick_slot = skill_quickslot_holder.GetComponentsInChildren<Skill_Quick_Slot>();
    }

    public void Skill_Use()
    {
        bool isUse = skill_info.Skill_Use();

        if (isUse)
        {
            //TODO : ��ų ��� �� ���� (������ų ���ǥ��)

            Register_selection.SetActive(false);

            if(skill_info.skilltype == SkillType.Buff)
            {
                Abillity_Script abs = GameObject.Find("Abillity_Slot_CANVAS ").gameObject.GetAddComponent<Abillity_Script>();
                abs.start_buff_skill(skill_info);// ������ų ���ӽð� Ÿ�̸� UI �Լ� ȣ��
            }

            return;
        }
        else if (!isUse)
        {
            Register_selection.SetActive(false); // �ܼ�â�� �����ϰ� �Լ� ����
            return;
        }

        return;

    }
    public void RegisterQuickSlot()
    {
        Register_selection.gameObject.SetActive(false);
        Managers.Sound.Play("Coin");



        for (int i = 0; i < skill_quick_slot.Length; i++) //�����Կ� �̹� �ش�������� �ִ��� �˻�
        {
            if (skill_quick_slot[i].skill == skill_info)
            {
                PlayerSkillQuickSlot.Instance.Quick_slot_RemoveSkill(skill_quick_slot[i].slotnum); // �� �ش������ �����͸� ���� �����ϰ�
            }
        }

        PlayerSkillQuickSlot.Instance.Quick_slot_AddSkill(skill_info); //���� �����Ͽ� ���

    }


    public void consoleExit()
    {
        Register_selection.gameObject.SetActive(false);
        
        return;

    }
}
