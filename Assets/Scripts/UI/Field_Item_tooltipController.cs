using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Field_Item_tooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Field_Item_Tooltip tooltip;
    public GameObject Inventory_canvas;
    [SerializeField]
    private GameObject tooltip_obj;

    private int _mask = (1 << (int)Define.Layer.Default);

    private void Start()
    {      
        Inventory_canvas = GameObject.Find("INVENTORY CANVAS").gameObject;
        tooltip_obj =Instantiate(tooltip.gameObject, Inventory_canvas.transform);
        tooltip_obj.gameObject.SetActive(false);

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




            tooltip_obj.gameObject.SetActive(true);
            tooltip_obj.GetComponent<Field_Item_Tooltip>().SetupToolTip(name);


            ontooltip = OnToolTipUpdated.On;
        }

        return;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Item OnPointerExit 호출");
        if (ontooltip != OnToolTipUpdated.off)
        {
            tooltip_obj.gameObject.SetActive(false);           
            ontooltip = OnToolTipUpdated.off;
        }

        return;
    }

}
