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
        Debug.Log("정령석 클릭");

    }
}

