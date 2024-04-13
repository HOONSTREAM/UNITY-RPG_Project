using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Equip_Slot : MonoBehaviour
{
    public int slotnum;

    public Item item;
    public Image itemicon;
    public GameObject Unique_Particle;


    public void UpdateSlotUI()
    {
        itemicon.sprite = item.itemImage;
        itemicon.gameObject.SetActive(true);
        Unique_Particle.gameObject.SetActive(false);

        
        if (item.itemtype == ItemType.Equipment)
        {          

            if (item.itemrank == ItemRank.Rare || item.itemrank == ItemRank.Unique || item.itemrank == ItemRank.Legend) // 아이템 랭크가 레어 이상이면, 파티클 활성화
            {
                Unique_Particle.gameObject.SetActive(true);
            }

        }

        

    }

    public void RemoveSlot()
    {
        item = null;
        itemicon.gameObject.SetActive(false);       
        Unique_Particle.gameObject.SetActive(false);
       
    }



}


