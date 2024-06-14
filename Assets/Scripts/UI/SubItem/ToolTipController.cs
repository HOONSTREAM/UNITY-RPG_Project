using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTipController : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler

   
{
    public ToolTip tooltip;
    public Slot[] player_slots;
    public Transform Player_SlotHolder;
    public Storage_Slots[] storage_slots;
    public Transform Storage_SlotHolder;
    
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

         
            Item item = GetComponent<Slot>().item;

            if(item == null)
            {
                return;
            }

            switch (item.itemtype)
            {
                case ItemType.SkillBook: // 모든 스텟정보를 필요로 하지않고, 스킬북 설명만 노출

                    tooltip.gameObject.SetActive(true);

                    tooltip.SetupTooltip(item.itemname, item.stat_1, item.stat_2, item.stat_3, item.stat_4, item.num_1, item.num_2, item.num_3, item.num_4, item.itemrank.ToString(), item.Description);

                    Dont_Display_Stat_Item(item);

                    break;

                default:

                    tooltip.gameObject.SetActive(true);


                    On_Display_Stat_Item(item);

                    tooltip.SetupTooltip(item.itemname, item.stat_1, item.stat_2, item.stat_3, item.stat_4, item.num_1, item.num_2, item.num_3, item.num_4, item.itemrank.ToString(), item.Description);

                    Dont_Display_Stat_Item_Menu(item);

                    break;

            }
          
        }

        ontooltip = OnToolTipUpdated.On;

        return;
    }
       
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if(ontooltip != OnToolTipUpdated.off)
        {
            tooltip.gameObject.SetActive(false);
            ontooltip = OnToolTipUpdated.off;
        }

        return;
    }

    private void Dont_Display_Stat_Item_Menu(Item item)
    {
        if(item.itemname == "이카루스의 깃털")
        {
            tooltip.num_1.gameObject.SetActive(false);
            tooltip.num_2.gameObject.SetActive(false);
            tooltip.num_3.gameObject.SetActive(false);
            tooltip.num_4.gameObject.SetActive(false);
            tooltip.stat_1.gameObject.SetActive(false);
            tooltip.stat_2.gameObject.SetActive(false);
            tooltip.stat_3.gameObject.SetActive(false);
            tooltip.stat_4.gameObject.SetActive(false);
        }

    }

    private void Dont_Display_Stat_Item(Item item)
    {
        tooltip.num_1.gameObject.SetActive(false);
        tooltip.num_2.gameObject.SetActive(false);
        tooltip.num_3.gameObject.SetActive(false);
        tooltip.num_4.gameObject.SetActive(false);
        tooltip.stat_1.gameObject.SetActive(false);
        tooltip.stat_2.gameObject.SetActive(false);
        tooltip.stat_3.gameObject.SetActive(false);
        tooltip.stat_4.gameObject.SetActive(false);
    }

    private void On_Display_Stat_Item(Item item)
    {
        tooltip.num_1.gameObject.SetActive(true);
        tooltip.num_2.gameObject.SetActive(true);
        tooltip.num_3.gameObject.SetActive(true);
        tooltip.num_4.gameObject.SetActive(true);
        tooltip.stat_1.gameObject.SetActive(true);
        tooltip.stat_2.gameObject.SetActive(true);
        tooltip.stat_3.gameObject.SetActive(true);
        tooltip.stat_4.gameObject.SetActive(true);
    }

  
}
