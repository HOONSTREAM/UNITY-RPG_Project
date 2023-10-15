using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopSlot : MonoBehaviour
{
    public TextMeshProUGUI Quantity_num_text;
    
    Shop shop;
    int quantity = 0;
    int testprice = 3000;
    public int totalGold = 0;
    public int slotnum;
    public Item item;



    void Start()
    {
        shop = GetComponent<Shop>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    public void PlusButton()
    {
        if (quantity >= 10)
        {
            return;
        }

        quantity++;       
        totalGold = testprice*quantity;
        Quantity_num_text.text = $"{quantity}/10";
        shop.ScrollViewText.text = $"Å×½ºÆ®°©¿Ê {quantity}";
  
    }

    public void MinusButton()
    {
        if (quantity <= 0)
        {
            
            return;
        }

        quantity--;
        totalGold = testprice * quantity;
        Quantity_num_text.text = $"{quantity}/10";       
        shop.ScrollViewText.text = $"Å×½ºÆ®°©¿Ê {quantity}";

        if (quantity == 0)
        {
            shop.ScrollViewText.text = "";
        }

    }

    public void ResetShop()
    {
        quantity = 0;
        totalGold = 0;
        Quantity_num_text.text = $"{quantity}/10";
        shop.TotalGoldText.text = $"{totalGold}";
        shop.ScrollViewText.text = "";

    }


}
