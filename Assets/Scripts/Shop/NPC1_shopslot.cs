using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC1_shopslot : MonoBehaviour
{
    public TextMeshProUGUI Quantity_num_text;
    
    NPC1_shop shop;
    public int quantity = 0;
    public int totalGold = 0;
    public int slotnum;
    public Item shopitem;



    void Start()
    {
        shop = GetComponent<NPC1_shop>();
        ResetShop(); //���� �ʱ�ȭ 1�� �ϰ� ���ӽ���

    }

    void Update()
    {
        
    }


#region Shop_ItemList

    public void TestItem_PlusButton()
    {
        if (quantity >= 10) 
        {
            if(shopitem.itemtype == ItemType.Consumables)
            {
                if(quantity >= 100) //�Ҹ�ǰ�� 100������ ���
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

        if(shopitem.itemtype == ItemType.Consumables) //�Ҹ�ǰ�ϰ�� 100��
        {
            Quantity_num_text.text = $"{quantity}/100";
        }
        else
        {
            Quantity_num_text.text = $"{quantity}/10";
        }
       
        switch (slotnum)  //TODO :�ؽ�Ʈ �ۼ����
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

        if (shopitem.itemtype == ItemType.Consumables) //�Ҹ�ǰ�ϰ�� 100��
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

        if (shopitem.itemtype == ItemType.Consumables) //�Ҹ�ǰ�ϰ�� 100��
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
