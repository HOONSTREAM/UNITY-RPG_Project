using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class Skill_QuickSlot_Register : MonoBehaviour
{
    public GameObject Register_selection;//������ ���â
    
    public Skill skill_info; // ��ų���Կ� �ش��ϴ� ��ų ����
    public int slot_number; // ��ų�� ���Թ�ȣ ����
    public Ability_Slot[] slots; //�÷��̾� �����Ƽ â ����
    public Skill_Quick_Slot[] skill_quick_slot; //�÷��̾��� ��ų ������ ����
    public Transform skill_quickslot_holder;
    public PlayerStat stat;
    public GameObject Skill_Slot_ui;
  
    public Skill Get_Slotnum(int slotnum) //���Կ� �ִ� ��ų�� �����޾� ������ �����صΰ�, �� ������ �ѹ��� ����
    {
        slot_number = slotnum;
        return skill_info = slots[slotnum].skill;
    }


    void Start()
    {
        GameObject go = Skill_Slot_ui;
        slots = go.GetComponentsInChildren<Ability_Slot>();
        stat = Managers.Game.GetPlayer().gameObject.GetComponent<PlayerStat>();
        skill_quick_slot = skill_quickslot_holder.GetComponentsInChildren<Skill_Quick_Slot>();
    }

    private void Update() // ������ �������� Ű�� ���ȴ��� �˻��ϰ�, �ش� ��ų�� ���
    {

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            skill_info = PlayerSkillQuickSlot.Instance.quick_slot_skill[0];
           
            Quick_Slot_Skill_Use();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            skill_info = PlayerSkillQuickSlot.Instance.quick_slot_skill[1];

            Quick_Slot_Skill_Use();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            skill_info = PlayerSkillQuickSlot.Instance.quick_slot_skill[2];

            Quick_Slot_Skill_Use();
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            skill_info = PlayerSkillQuickSlot.Instance.quick_slot_skill[3];

            Quick_Slot_Skill_Use();
        }

    }

    public void Quick_Slot_Skill_Use()
    {
        if (this.skill_info == null)
        {
            GameObject player = Managers.Game.GetPlayer();
            GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("�����Կ� ��ų�� �����ϴ�.");

            return;
        }
        if (skill_info.skilltype == SkillType.Buff)
        {
            bool isUsed = this.skill_info.Skill_Use();

            if (isUsed)

            {
                //TODO: �������� ������� �� ����
                Ability_Script abs = GameObject.Find("Ability_Slot_CANVAS").gameObject.GetAddComponent<Ability_Script>();
                abs.start_buff_skill(this.skill_info);

            }
            return;
        }

        else if (skill_info.skilltype == SkillType.Active)
        {
            //TODO: �������� ������� �� ����
            return;
        }
    }

    public void RegisterQuickSlot()
    {
        Register_selection.gameObject.SetActive(false);
        Managers.Sound.Play("Coin");



        for (int i = 0; i < skill_quick_slot.Length; i++) //�����Կ� �̹� �ش罺ų�� �ִ��� �˻�
        {
            if (skill_quick_slot[i].skill == skill_info)
            {
                PlayerSkillQuickSlot.Instance.Quick_slot_RemoveSkill(skill_quick_slot[i].slotnum); // �� �ش� �����͸� ���� �����ϰ�
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
