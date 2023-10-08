using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTipController : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler

{
    public ToolTip tooltip;
    
    enum OnToolTipUpdated
    {
        None,
        On,
        off,

    }
    OnToolTipUpdated ontooltip = OnToolTipUpdated.None;

    void Start()
    {
       
    }
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ontooltip != OnToolTipUpdated.On)
        {
            
            Item item = GetComponent<Slot>().item;
            

            if (item != null)
            {

                tooltip.gameObject.SetActive(true);

                tooltip.SetupTooltip(item.itemname, item.stat_1, item.stat_2, item.num_1, item.num_2, item.Description);
                
                ontooltip = OnToolTipUpdated.On;

            }
        }
       
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(ontooltip != OnToolTipUpdated.off)
        {
            tooltip.gameObject.SetActive(false);
            ontooltip = OnToolTipUpdated.off;
        }
     
    
    
    }

  
}
