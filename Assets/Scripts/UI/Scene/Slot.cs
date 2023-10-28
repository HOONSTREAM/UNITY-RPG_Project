using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerUpHandler
{
    
    public int slotnum;
    
    public Item item;
    public Image itemicon;
    public Image equiped_image;
    public TextMeshProUGUI amount_text;
    public bool isShopMode = false;
    public GameObject Sell_Panel;
    public GameObject Equip_Drop_Panel;
    public GameObject Use_Drop_Panel;



    void Start()
    {
        
        equiped_image.gameObject.SetActive(false); //초기화 (체크표시 안함)
        itemicon.gameObject.SetActive(false); //초기화 (아이콘 표시 안함)
        amount_text.text = "";
        
        

    }
    public void UpdateSlotUI()
    {
        itemicon.sprite = item.itemImage;
        itemicon.gameObject.SetActive(true);

        if (item.Equip) //아이템 체크(장착표시) 재검사 
        {
            equiped_image.gameObject.SetActive(true);

        }
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
        equiped_image.gameObject.SetActive(false); //체크표시 전체해제 (업데이트)
        amount_text.text = "";
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isShopMode) 
        {
            Debug.Log("상점에 판매합니다.");
            if (item.Equip)
            {
                GameObject gos = GameObject.Find("UnityChan").gameObject;
                PlayerStat stats = gos.GetComponent<PlayerStat>();
                stats.PrintUserText("장착중인 장비는 판매할 수 없습니다.");
                return;
            }

            GameObject GUI = GameObject.Find("GUI").gameObject;
            Sell_Console sell = GUI.GetComponent<Sell_Console>();
            sell.Get_Slotnum(slotnum); //slot에 대한 정보를 sellconsole 스크립트에 넘겨줌
            Managers.Sound.Play("Coin");
            Sell_Panel.gameObject.SetActive(true);
            
            return;
        }

        else //상점모드가 아닌경우 장착/해제 담당
        {
            if(PlayerInventory.Instance.player_items.Count == 0)
            {
                return;
            }

            if(this.item.itemtype == ItemType.Equipment) //장비아이템인경우
            {
                GameObject go = GameObject.Find("NewInvenUI").gameObject;
                Slot_Equip_Drop equip_drop = go.GetComponent<Slot_Equip_Drop>();
                equip_drop.Get_Slotnum(slotnum); //slot에 대한 정보를 sellconsole 스크립트에 넘겨줌
                Equip_Drop_Panel.SetActive(true);
                Equip_Drop_Panel.transform.position = Input.mousePosition;
            }
            else // 소모품인경우 
            {
                GameObject go = GameObject.Find("NewInvenUI").gameObject;
                Slot_Equip_Drop equip_drop = go.GetComponent<Slot_Equip_Drop>();
                equip_drop.Get_Slotnum(slotnum); //slot에 대한 정보를 sellconsole 스크립트에 넘겨줌
                Use_Drop_Panel.SetActive(true);
                Use_Drop_Panel.transform.position = Input.mousePosition;

            }


        }
      

    }

   

}

