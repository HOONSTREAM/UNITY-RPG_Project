using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Field_Item_tooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Field_Item_Tooltip tooltip;
    public GameObject Inventory_canvas;
    

    private int _mask = (1 << (int)Define.Layer.Default);

    private void Start()
    {
        
        tooltip = Managers.Resources.Load<GameObject>("PreFabs/UI/SubItem/Field_Item_Tooltip").GetComponent<Field_Item_Tooltip>();
        Inventory_canvas = GameObject.Find("INVENTORY CANVAS").gameObject;

        

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
        Debug.Log("Item OnPointerEnter 호출");
        if (ontooltip != OnToolTipUpdated.On)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool raycasthit = Physics.Raycast(ray, out hit, 100.0f, _mask);

            string name = hit.collider.gameObject.GetComponent<FieldItem>().item.itemname;
            Debug.Log(name);


           
           
            tooltip.gameObject.SetActive(true);
            tooltip.SetupToolTip(name);
            Instantiate(tooltip, Inventory_canvas.transform);
            tooltip.transform.position = Input.mousePosition;
            ontooltip = OnToolTipUpdated.On;
        }

        return;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Item OnPointerExit 호출");
        if (ontooltip != OnToolTipUpdated.off)
        {
            tooltip.gameObject.SetActive(false);
            ontooltip = OnToolTipUpdated.off;
        }

        return;
    }

}
