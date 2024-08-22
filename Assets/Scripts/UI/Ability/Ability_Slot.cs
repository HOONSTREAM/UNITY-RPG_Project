using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Ability_Slot : MonoBehaviour , IPointerUpHandler
{
    public int slotnum;

    private const float Ability_INTERMEDIATE_LEVEL = 50.00f;
    private const float Ability_MASTER_LEVEL = 100.00f;

    public GameObject Skill_Quickslot_Panel;
    public GameObject Ability_Grade_Total_Info;
    public Skill skill;
    public Image skill_icon;
    public TextMeshProUGUI skill_name;
    public TextMeshProUGUI grade_amount;
    public TextMeshProUGUI LEVEL;
    public TextMeshProUGUI Name_grade;

    [SerializeField]
    public Slider _slider;

  
    public void UpdateSlotUI()
    {
        if(skill.skilltype == SkillType.Active || skill.skilltype == SkillType.Buff)
        {
            Ability_Grade_Total_Info.gameObject.SetActive(false);
        }

        skill_icon.sprite = skill.skill_image;
        skill_name.text = skill.skill_name;
        skill_icon.gameObject.SetActive(true);
        LEVEL.text = skill.Ability.ToString();
        
        if(skill.Ability >= 50.00f)
        {
            Name_grade.text = "SENIOR";
        }
        else if(skill.Ability >= 100.00f)
        {
            Name_grade.text = "MASTER";
            
        }

        grade_amount.text = skill.Ability_Grade.ToString();

        

        return;
    }
    public void RemoveSlot()
    {
        skill = null;
        skill_icon.gameObject.SetActive(false); //초기화 (아이콘 표시 안함)
        skill_name.text = "";
        
        return;
    }


    public void OnPointerUp(PointerEventData eventData)
    {

        if (this.skill.skilltype == SkillType.Ability) // 어빌리티 인경우
        {
            //TODO : 100이 넘었을 경우 그레이드 과정 등록

            return;
        }
        else if(this.skill.skilltype == SkillType.Buff) 
        { 
                     
            Ability_Script abs = GameObject.Find("Ability_Slot_CANVAS").gameObject.GetComponent<Ability_Script>();
            Skill_QuickSlot_Register quick_slot = abs.GetComponent<Skill_QuickSlot_Register>();
            abs.Get_Slotnum(slotnum);  //slot에 대한 정보를 넘겨줌
            quick_slot.Get_Slotnum(slotnum); //slot에 대한 정보를 넘겨줌
            Skill_Quickslot_Panel.SetActive(true);
            Skill_Quickslot_Panel.transform.position = Input.mousePosition;

        }

        else if(this.skill.skilltype == SkillType.Active)
        {
            Ability_Script abs = GameObject.Find("Ability_Slot_CANVAS").gameObject.GetComponent<Ability_Script>();
            Skill_QuickSlot_Register quick_slot = abs.GetComponent<Skill_QuickSlot_Register>();
            abs.Get_Slotnum(slotnum);  //slot에 대한 정보를 넘겨줌
            quick_slot.Get_Slotnum(slotnum); //slot에 대한 정보를 넘겨줌
            Skill_Quickslot_Panel.SetActive(true);
            Skill_Quickslot_Panel.transform.position = Input.mousePosition;
            
        }

    }

}
