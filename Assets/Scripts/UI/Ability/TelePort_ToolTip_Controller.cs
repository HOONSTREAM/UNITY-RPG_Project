using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TelePort_ToolTip_Controller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TelePort_ToolTip tooltip;

    enum OnToolTipUpdated
    {
        None,
        On,
        off,

    }

    OnToolTipUpdated ontooltip = OnToolTipUpdated.None;

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (ontooltip != OnToolTipUpdated.On)
        {

            tooltip.gameObject.SetActive(true); // ÅøÆÁ È°¼ºÈ­

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
