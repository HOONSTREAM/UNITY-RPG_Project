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
    public List<Item> shopitemDB;
    public List<Item> shop_items;



    void Start()
    {
        shopitemDB = ItemDataBase.instance.itemDB; //데이터베이스 복사 
        TotalGoldText.text = "0";
        Player = GameObject.Find("UnityChan").gameObject;
        stat =  Player.GetComponent<PlayerStat>();
        slots = slotHolder.GetComponentsInChildren<ShopSlot>();


        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
            slots[i].shopitem = shopitemDB[i]; //각 슬롯에 샵아이템데이터 등록 
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

    private void AddItemInShop()
    {
        
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
    #region 상점 구매/판매 코드
    //TODO : 각 아이템별 구매/판매가격 세팅, 갯수대로 구매 세팅 ,판매구현

    int totalquantity = 0; // 토탈 구매갯수 검사하여 가방갯수보다 많으면 리턴
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

        else //소지금 검사 통과
        {
            if(total == 0) //토탈구매골드가 0이면 buy버튼 비활성화
            {
                return;
            }

            if (PlayerInventory.Instance.player_items.Count >= 20) //가방 칸 갯수검사
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

            // 가방 칸 갯수가 충분한지 검사
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].shopitem.itemtype == ItemType.Consumables)
                {
                    break;
                   //TODO 소모품은 스택을 사용하므로 1개로 가정한다. 
                }
                else
                {
                    totalquantity += slots[i].quantity;
                }
                
            }
            Debug.Log($"구매하려는 갯수 (소모품은1개) : {totalquantity}");
            Debug.Log($"가방 채워진 수 : {PlayerInventory.Instance.player_items.Count}");


            if (totalquantity+ PlayerInventory.Instance.player_items.Count > 20)
            {
                Managers.Sound.Play("Coin");
                ScrollViewText1.text = "가방 칸이 모자라 구매할 수 없습니다.";
                ScrollViewText2.text = "";
                ScrollViewText3.text = "";
                ScrollViewText4.text = "";
                ScrollViewText5.text = "";
                ScrollViewText6.text = "";
                ScrollViewText7.text = "";
                
                totalquantity = 0; //토탈갯수 검사 초기화
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

            for(int i = 0; i < slots.Length; i++)
            {
                switch (slots[i].slotnum) // 각 슬롯별 구매 갯수 인식
                {

                    case 0:
                        int quan = slots[0].quantity;
                        Debug.Log(quan);
                        if (quan == 0)
                        {
                            break;
                        }
                        
                        for(int i0 = 0; i0<quan ; i0++)
                        {
                            PlayerInventory.Instance.AddItem(shopitemDB[0]);
                            
                        }

                        totalquantity = 0; //구매 후 토탈갯수 초기화
                        break;
                    case 1:
                        int quan1 = slots[1].quantity;
                        Debug.Log(quan1);
                        if (quan1 == 0)
                        {
                            break;
                        }
                        
                        for (int i1= 0; i1<quan1; i1++)
                        {
                            PlayerInventory.Instance.AddItem(shopitemDB[1]);
                            
                        }
                        totalquantity = 0; //구매 후 토탈갯수 초기화
                        break;
                    case 2:
                        int quan2 = slots[2].quantity;
                        Debug.Log(quan2);
                        if (quan2 == 0)
                        {
                            break;
                        }
                        
                        for (int i2= 0; i2<quan2; i2++)
                        {
                            PlayerInventory.Instance.AddItem(shopitemDB[2]);
                            
                        }
                        totalquantity = 0; //구매 후 토탈갯수 초기화
                        break;
                    case 3:
                        int quan3 = slots[3].quantity;
                        Debug.Log(quan3);
                        break;
                    case 4:
                        int quan4 = slots[4].quantity;
                        Debug.Log(quan4);
                        break;
                    case 5:
                        int quan5 = slots[5].quantity;
                        Debug.Log(quan5);
                        break;
                    case 6:
                        int quan6 = slots[6].quantity;
                        Debug.Log(quan6);
                        break;


                }
            }
           

            //TDOO 슬롯별로 아이템 , 갯수 비교하여 아이템 추가 

            return;
        }
    }
    #endregion

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
