using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public GameObject ShopPanel; //RectTransform으로 해도 됨.
    public TextMeshProUGUI TotalGoldText;

    public TextMeshProUGUI ScrollViewText1;
    public TextMeshProUGUI ScrollViewText2;
    public TextMeshProUGUI ScrollViewText3;
    public TextMeshProUGUI ScrollViewText4;
    public TextMeshProUGUI ScrollViewText5;
    public TextMeshProUGUI ScrollViewText6;
    public TextMeshProUGUI ScrollViewText7;



    GameObject Player;
    PlayerStat stat;
    ShopSlot[] slots;
    public Transform slotHolder;
    ItemDataBase Instance;
    
    


    void Start()
    {
        TotalGoldText.text = "0";
        Player = GameObject.Find("UnityChan").gameObject;
        stat =  Player.GetComponent<PlayerStat>();
        slots = slotHolder.GetComponentsInChildren<ShopSlot>();
        

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
   
        }
       

    }

    void Update()
    {
        PrintTotalGold();
    }


    public void Enter()
    {
       
        ShopPanel.SetActive(true);
        


    }

    public void Exit()
    {
        ShopPanel.SetActive(false);
        Managers.Sound.Play("Inven_open",Define.Sound.Effect);
        for (int i = 0; i < slots.Length; i++) //상점 초기화
        {
            slots[i].ResetShop();

        }
    }

    public void Buy()
    {
        int total = Convert.ToInt32(TotalGoldText.text);
        if(stat.Gold < total)
        {
         
            Managers.Sound.Play("Coin");
            ScrollViewText1.text = "소지금이 부족합니다.";
            ScrollViewText2.text = "";
            ScrollViewText3.text = "";
            ScrollViewText4.text = "";
            ScrollViewText5.text = "";
            ScrollViewText6.text = "";
            ScrollViewText7.text = "";

           
            return;

        }

        else
        {
            if(total == 0)
            {
                return;
            }

            if (PlayerInventory.Instance.player_items.Count >= 20)
            {
                Managers.Sound.Play("Coin");
                ScrollViewText1.text = "가방이 가득찼습니다.";
                ScrollViewText2.text = "";
                ScrollViewText3.text = "";
                ScrollViewText4.text = "";
                ScrollViewText5.text = "";
                ScrollViewText6.text = "";
                ScrollViewText7.text = "";
                return;

            }

            Debug.Log("구매 완료");
            Managers.Sound.Play("Coin");
            ScrollViewText1.text = "구매 완료하였습니다.";           
            ScrollViewText2.text = "";
            ScrollViewText3.text = "";
            ScrollViewText4.text = "";
            ScrollViewText5.text = "";
            ScrollViewText6.text = "";
            ScrollViewText7.text = "";
            stat.Gold -= total;
            
            PlayerInventory.Instance.AddItem(ItemDataBase.instance.itemDB[5]);

            //TDOO 슬롯별로 아이템 , 갯수 비교하여 아이템 추가 

            return;
        }
    }

    private void PrintTotalGold()
    {
        int totalgold = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            totalgold += slots[i].totalGold;
        }
        TotalGoldText.text = $"{totalgold}";
    }

   



}
