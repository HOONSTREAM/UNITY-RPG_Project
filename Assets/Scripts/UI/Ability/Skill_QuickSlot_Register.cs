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
    public GameObject Register_selection;//퀵슬롯 등록창
    
    public Skill skill_info; // 스킬슬롯에 해당하는 스킬 참조
    public int slot_number; // 스킬의 슬롯번호 참조
    public Ability_Slot[] slots; //플레이어 어빌리티 창 참조
    public Skill_Quick_Slot[] skill_quick_slot; //플레이어의 스킬 퀵슬롯 참조
    public Transform skill_quickslot_holder;
    public PlayerStat stat;
    public GameObject Skill_Slot_ui;
  
    public Skill Get_Slotnum(int slotnum) //슬롯에 있는 스킬을 참조받아 변수에 저장해두고, 그 슬롯의 넘버도 보관
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

    private void Update() // 실제로 퀵슬롯의 키가 눌렸는지 검사하고, 해당 스킬을 사용
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
            GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("퀵슬롯에 스킬이 없습니다.");

            return;
        }
        if (skill_info.skilltype == SkillType.Buff)
        {
            bool isUsed = this.skill_info.Skill_Use();

            if (isUsed)

            {
                //TODO: 퀵슬롯을 사용했을 때 로직
                Ability_Script abs = GameObject.Find("Ability_Slot_CANVAS").gameObject.GetAddComponent<Ability_Script>();
                abs.start_buff_skill(this.skill_info);

            }
            return;
        }

        else if (skill_info.skilltype == SkillType.Active)
        {
            //TODO: 퀵슬롯을 사용했을 때 로직
            return;
        }
    }

    public void RegisterQuickSlot()
    {
        Register_selection.gameObject.SetActive(false);
        Managers.Sound.Play("Coin");



        for (int i = 0; i < skill_quick_slot.Length; i++) //퀵슬롯에 이미 해당스킬이 있는지 검사
        {
            if (skill_quick_slot[i].skill == skill_info)
            {
                PlayerSkillQuickSlot.Instance.Quick_slot_RemoveSkill(skill_quick_slot[i].slotnum); // 그 해당 데이터를 전부 삭제하고
            }
        }

        PlayerSkillQuickSlot.Instance.Quick_slot_AddSkill(skill_info); //새로 갱신하여 등록

    }


    public void consoleExit()
    {
        Register_selection.gameObject.SetActive(false);
        
        return;

    }
}
