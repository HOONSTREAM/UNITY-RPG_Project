using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    public TextMeshProUGUI Quantity_num_text;
    
    Shop shop;
    public int quantity = 0;
    public int totalGold = 0;
    public int slotnum;
    public Item shopitem;



    void Start()
    {
        shop = GetComponent<Shop>();
        ResetShop(); //상점 초기화 1번 하고 게임시작

    }


#region Shop_ItemList

    public void TestItem_PlusButton()
    {
        if (quantity >= 10) 
        {
            if(shopitem.itemtype == ItemType.Consumables)
            {
                if(quantity >= 100) //소모품은 100개까지 허용
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
            Quantity_num_text.text = $"{quantity}/100";
        }
        else
        {
            Quantity_num_text.text = $"{quantity}/10";
        }
       
        switch (slotnum)  //TODO
        {

            case 0:
                shop.ScrollViewText.text = $"{shopitem.itemname} {quantity},";
                break;
            case 1:
                shop.ScrollViewText.text = $"{shopitem.itemname} {quantity},";
                break;
            case 2:
                shop.ScrollViewText.text = $"{shopitem.itemname} {quantity},";
                break;
            case 3:
                shop.ScrollViewText.text = $"{shopitem.itemname} {quantity},";
                break;
            case 4:
                shop.ScrollViewText.text = $"{shopitem.itemname} {quantity},";
                break;
            case 5:
                shop.ScrollViewText.text = $"{shopitem.itemname} {quantity},";
                break;
            case 6:
                shop.ScrollViewText.text = $"{shopitem.itemname} {quantity},";
                break;


        }
       
  
    }

    public void TestItem_MinusButton()
    {
        if (quantity <= 0)
        {
            
            return;
        }

        quantity--;
        totalGold = shopitem.buyprice * quantity;

        if (shopitem.itemtype == ItemType.Consumables) //소모품일경우 100개
        {
            Quantity_num_text.text = $"{quantity}/100";
        }
        else
        {
            Quantity_num_text.text = $"{quantity}/10";
        }

        switch (slotnum)
        {

            case 0:
                shop.ScrollViewText.text += $"{shopitem.itemname} {quantity},";
                break;
            case 1:
                shop.ScrollViewText.text += $"{shopitem.itemname} {quantity},";
                break;
            case 2:
                shop.ScrollViewText.text += $"{shopitem.itemname} {quantity},";
                break;
            case 3:
                shop.ScrollViewText.text += $"{shopitem.itemname} {quantity},";
                break;
            case 4:
                shop.ScrollViewText.text += $"{shopitem.itemname} {quantity},";
                break;
            case 5:
                shop.ScrollViewText.text += $"{shopitem.itemname} {quantity},";
                break;
            case 6:
                shop.ScrollViewText.text += $"{shopitem.itemname} {quantity},";
                break;


        }

        if (quantity == 0)
        {
            switch (slotnum)
            {

                case 0:
                    shop.ScrollViewText.text += $"{shopitem.itemname} {quantity},";
                    break;
                case 1:
                    shop.ScrollViewText.text += $"{shopitem.itemname} {quantity},";
                    break;
                case 2:
                    shop.ScrollViewText.text += $"{shopitem.itemname} {quantity},";
                    break;
                case 3:
                    shop.ScrollViewText.text += $"{shopitem.itemname} {quantity},";
                    break;
                case 4:
                    shop.ScrollViewText.text += $"{shopitem.itemname} {quantity},";
                    break;
                case 5:
                    shop.ScrollViewText.text += $"{shopitem.itemname} {quantity},";
                    break;
                case 6:
                    shop.ScrollViewText.text += $"{shopitem.itemname} {quantity},";
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
            Quantity_num_text.text = $"{quantity}/100";
        }
        else
        {
            Quantity_num_text.text = $"{quantity}/10";
        }

        shop.TotalGoldText.text = $"{totalGold}";
  
    }


}
