using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop2_ToolTip_Controller : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{   /*농부헤리안 툴팁컨트롤러 스크립트*/

    public ShopSlot[] kenen_slots;
    public ToolTip tooltip;
    public Transform kenen_SlotHolder;

    enum OnToolTipUpdated
    {
        None,
        On,
        off,

    }
    OnToolTipUpdated ontooltip = OnToolTipUpdated.None;

    void Start()
    {
        kenen_slots = kenen_SlotHolder.GetComponentsInChildren<ShopSlot>();       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ontooltip != OnToolTipUpdated.On)
        {


            Item item = GetComponent<ShopSlot>().shopitem;


            if (item != null)
            {

                tooltip.gameObject.SetActive(true);

                tooltip.SetupTooltip(item.itemname, item.stat_1, item.stat_2, item.num_1, item.num_2, item.Description);


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
