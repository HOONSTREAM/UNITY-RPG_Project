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
            bool isUse = item.Use();

            if (isUse)
            {
                if (item.itemtype == ItemType.Equipment)
                {
                    if (item.Equip) //이미 장착중인경우 다시 누를경우 장착해제
                    {
                        PlayerEquipment.Instance.UnEquipItem(this);
                        equiped_image.gameObject.SetActive(false); //장착해제 후 체크표시 해제

                        return;
                    }

                    else
                    {
                        PlayerEquipment.Instance.EquipItem(this); //아이템 장착 함수
                        if (item.Equip)
                        {
                            equiped_image.gameObject.SetActive(true); //체크표시
                          
                        }
                        else //장착할 수 없을경우 (부위가 겹칠경우)
                        {
                            equiped_image.gameObject.SetActive(false); //체크표시 안함
                        }

                    }

                }

                else //소모품일경우
                {

                    PlayerInventory.Instance.RemoveItem(slotnum);

                    if (item == null)
                    {
                        return;
                    }
                }

            }
        
        }

      

    }

   

}

