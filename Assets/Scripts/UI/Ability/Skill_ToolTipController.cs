using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Skill_ToolTipController : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    #region 무기종류
    private const string ONE_HAND_SWORD = "한손검";
    private const string TWO_HAND_SWORD = "양손검";
    #endregion


    public Skill_ToolTip tooltip; 
    public Ability_Slot[] Ability_Slots;
    public Transform Ability_slotholder;
    public GameObject Ability_UI;
    private PlayerStat stat;
    private Ability_Script Ability_script;
    enum OnToolTipUpdated
    {
        None,
        On,
        off,
    }

    OnToolTipUpdated ontooltip = OnToolTipUpdated.None;

    void Start()
    {
        Ability_script = GameObject.Find("Ability_Slot_CANVAS").gameObject.GetComponent<Ability_Script>();
        Ability_Slots = Ability_slotholder.GetComponentsInChildren<Ability_Slot>();
        stat = Managers.Game.GetPlayer().gameObject.GetComponent<PlayerStat>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
        if (ontooltip != OnToolTipUpdated.On)
        {

            Skill skill = GetComponent<Ability_Slot>().skill;

            if(skill == null)
            {
                return;
            }
           
            tooltip.gameObject.SetActive(true); // 툴팁 활성화

                   
            if (skill.skilltype == SkillType.Ability)
            {

                switch (skill.skill_name)
                {
                    case ONE_HAND_SWORD:

                        tooltip.SetupAbilityToolTip(skill.skill_name, skill.stat_1, Calculate_Weapon_ATK_In_ToolTip(ONE_HAND_SWORD), skill.Description, skill.skill_image);
                        tooltip.stat_2.gameObject.SetActive(false); // 나타낼 필요 없는 정보            
                        tooltip.num_2.gameObject.SetActive(false); // 나타낼 필요 없는 정보 

                        break;

                    case TWO_HAND_SWORD:

                        tooltip.SetupAbilityToolTip(skill.skill_name, skill.stat_1, Calculate_Weapon_ATK_In_ToolTip(TWO_HAND_SWORD), skill.Description, skill.skill_image);
                        tooltip.stat_2.gameObject.SetActive(false); // 나타낼 필요 없는 정보            
                        tooltip.num_2.gameObject.SetActive(false); // 나타낼 필요 없는 정보 


                        break;

                  

                }
          
            }

            else if(skill.skilltype == SkillType.Buff)// 스펠인경우

            { 
                switch (skill.skill_name)
                {
                    case "어드밴스드어택":                        
                    case "난공불락":
                        tooltip.SetupTooltip(skill.skill_name, skill.stat_1, skill.stat_2, skill.num_1, skill.num_2, skill.Description, skill.skill_image);
                        tooltip.stat_1.gameObject.SetActive(true);
                        tooltip.stat_2.gameObject.SetActive(true);
                        tooltip.num_1.gameObject.SetActive(true);
                        tooltip.num_2.gameObject.SetActive(true);
                        break;

                    default:
                        tooltip.SetupTooltip(skill.skill_name, skill.stat_1, skill.stat_2, skill.num_1, skill.num_2, skill.Description, skill.skill_image);
                        tooltip.stat_1.gameObject.SetActive(true);
                        tooltip.stat_2.gameObject.SetActive(true);
                        tooltip.num_1.gameObject.SetActive(true);
                        tooltip.num_2.gameObject.SetActive(true);
                        break;
                }

            }

            else if(skill.skilltype == SkillType.Active)
            {
                tooltip.SetupTooltip(skill.skill_name, skill.stat_1, skill.stat_2, skill.num_1, skill.num_2, skill.Description, skill.skill_image);
                tooltip.stat_1.gameObject.SetActive(true);
                tooltip.stat_2.gameObject.SetActive(true);
                tooltip.num_1.gameObject.SetActive(true);
                tooltip.num_2.gameObject.SetActive(true);

            }
                       
        }

        ontooltip = OnToolTipUpdated.On;

        return;

    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (ontooltip != OnToolTipUpdated.off)
        {
            tooltip.gameObject.SetActive(false);
            ontooltip = OnToolTipUpdated.off;
        }

        return;
    }

    #region 스킬 툴팁에서의 출력되는 공격력을 나타내주는 메서드
    public int Calculate_Weapon_ATK_In_ToolTip(string skill_name)
    {

        switch (skill_name)
        {
            case ONE_HAND_SWORD:

                int oneHandAttack = 0;
              
                for (int i = 0; i < Ability_script.Ability_Slots.Length; i++)
                {
                    if (Ability_script.Ability_Slots[i].skill_name.text == ONE_HAND_SWORD)
                    {
                        double Ability_attack_improvement = (double.Parse(Ability_script.Ability_Slots[i].LEVEL.text) * 5);
                        double Abillity_Grade_improvement = (double.Parse(Ability_script.Ability_Slots[i].grade_amount.text) * 500);
                        oneHandAttack = (int)Ability_attack_improvement + (int)Abillity_Grade_improvement;
                        break;
                    }
                }

                return oneHandAttack;
             
            case TWO_HAND_SWORD:

                int twoHandAttack = 0;

                for (int i = 0; i < Ability_script.Ability_Slots.Length; i++)
                {
                    if (Ability_script.Ability_Slots[i].skill_name.text == TWO_HAND_SWORD)
                    {
                        double Ability_attack_improvement = (double.Parse(Ability_script.Ability_Slots[i].LEVEL.text) * 5);
                        double Abillity_Grade_improvement = (double.Parse(Ability_script.Ability_Slots[i].grade_amount.text) * 500);
                        twoHandAttack = (int)Ability_attack_improvement + (int)Abillity_Grade_improvement;
                        break;
                    }
                }

                return twoHandAttack;

            default:
                return 0;

                
        }
       
    }

    #endregion
}
