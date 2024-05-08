using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngineInternal;

public class NPC_ToolTip_Controller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public NPC_ToolTip tooltip;

    private int _mask = (1 << (int)Define.Layer.NPC1 | 1 << (int)Define.Layer.NPC | 1 << (int)Define.Layer.NPC2 | 
                         1 << (int)Define.Layer.NPC3 | 1 << (int)Define.Layer.NPC4 | 1 << (int)Define.Layer.NPC5 | 
                         1<<(int)Define.Layer.NPC6 | 1 << (int)Define.Layer.NPC7);


    enum OnToolTipUpdated
    {
        None,
        On,
        off,

    }

    OnToolTipUpdated ontooltip = OnToolTipUpdated.None;

    public void OnPointerEnter(PointerEventData eventData) 
    {
        Debug.Log("NPC_ OnPointerEnter È£Ãâ");
        if (ontooltip != OnToolTipUpdated.On)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool raycasthit = Physics.Raycast(ray, out hit, 100.0f, _mask);

            string name = hit.collider.gameObject.name;

            
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
