using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JEWEL_Slot : MonoBehaviour, IPointerUpHandler
{
    public int slotnum;

    public Item item;
    public Image itemicon;

    public GameObject Unique_Particle;
    public GameObject Use_Cancel_Panel;

    public void UpdateSlotUI()
    {

        itemicon.sprite = item.itemImage;
        itemicon.gameObject.SetActive(true);
        Unique_Particle.gameObject.SetActive(true);

    }

    public void RemoveSlot()
    {
        item = null;
        itemicon.gameObject.SetActive(false);
        Unique_Particle.gameObject.SetActive(false);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameObject go = GameObject.Find("JEWEL_UI").gameObject;
        JEWEL_Slot_Use_Cancel use_cancel = go.GetComponent<JEWEL_Slot_Use_Cancel>();
        use_cancel.Get_Slotnum(slotnum); //slot에 대한 정보
        Use_Cancel_Panel.SetActive(true);
        Use_Cancel_Panel.transform.position = Input.mousePosition;
    }
}

