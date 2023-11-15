using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTipController : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler


    //TODO : 창고슬롯과 동시에 열렸을 때 , 인벤토리 툴팁이 안보이는 현상
{
    public ToolTip tooltip;
    public Slot[] player_slots;
    public Transform Player_SlotHolder;
    public Storage_Slots[] storage_slots;
    public Transform Storage_SlotHolder;
    public GameObject StorageUI;
    enum OnToolTipUpdated
    {
        None,
        On,
        off,

    }
    OnToolTipUpdated ontooltip = OnToolTipUpdated.None;

    void Start()
    {
        player_slots = Player_SlotHolder.GetComponentsInChildren<Slot>();
        storage_slots = Storage_SlotHolder.GetComponentsInChildren<Storage_Slots>();
    }
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ontooltip != OnToolTipUpdated.On)
        {

            if (StorageUI.activeSelf)
            {
                Item storage_item = GetComponent<Storage_Slots>().item;

                if (storage_item != null)
                {

                    tooltip.gameObject.SetActive(true);

                    tooltip.SetupTooltip(storage_item.itemname, storage_item.stat_1, storage_item.stat_2, storage_item.num_1, storage_item.num_2, storage_item.Description);

                    ontooltip = OnToolTipUpdated.On;

                }
            }
               
            Item item = GetComponent<Slot>().item;


            if (item != null)
            {

                tooltip.gameObject.SetActive(true);

                tooltip.SetupTooltip(item.itemname, item.stat_1, item.stat_2, item.num_1, item.num_2, item.Description);

                ontooltip = OnToolTipUpdated.On;

            }
        }

            
     }
       
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if(ontooltip != OnToolTipUpdated.off)
        {
            tooltip.gameObject.SetActive(false);
            ontooltip = OnToolTipUpdated.off;
        }
     
    
    
    }

  
}
