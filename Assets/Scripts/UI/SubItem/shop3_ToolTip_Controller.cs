using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 보석상 케일 상점창 툴팁컨트롤러 스크립트 입니다.
/// </summary>
public class shop3_ToolTip_Controller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public NPC2_shopslot[] Kale_slots;
    public ToolTip tooltip;
    public Transform Kale_SlotHolder;


    enum OnToolTipUpdated
    {
        None,
        On,
        off,

    }
    OnToolTipUpdated ontooltip = OnToolTipUpdated.None;

    void Start()
    {
        Kale_slots = Kale_SlotHolder.GetComponentsInChildren<NPC2_shopslot>();
        tooltip.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ontooltip != OnToolTipUpdated.On)
        {


            Item item = GetComponent<NPC2_shopslot>().shopitem;


            if (item != null)
            {

                tooltip.gameObject.SetActive(true);

                tooltip.SetupTooltip(item.itemname, item.stat_1, item.stat_2, item.stat_3, item.stat_4, item.num_1, item.num_2, item.num_3, item.num_4, item.itemrank.ToString(), item.Description);


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
