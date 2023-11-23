using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Quick_Slot : MonoBehaviour
{
    public int slotnum;

    public Item item;
    public Image itemicon;
    public TextMeshProUGUI amount_text;

    void Start()
    {
        
        itemicon.gameObject.SetActive(false); //초기화 (아이콘 표시 안함)
        amount_text.text = "";
    }


    public void UpdateSlotUI()
    {
        itemicon.sprite = item.itemImage;
        itemicon.gameObject.SetActive(true);

        if (item.itemtype == ItemType.Equipment)
        {
            amount_text.text = "";
        }

        else if (item.itemtype == ItemType.Consumables)
        {
            amount_text.text = item.amount.ToString();
        }

    }

    public void RemoveSlot()
    {
        item = null;
        itemicon.gameObject.SetActive(false);
        amount_text.text = "";
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (item.itemtype == ItemType.Consumables)
        {

            //TODO
        }

        
    }










}