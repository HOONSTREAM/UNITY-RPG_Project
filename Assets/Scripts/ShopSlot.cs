using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    public TextMeshProUGUI Quantity_num_text;
    
    Shop shop;
    public int quantity = 0;
    int testprice = 3000;
    public int totalGold = 0;
    public int slotnum;
    public Item shopitem;



    void Start()
    {
        shop = GetComponent<Shop>();

    }

    void Update()
    {
        
    }


#region Shop_ItemList

    public void TestItem_PlusButton()
    {
        if (quantity >= 10)
        {
            return;
        }

        quantity++;       
        totalGold = testprice*quantity;
        Quantity_num_text.text = $"{quantity}/10";

        switch (slotnum) 
        {

            case 0:
                shop.ScrollViewText1.text = $"Å×½ºÆ®°©¿Ê {quantity}";
                break;
            case 1:
                shop.ScrollViewText2.text = $"Å×½ºÆ®°©¿Ê2 {quantity}";
                break;
            case 2:
                shop.ScrollViewText3.text = $"Å×½ºÆ®°©¿Ê3 {quantity}";
                break;
            case 3:
                shop.ScrollViewText4.text = $"Å×½ºÆ®°©¿Ê4 {quantity}";
                break;
            case 4:
                shop.ScrollViewText5.text = $"Å×½ºÆ®°©¿Ê5 {quantity}";
                break;
            case 5:
                shop.ScrollViewText6.text = $"Å×½ºÆ®°©¿Ê6 {quantity}";
                break;
            case 6:
                shop.ScrollViewText7.text = $"Å×½ºÆ®°©¿Ê7 {quantity}";
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
        totalGold = testprice * quantity;
        Quantity_num_text.text = $"{quantity}/10";

        switch (slotnum)
        {

            case 0:
                shop.ScrollViewText1.text = $"Å×½ºÆ®°©¿Ê {quantity}";
                break;
            case 1:
                shop.ScrollViewText2.text = $"Å×½ºÆ®°©¿Ê2 {quantity}";
                break;
            case 2:
                shop.ScrollViewText3.text = $"Å×½ºÆ®°©¿Ê3 {quantity}";
                break;
            case 3:
                shop.ScrollViewText4.text = $"Å×½ºÆ®°©¿Ê4 {quantity}";
                break;
            case 4:
                shop.ScrollViewText5.text = $"Å×½ºÆ®°©¿Ê5 {quantity}";
                break;
            case 5:
                shop.ScrollViewText6.text = $"Å×½ºÆ®°©¿Ê6 {quantity}";
                break;
            case 6:
                shop.ScrollViewText7.text = $"Å×½ºÆ®°©¿Ê7 {quantity}";
                break;


        }

        if (quantity == 0)
        {
            switch (slotnum)
            {

                case 0:
                    shop.ScrollViewText1.text = $"Å×½ºÆ®°©¿Ê {quantity}";
                    break;
                case 1:
                    shop.ScrollViewText2.text = $"Å×½ºÆ®°©¿Ê2 {quantity}";
                    break;
                case 2:
                    shop.ScrollViewText3.text = $"Å×½ºÆ®°©¿Ê3 {quantity}";
                    break;
                case 3:
                    shop.ScrollViewText4.text = $"Å×½ºÆ®°©¿Ê4 {quantity}";
                    break;
                case 4:
                    shop.ScrollViewText5.text = $"Å×½ºÆ®°©¿Ê5 {quantity}";
                    break;
                case 5:
                    shop.ScrollViewText6.text = $"Å×½ºÆ®°©¿Ê6 {quantity}";
                    break;
                case 6:
                    shop.ScrollViewText7.text = $"Å×½ºÆ®°©¿Ê7 {quantity}";
                    break;


            }
        }

    }


#endregion


    public void ResetShop()
    {
        quantity = 0;
        totalGold = 0;
        Quantity_num_text.text = $"{quantity}/10";
        shop.TotalGoldText.text = $"{totalGold}";
        shop.ScrollViewText1.text = "";
        shop.ScrollViewText2.text = "";
        shop.ScrollViewText3.text = "";
        shop.ScrollViewText4.text = "";
        shop.ScrollViewText5.text = "";
        shop.ScrollViewText6.text = "";
        shop.ScrollViewText7.text = "";



    }


}
