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



    private void OnEnable()
    {
        FieldItem.OnFieldItemDestroyed += HandleFieldItemDestroyed;
    }

    private void OnDisable()
    {
        FieldItem.OnFieldItemDestroyed -= HandleFieldItemDestroyed;
    }

    /// <summary>
    /// 파괴된 필드아이템의 툴팁이 계속 떠있다면, 검사하고 툴팁을 비활성화 시킵니다.
    /// </summary>
    /// <param name="item"></param>
    private void HandleFieldItemDestroyed(FieldItem item)
    {
        
        if (tooltip_obj.activeSelf && tooltip.GetComponent<Field_Item_Tooltip>().CurrentItem == item)
        {
            tooltip_obj.SetActive(false);
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (ontooltip != OnToolTipUpdated.On)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool raycasthit = Physics.Raycast(ray, out hit, 100.0f, _mask);

            string name = hit.collider.gameObject.GetComponent<FieldItem>().item.itemname;
            var item = hit.collider.gameObject.GetComponent<FieldItem>();
            
            tooltip_obj.gameObject.SetActive(true);
            
            tooltip_obj.GetComponent<Field_Item_Tooltip>().SetupToolTip(item);


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
