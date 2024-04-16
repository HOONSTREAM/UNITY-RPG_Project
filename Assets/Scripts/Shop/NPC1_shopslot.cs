using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 루덴시안의 상점 NPC 브로아의 스크립트 입니다.
/// </summary>
public class NPC1_shopslot : MonoBehaviour
{
    private const int PURCHASE_LIMIT_WEAPON = 10;
    private const int PURCHASE_LIMIT_CONSUMABLE = 100;

    public TextMeshProUGUI Quantity_num_text;


    #region 타 클래스에서 참조하고 있는 변수입니다. (Public)
    NPC1_shop shop;
    public int quantity = 0;
    public int totalGold = 0;
    public int slotnum;
    public Item shopitem;
    #endregion


    void Start()
    {
        shop = GetComponent<NPC1_shop>();
        ResetShop(); //상점 초기화 1번 하고 게임시작
    }

    #region Shop_ItemList

    public void Item_PlusButton()
    {
        
        if (quantity >= PURCHASE_LIMIT_WEAPON) 
        {
            if(shopitem.itemtype == ItemType.Consumables)
            {
                if(quantity >= PURCHASE_LIMIT_CONSUMABLE) //소모품은 100개까지 허용
                {
                    return;
                }
            }
            else
            {
                return;
            }
            
        }

        quantity++;       
        totalGold = shopitem.buyprice*quantity;

        if(shopitem.itemtype == ItemType.Consumables) //소모품일경우 100개
        {
            Quantity_num_text.text = $"{quantity}/{PURCHASE_LIMIT_CONSUMABLE.ToString()}";
        }
        else if(shopitem.itemtype == ItemType.Equipment)
        {
            Quantity_num_text.text = $"{quantity}/{PURCHASE_LIMIT_WEAPON.ToString()}";
        }


        switch (slotnum)  //TODO :텍스트 작성방식
        {

            case 0:
                shop.ScrollViewText_item1.text = $"{shopitem.itemname} {quantity}";
                break;
            case 1:
                shop.ScrollViewText_item2.text = $" {shopitem.itemname} {quantity}";
                break;
            case 2:
                shop.ScrollViewText_item3.text = $"{shopitem.itemname} {quantity}";
                break;
            case 3:
                shop.ScrollViewText_item4.text = $"{shopitem.itemname} {quantity}";
                break;
        }
       
  
    }

    public void Item_MinusButton()
    {
        if (quantity <= 0)
        {          
            return;
        }

        quantity--;
        totalGold = shopitem.buyprice * quantity;

        if (shopitem.itemtype == ItemType.Consumables) //소모품일경우 100개
        {
            Quantity_num_text.text = $"{quantity}/{PURCHASE_LIMIT_CONSUMABLE.ToString()}";
        }
        else
        {
            Quantity_num_text.text = $"{quantity}/{PURCHASE_LIMIT_WEAPON.ToString()}";
        }

        switch (slotnum)  //TODO :텍스트 작성방식
        {

            case 0:
                shop.ScrollViewText_item1.text = $"{shopitem.itemname} {quantity}";
                break;
            case 1:
                shop.ScrollViewText_item2.text = $" {shopitem.itemname} {quantity}";
                break;
            case 2:
                shop.ScrollViewText_item3.text = $"{shopitem.itemname} {quantity}";
                break;
            case 3:
                shop.ScrollViewText_item4.text = $"{shopitem.itemname} {quantity}";
                break;
        }

        if (quantity == 0)
        {
            switch (slotnum)
            {
                case 0:
                    shop.ScrollViewText_item1.text = "";
                    break;
                case 1:
                    shop.ScrollViewText_item2.text = "";
                    break;
                case 2:
                    shop.ScrollViewText_item3.text = "";
                    break;
                case 3:
                    shop.ScrollViewText_item4.text = "";
                    break;

            }
        }

    }

#endregion

    public void ResetShop()
    {
        quantity = 0;

        totalGold = 0;

        if (shopitem.itemtype == ItemType.Consumables) //소모품일경우 100개
        {
            Quantity_num_text.text = $"{quantity}/{PURCHASE_LIMIT_CONSUMABLE.ToString()}";
        }
        else if (shopitem.itemtype == ItemType.Equipment)
        {
            Quantity_num_text.text = $"{quantity}/{PURCHASE_LIMIT_WEAPON.ToString()}";
        }

        shop.TotalGoldText.text = $"{totalGold}";

        shop.ScrollViewText_item1.text = "";
        shop.ScrollViewText_item2.text = "";
        shop.ScrollViewText_item3.text = "";
        shop.ScrollViewText_item4.text = "";
        
        return;

    }


}
