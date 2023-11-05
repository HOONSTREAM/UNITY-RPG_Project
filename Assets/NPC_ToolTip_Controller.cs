using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngineInternal;

public class NPC_ToolTip_Controller : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public NPC_ToolTip tooltip;

    private int _mask = (1 << (int)Define.Layer.NPC1 | 1<<(int)Define.Layer.NPC);

    enum OnToolTipUpdated
    {
        None,
        On,
        off,

    }

    OnToolTipUpdated ontooltip = OnToolTipUpdated.None;

    public void OnPointerEnter(PointerEventData eventData) //TODO : 실행조건 정확하게 캐치 필요 
    {
        Debug.Log("OnPointerEnter 호출");
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ontooltip != OnToolTipUpdated.off)
        {
            tooltip.gameObject.SetActive(false);
            ontooltip = OnToolTipUpdated.off;
        }

    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
        
      
    }
}
