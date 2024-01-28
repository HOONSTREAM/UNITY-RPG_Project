using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Abillity_Slot : MonoBehaviour , IPointerUpHandler
{
    public int slotnum;

    public GameObject Skill_Quickslot_Panel;
    public Skill skill;
    public Image skill_icon;
    public TextMeshProUGUI skill_name;
    public TextMeshProUGUI grade_amount;
    public TextMeshProUGUI Level;
    public TextMeshProUGUI Name_grade;

    [SerializeField]
    public Slider _slider;

  
    public void UpdateSlotUI()
    {
        skill_icon.sprite = skill.skill_image;
        skill_name.text = skill.skill_name;
        skill_icon.gameObject.SetActive(true);

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

        if (this.skill.skilltype == SkillType.Abillity) // 어빌리티 인경우
        {
            //TODO : 100이 넘었을 경우 그레이드 과정 등록

            return;
        }
        else if(this.skill.skilltype == SkillType.Buff) 
        { 
            GameObject go = GameObject.Find("Skill_Slot_UI").gameObject;
            Skill_QuickSlot_Register quick_slot = go.GetComponent<Skill_QuickSlot_Register>();
            Abillity_Script abs = GameObject.Find("Abillity_Slot_CANVAS ").gameObject.GetComponent<Abillity_Script>();
            abs.Get_Slotnum(slotnum);  //slot에 대한 정보를 넘겨줌
            quick_slot.Get_Slotnum(slotnum); //slot에 대한 정보를 넘겨줌
            Skill_Quickslot_Panel.SetActive(true);
            Skill_Quickslot_Panel.transform.position = Input.mousePosition;

        }

    }

}
