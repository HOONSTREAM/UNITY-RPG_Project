using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Skill_ToolTipController : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{

    public Skill_ToolTip tooltip; 
    public Abillity_Slot[] abillity_Slots;
    public Transform Abillity_slotholder;
    public GameObject Abillity_UI;
    private PlayerStat stat;
    enum OnToolTipUpdated
    {
        None,
        On,
        off,

    }

    OnToolTipUpdated ontooltip = OnToolTipUpdated.None;

    void Start()
    {
        
        abillity_Slots = Abillity_slotholder.GetComponentsInChildren<Abillity_Slot>();
        stat = Managers.Game.GetPlayer().gameObject.GetComponent<PlayerStat>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       
        if (ontooltip != OnToolTipUpdated.On)
        {

            Skill skill = GetComponent<Abillity_Slot>().skill;

            if(skill == null)
            {
                return;
            }
           
            tooltip.gameObject.SetActive(true); // 툴팁 활성화

                   
            if (skill.skilltype == SkillType.Abillity)
            {

                switch (skill.skill_name)
                {
                    case "한손검":

                        tooltip.SetupAbillityToolTip(skill.skill_name, skill.stat_1, stat.one_hand_sword_abillityAttack, skill.Description, skill.skill_image);
                        tooltip.stat_2.gameObject.SetActive(false); // 나타낼 필요 없는 정보            
                        tooltip.num_2.gameObject.SetActive(false); // 나타낼 필요 없는 정보 

                        break;

                    case "양손검":

                        tooltip.SetupAbillityToolTip(skill.skill_name, skill.stat_1, stat.two_hand_sword_abillityAttack, skill.Description, skill.skill_image);
                        tooltip.stat_2.gameObject.SetActive(false); // 나타낼 필요 없는 정보            
                        tooltip.num_2.gameObject.SetActive(false); // 나타낼 필요 없는 정보 


                        break;
                }
          
            }

            else // 스펠인경우
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
}
