using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Equipment_Tooltip_Controller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public ToolTip tooltip; //장비창에서 마우스 접근시 나타나는 툴팁 

    #region 장비창 슬롯
    public Equip_Slot[] upper_equip_slots; // Necklace,Head,Head_Deco
    public Equip_Slot[] middle_equip_slots; // Weapon, Shield
    public Equip_Slot[] middle2_equip_slots; // Ring 1 , Ring 2
    public Equip_Slot[] bottom_equip_slots; // chest , pants, outter_plate
    public Equip_Slot[] bottom2_equip_slots; // shoes, cape , vehicle

    public Transform upper_equip_slot_holder;
    public Transform middle_equip_slot_holder;
    public Transform middle2_equip_slot_holder;
    public Transform bottom_equip_slot_holder;
    public Transform bottom2_equip_slot_holder;
    #endregion

    
    public GameObject Equipment_UI;

    enum OnToolTipUpdated
    {
        None,
        On,
        off,

    }

    OnToolTipUpdated ontooltip = OnToolTipUpdated.None;

    void Start()
    {
        #region 장비창 슬롯 (구분)
        upper_equip_slots = upper_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        middle_equip_slots = middle_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        middle2_equip_slots = middle2_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        bottom_equip_slots = bottom_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        bottom2_equip_slots = bottom2_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        #endregion
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (ontooltip != OnToolTipUpdated.On)
        {

            Item equip_item = GetComponent<Equip_Slot>().item;

            if (equip_item != null)
            {

                tooltip.gameObject.SetActive(true);

                tooltip.SetupTooltip(equip_item.itemname, equip_item.stat_1, equip_item.stat_2, equip_item.stat_3, equip_item.stat_4, equip_item.num_1, equip_item.num_2, equip_item.num_3, equip_item.num_4, equip_item.itemrank.ToString(), equip_item.Description);

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
