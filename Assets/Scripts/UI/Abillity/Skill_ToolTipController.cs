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
           
            tooltip.gameObject.SetActive(true);

            tooltip.SetupTooltip(skill.skill_name, skill.stat_1, skill.stat_2, skill.num_1, skill.num_2, skill.Description, skill.skill_image);

            
            if (skill.skilltype == SkillType.Abillity)
            {
                tooltip.stat_1.gameObject.SetActive(false);
                tooltip.stat_2.gameObject.SetActive(false);
                tooltip.num_1.gameObject.SetActive(false);
                tooltip.num_2.gameObject.SetActive(false);

            }
            else
            {
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
