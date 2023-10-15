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
    public TextMeshProUGUI ScrollViewText;
    GameObject Player;
    PlayerStat stat;
    ShopSlot[] slots;
    public Transform slotHolder;
    
    


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
            Debug.Log($"{total}골드가 필요하지만 당신은 {stat.Gold}를 갖고 있으므로 돈이 부족하다.");
            Managers.Sound.Play("Coin");
            ScrollViewText.text = "소지금이 부족합니다.";
            
            return;

        }

        else
        {
            if(total == 0)
            {
                return;
            }
            Debug.Log("구매 완료");
            Managers.Sound.Play("Coin");
            ScrollViewText.text = "구매 완료하였습니다.";
            stat.Gold -= total;
            

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
