using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_ToolTip_Controller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button_ToolTip tooltip;

    private int _mask = (1 << (int)Define.Layer.UI);
    enum OnToolTipUpdated
    {
        None,
        On,
        off,

    }

    OnToolTipUpdated ontooltip = OnToolTipUpdated.None;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("button_OnPointerEnter 진입");
        if (ontooltip != OnToolTipUpdated.On)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0)
            {
                // 결과 중 가장 위에 있는 UI 오브젝트의 이름 출력
                Debug.Log("UI Object Name = " + results[0].gameObject.name);
            }
            tooltip.SetupToolTip(results[0].gameObject.name);
            tooltip.gameObject.SetActive(true);

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
