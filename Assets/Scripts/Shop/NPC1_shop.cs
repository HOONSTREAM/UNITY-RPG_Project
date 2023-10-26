using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPC1_shop : MonoBehaviour
{

    public GameObject ShopPanel; //RectTransform으로 해도 됨.
    public TextMeshProUGUI TotalGoldText;

    public TextMeshProUGUI ScrollViewText;
    



    GameObject Player;
    PlayerStat stat;
    NPC1_shopslot[] slots;
    public Transform slotHolder;
    public List<Item> shopitemDB;
    Slot[] playerslots;
    public Transform playerslotHolder;



    void Start()
    {
        shopitemDB = ItemDataBase.instance.itemDB; //데이터베이스 복사 
        TotalGoldText.text = "0";
        Player = GameObject.Find("UnityChan").gameObject;
        stat =  Player.GetComponent<PlayerStat>();
        slots = slotHolder.GetComponentsInChildren<NPC1_shopslot>();
        playerslots = playerslotHolder.GetComponentsInChildren<Slot>(); //플레이어 슬롯 참조 (샵모드 불리언 검사)

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
            
        }
        //샵에 등록할 아이템
        slots[0].shopitem = shopitemDB[3]; //각 슬롯에 샵아이템데이터 등록 
        slots[1].shopitem = shopitemDB[5];

    }

    void Update()
    {
        PrintTotalGold(); 
    }

    public void Enter()
    {      
        ShopPanel.SetActive(true);

        for (int i = 0; i < playerslots.Length; i++)
        {
            playerslots[i].isShopMode = true;
        }
    }

    

    public void Exit()
    {
        ShopPanel.SetActive(false);
        Managers.Sound.Play("Inven_open",Define.Sound.Effect);
        for (int i = 0; i < slots.Length; i++) //상점 초기화
        {
            slots[i].ResetShop();

        }

        for (int i = 0; i < playerslots.Length; i++)
        {
            playerslots[i].isShopMode = false;
        }

    }
    #region 상점 구매/판매 코드
    

    int totalquantity = 0; // 토탈 구매갯수 검사하여 가방갯수보다 많으면 리턴
    public void Buy()
    {
        int total = Convert.ToInt32(TotalGoldText.text);

        if(stat.Gold < total)
        {
         
            Managers.Sound.Play("Coin");
            ScrollViewText.text = "소지금이 부족합니다.";
            
  
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
                ScrollViewText.text = "가방이 가득찼습니다.";
                
                return;

            }

            // 가방 칸 갯수가 충분한지 검사
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].shopitem.itemtype == ItemType.Consumables)
                {
                    int tempquantity = 1; //임시로 1개로 증가, 소모품은 스택사용하므로 가방칸 1개만 사용
                    totalquantity += tempquantity;
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
                ScrollViewText.text = "가방 칸이 모자라 구매할 수 없습니다.";
               
                
                totalquantity = 0; //토탈갯수 검사 초기화
                return;
            }
            
            
            Debug.Log("구매 완료");
            Managers.Sound.Play("Coin");
            ScrollViewText.text = "구매 완료하였습니다.";           
            
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
                            PlayerInventory.Instance.AddItem(shopitemDB[3].Clone());
                            
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
                            PlayerInventory.Instance.AddItem(shopitemDB[5].Clone());
                            
                        }
                        totalquantity = 0; //구매 후 토탈갯수 초기화
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
