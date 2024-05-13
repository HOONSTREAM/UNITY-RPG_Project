using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTipController : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler

   
{
    public ToolTip tooltip;
    public Slot[] player_slots;
    public Transform Player_SlotHolder;
    public Storage_Slots[] storage_slots;
    public Transform Storage_SlotHolder;
    
    enum OnToolTipUpdated
    {
        None,
        On,
        off,

    }
    OnToolTipUpdated ontooltip = OnToolTipUpdated.None;

    void Start()
    {
        player_slots = Player_SlotHolder.GetComponentsInChildren<Slot>();
        storage_slots = Storage_SlotHolder.GetComponentsInChildren<Storage_Slots>();
    }
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ontooltip != OnToolTipUpdated.On)
        {

         
            Item item = GetComponent<Slot>().item;

            if(item == null)
            {
                return;
            }

            else
            {
                tooltip.gameObject.SetActive(true);

                tooltip.SetupTooltip(item.itemname, item.stat_1, item.stat_2, item.stat_3, item.stat_4,item.num_1, item.num_2, item.num_3 , item.num_4, item.itemrank.ToString(),item.Description);
               
            }


        }

        ontooltip = OnToolTipUpdated.On;

        return;
    }
       
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if(ontooltip != OnToolTipUpdated.off)
        {
            tooltip.gameObject.SetActive(false);
            ontooltip = OnToolTipUpdated.off;
        }

        return;
    }

  
}
