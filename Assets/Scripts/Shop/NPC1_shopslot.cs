using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// �絧�þ��� ���� NPC ��ξ��� ��ũ��Ʈ �Դϴ�.
/// </summary>
public class NPC1_shopslot : MonoBehaviour
{
    private const int PURCHASE_LIMIT_WEAPON = 10;
    private const int PURCHASE_LIMIT_CONSUMABLE = 100;

    public TextMeshProUGUI Quantity_num_text;


    #region Ÿ Ŭ�������� �����ϰ� �ִ� �����Դϴ�. (Public)
    NPC1_shop shop;
    public int quantity = 0;
    public int totalGold = 0;
    public int slotnum;
    public Item shopitem;
    #endregion


    void Start()
    {
        shop = GetComponent<NPC1_shop>();
        ResetShop(); //���� �ʱ�ȭ 1�� �ϰ� ���ӽ���
    }

    #region Shop_ItemList

    public void Item_PlusButton()
    {
        
        if (quantity >= PURCHASE_LIMIT_WEAPON) 
        {
            if(shopitem.itemtype == ItemType.Consumables)
            {
                if(quantity >= PURCHASE_LIMIT_CONSUMABLE) //�Ҹ�ǰ�� 100������ ���
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
            Quantity_num_text.text = $"{quantity}/{PURCHASE_LIMIT_CONSUMABLE.ToString()}";
        }
        else if(shopitem.itemtype == ItemType.Equipment)
        {
            Quantity_num_text.text = $"{quantity}/{PURCHASE_LIMIT_WEAPON.ToString()}";
        }


        switch (slotnum)  //TODO :�ؽ�Ʈ �ۼ����
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

        if (shopitem.itemtype == ItemType.Consumables) //�Ҹ�ǰ�ϰ�� 100��
        {
            Quantity_num_text.text = $"{quantity}/{PURCHASE_LIMIT_CONSUMABLE.ToString()}";
        }
        else
        {
            Quantity_num_text.text = $"{quantity}/{PURCHASE_LIMIT_WEAPON.ToString()}";
        }

        switch (slotnum)  //TODO :�ؽ�Ʈ �ۼ����
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

        if (shopitem.itemtype == ItemType.Consumables) //�Ҹ�ǰ�ϰ�� 100��
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
