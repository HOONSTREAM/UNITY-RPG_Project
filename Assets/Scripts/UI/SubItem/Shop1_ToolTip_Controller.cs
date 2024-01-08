using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop1_ToolTip_Controller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
/*농부헤리안 툴팁컨트롤러 스크립트*/
{
   
    public NPC1_shopslot[] Herian_slots;
    public ToolTip tooltip;   
    public Transform Herian_SlotHolder;
   

    enum OnToolTipUpdated
    {
        None,
        On,
        off,

    }
    OnToolTipUpdated ontooltip = OnToolTipUpdated.None;

    void Start()
    {
        Herian_slots = Herian_SlotHolder.GetComponentsInChildren<NPC1_shopslot>();        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ontooltip != OnToolTipUpdated.On)
        {


            Item item = GetComponent<NPC1_shopslot>().shopitem;


            if (item != null)
            {

                tooltip.gameObject.SetActive(true);

                tooltip.SetupTooltip(item.itemname, item.stat_1, item.stat_2, item.stat_3, item.stat_4, item.num_1, item.num_2, item.num_3, item.num_4,item.itemrank.ToString(), item.Description);


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
