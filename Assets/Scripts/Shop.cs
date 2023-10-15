using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public GameObject ShopPanel; //RectTransform���� �ص� ��.
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
        for (int i = 0; i < slots.Length; i++) //���� �ʱ�ȭ
        {
            slots[i].ResetShop();

        }
    }

    public void Buy()
    {
        int total = Convert.ToInt32(TotalGoldText.text);
        if(stat.Gold < total)
        {
            Debug.Log($"{total}��尡 �ʿ������� ����� {stat.Gold}�� ���� �����Ƿ� ���� �����ϴ�.");
            Managers.Sound.Play("Coin");
            ScrollViewText.text = "�������� �����մϴ�.";
            
            return;

        }

        else
        {
            if(total == 0)
            {
                return;
            }
            Debug.Log("���� �Ϸ�");
            Managers.Sound.Play("Coin");
            ScrollViewText.text = "���� �Ϸ��Ͽ����ϴ�.";
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
