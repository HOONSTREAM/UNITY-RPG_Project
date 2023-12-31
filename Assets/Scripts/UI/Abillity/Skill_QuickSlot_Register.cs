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
    public GameObject Register_selection;//퀵슬롯 등록창
    
    public Skill skill_info; // 스킬슬롯에 해당하는 스킬 참조
    public int slot_number; // 스킬의 슬롯번호 참조
    public Abillity_Slot[] slots; //플레이어 어빌리티 창 참조
    public Skill_Quick_Slot[] skill_quick_slot; //플레이어의 스킬 퀵슬롯 참조
    public Transform skill_quickslot_holder;
    public PlayerStat stat;

  
    public Skill Get_Slotnum(int slotnum) //슬롯에 있는 스킬을 참조받아 변수에 저장해두고, 그 슬롯의 넘버도 보관
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
            //TODO : 스킬 사용 후 로직 (버프스킬 상단표시)

            Register_selection.SetActive(false);

            if(skill_info.skilltype == SkillType.Buff)
            {
                Abillity_Script abs = GameObject.Find("Abillity_Slot_CANVAS ").gameObject.GetAddComponent<Abillity_Script>();
                abs.start_buff_skill(skill_info);// 버프스킬 지속시간 타이머 UI 함수 호출
            }

            return;
        }
        else if (!isUse)
        {
            Register_selection.SetActive(false); // 콘솔창만 종료하고 함수 종료
            return;
        }

        return;

    }
    public void RegisterQuickSlot()
    {
        Register_selection.gameObject.SetActive(false);
        Managers.Sound.Play("Coin");



        for (int i = 0; i < skill_quick_slot.Length; i++) //퀵슬롯에 이미 해당아이템이 있는지 검사
        {
            if (skill_quick_slot[i].skill == skill_info)
            {
                PlayerSkillQuickSlot.Instance.Quick_slot_RemoveSkill(skill_quick_slot[i].slotnum); // 그 해당아이템 데이터를 전부 삭제하고
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
