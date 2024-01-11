using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Field_Item_tooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public Field_Item_Tooltip tooltip;

   

    private int _mask = (1 << (int)Define.Layer.Default);

    private void Start()
    {
        tooltip = GameObject.Find("Field_Item_Tooltip").gameObject.GetComponent<Field_Item_Tooltip>();
    }
    enum OnToolTipUpdated
    {
        None,
        On,
        off,

    }

    OnToolTipUpdated ontooltip = OnToolTipUpdated.None;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Item OnPointerEnter »£√‚");
        if (ontooltip != OnToolTipUpdated.On)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool raycasthit = Physics.Raycast(ray, out hit, 100.0f, _mask);

            string name = hit.collider.gameObject.GetComponent<FieldItem>().item.itemname;
            Debug.Log(name);


            tooltip.SetupToolTip(name);
            tooltip.gameObject.SetActive(true);
            ontooltip = OnToolTipUpdated.On;
        }

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
