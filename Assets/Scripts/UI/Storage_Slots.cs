using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Storage_Slots : MonoBehaviour, IPointerUpHandler
{
    public int slotnum;

    public Item item;
    public Image itemicon;
    public GameObject Unique_Particle;
    public Image equiped_image;
    public TextMeshProUGUI amount_text;
    public bool isShopMode = false;
    public bool isStorageMode = false;
    public GameObject WithDrawPanel;
    
    void Start()
    {
        equiped_image.gameObject.SetActive(false); //초기화 (체크표시 안함)
        itemicon.gameObject.SetActive(false); //초기화 (아이콘 표시 안함)
        Unique_Particle.gameObject.SetActive(false);
        amount_text.text = "";
    }

    public void UpdateSlotUI()
    {
        itemicon.sprite = item.itemImage;
        itemicon.gameObject.SetActive(true);
        Unique_Particle.gameObject.SetActive(false);

        if (item.Equip) //아이템 체크(장착표시) 재검사 
        {
            equiped_image.gameObject.SetActive(true);

        }
        if (item.itemtype == ItemType.Equipment)
        {
            amount_text.text = "";

            if (item.itemrank == ItemRank.Rare || item.itemrank == ItemRank.Unique || item.itemrank == ItemRank.Legend) // 아이템 랭크가 레어 이상이면, 파티클 활성화
            {
                Unique_Particle.gameObject.SetActive(true);
            }
        }

        else if (item.itemtype == ItemType.Consumables || item.itemtype == ItemType.Etc)
        {
            amount_text.text = item.amount.ToString();

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
        equiped_image.gameObject.SetActive(false); //체크표시 전체해제 (업데이트)
        Unique_Particle.gameObject.SetActive(false);
        amount_text.text = "";

    }

    public void OnPointerUp(PointerEventData eventData)
    {

       if(item.itemtype == ItemType.Consumables || item.itemtype == ItemType.Etc)
        {
   
            if(item.amount > 1)
            {

                GameObject GUI = GameObject.Find("GUI_Console").gameObject;
                Storage_WithDraw_Console storage = GUI.GetComponent<Storage_WithDraw_Console>();
                storage.Get_Slotnum(slotnum); //slot에 대한 정보를 storage_input_console 스크립트에 넘겨줌
                Managers.Sound.Play("Coin");
                WithDrawPanel.SetActive(true);
            }

            else
            {
                PlayerInventory.Instance.AddItem(item.Clone());
                PlayerStorage.Instance.RemoveItem(this.slotnum);              
            }

            return;
        }

       else if(item.itemtype == ItemType.Equipment)
        {
            

            PlayerInventory.Instance.AddItem(item.Clone());
            PlayerStorage.Instance.RemoveItem(this.slotnum);

            return;
        }


    }

}
