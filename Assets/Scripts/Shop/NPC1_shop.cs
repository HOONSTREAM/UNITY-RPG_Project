using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 브로아 상점 스크립트 입니다.
/// </summary>
public class NPC1_shop : MonoBehaviour
{

    public GameObject ShopPanel; //RectTransform으로 해도 됨.
    public TextMeshProUGUI TotalGoldText;

    public TextMeshProUGUI ScrollViewText_item1;
    public TextMeshProUGUI ScrollViewText_item2;
    public TextMeshProUGUI ScrollViewText_item3;
    public TextMeshProUGUI ScrollViewText_item4;


    private GameObject Player;
    private PlayerStat stat;
    private NPC1_shopslot[] slots;
    public Transform slotHolder;
    public Dictionary<string, ReadOnlyCollection<Item>> shopitemDB;
    private Slot[] playerslots;
    public Transform playerslotHolder; //content 이며 인벤토리 슬롯
    public string NPCname;

    void Start()
    {
        playerslotHolder = PlayerInventory.Instance._player_Inven_Content.transform;

        NPCname = "브로아";
        shopitemDB = ItemDataBase.instance.GetAllItems(); //아이템 데이터베이스 복사 
        TotalGoldText.text = "0";
        Player = Managers.Game.GetPlayer();
        stat =  Player.GetComponent<PlayerStat>();
        slots = slotHolder.GetComponentsInChildren<NPC1_shopslot>();
        playerslots = playerslotHolder.GetComponentsInChildren<Slot>(); //플레이어 슬롯 참조 (샵모드 불리언 검사)

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;            
        }
        
        if(slots.Length >= 4 && slots != null)
        {
            slots[0].shopitem = shopitemDB["Consumable"][0]; 
            slots[1].shopitem = shopitemDB["Consumable"][1];
            slots[2].shopitem = shopitemDB["Consumable"][2];
            slots[3].shopitem = shopitemDB["Weapon_TwoHand"][0];
        }
       
    }

    void Update()
    {
        PrintTotalGold(); //TDOO :성능저하  
    }

    public void Enter()
    {
        Canvas canvas = GameObject.Find("SHOP CANVAS").gameObject.GetComponent<Canvas>();
        Managers.UI.SetCanvas(canvas.gameObject, true);
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
            ScrollViewText_item1.text = "소지금이 부족합니다.";
            ScrollViewText_item2.text = "";
            ScrollViewText_item3.text = "";
            ScrollViewText_item4.text = "";
            
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
                ScrollViewText_item1.text = "가방 칸이 부족합니다.";
                ScrollViewText_item2.text = "";
                ScrollViewText_item3.text = "";
                ScrollViewText_item4.text = "";

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
            if (totalquantity+ PlayerInventory.Instance.player_items.Count > 20)
            {
                Managers.Sound.Play("Coin");
                ScrollViewText_item1.text = "가방 칸이 모자라 구매 할 수 없습니다.";
                ScrollViewText_item2.text = "";
                ScrollViewText_item3.text = "";
                ScrollViewText_item4.text = "";


                totalquantity = 0; //토탈갯수 검사 초기화
                return;
            }
            
            
            Debug.Log("구매 완료");
            Managers.Sound.Play("Coin");
            ScrollViewText_item1.text = "구매완료";
            ScrollViewText_item2.text = "";
            ScrollViewText_item3.text = "";
            ScrollViewText_item4.text = "";

            stat.Gold -= total;
            stat.onchangestat.Invoke();

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
                            PlayerInventory.Instance.AddItem(shopitemDB["Consumable"][0].Clone());
                            
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
                            PlayerInventory.Instance.AddItem(shopitemDB["Consumable"][1].Clone());
                            
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

                        for (int i2 = 0; i2 < quan2; i2++)
                        {
                            PlayerInventory.Instance.AddItem(shopitemDB["Consumable"][2].Clone());

                        }
                        totalquantity = 0; //구매 후 토탈갯수 초기화
                        break;


                    case 3:

                        int quan3 = slots[3].quantity;
                        Debug.Log(quan3);
                        if (quan3 == 0)
                        {
                            break;
                        }

                        for (int i3 = 0; i3 < quan3; i3++)
                        {
                            PlayerInventory.Instance.AddItem(shopitemDB["Weapon_TwoHand"][0].Clone());

                        }
                        totalquantity = 0; //구매 후 토탈갯수 초기화

                        break;

                }
            }
           
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
