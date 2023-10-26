using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPC1_shop : MonoBehaviour
{

    public GameObject ShopPanel; //RectTransform���� �ص� ��.
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
        shopitemDB = ItemDataBase.instance.itemDB; //�����ͺ��̽� ���� 
        TotalGoldText.text = "0";
        Player = GameObject.Find("UnityChan").gameObject;
        stat =  Player.GetComponent<PlayerStat>();
        slots = slotHolder.GetComponentsInChildren<NPC1_shopslot>();
        playerslots = playerslotHolder.GetComponentsInChildren<Slot>(); //�÷��̾� ���� ���� (����� �Ҹ��� �˻�)

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
            
        }
        //���� ����� ������
        slots[0].shopitem = shopitemDB[3]; //�� ���Կ� �������۵����� ��� 
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
        for (int i = 0; i < slots.Length; i++) //���� �ʱ�ȭ
        {
            slots[i].ResetShop();

        }

        for (int i = 0; i < playerslots.Length; i++)
        {
            playerslots[i].isShopMode = false;
        }

    }
    #region ���� ����/�Ǹ� �ڵ�
    

    int totalquantity = 0; // ��Ż ���Ű��� �˻��Ͽ� ���氹������ ������ ����
    public void Buy()
    {
        int total = Convert.ToInt32(TotalGoldText.text);

        if(stat.Gold < total)
        {
         
            Managers.Sound.Play("Coin");
            ScrollViewText.text = "�������� �����մϴ�.";
            
  
            return;

        }

        else //������ �˻� ���
        {
            if(total == 0) //��Ż���Ű�尡 0�̸� buy��ư ��Ȱ��ȭ
            {
                return;
            }

            if (PlayerInventory.Instance.player_items.Count >= 20) //���� ĭ �����˻�
            {
                Managers.Sound.Play("Coin");
                ScrollViewText.text = "������ ����á���ϴ�.";
                
                return;

            }

            // ���� ĭ ������ ������� �˻�
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].shopitem.itemtype == ItemType.Consumables)
                {
                    int tempquantity = 1; //�ӽ÷� 1���� ����, �Ҹ�ǰ�� ���û���ϹǷ� ����ĭ 1���� ���
                    totalquantity += tempquantity;
                }
                else
                {
                    totalquantity += slots[i].quantity;
                }
                
            }
            Debug.Log($"�����Ϸ��� ���� (�Ҹ�ǰ��1��) : {totalquantity}");
            Debug.Log($"���� ä���� �� : {PlayerInventory.Instance.player_items.Count}");


            if (totalquantity+ PlayerInventory.Instance.player_items.Count > 20)
            {
                Managers.Sound.Play("Coin");
                ScrollViewText.text = "���� ĭ�� ���ڶ� ������ �� �����ϴ�.";
               
                
                totalquantity = 0; //��Ż���� �˻� �ʱ�ȭ
                return;
            }
            
            
            Debug.Log("���� �Ϸ�");
            Managers.Sound.Play("Coin");
            ScrollViewText.text = "���� �Ϸ��Ͽ����ϴ�.";           
            
            stat.Gold -= total;

            for(int i = 0; i < slots.Length; i++)
            {
                switch (slots[i].slotnum) // �� ���Ժ� ���� ���� �ν�
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

                        totalquantity = 0; //���� �� ��Ż���� �ʱ�ȭ
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
                        totalquantity = 0; //���� �� ��Ż���� �ʱ�ȭ
                        break;
                    
                }
            }
           

            //TDOO ���Ժ��� ������ , ���� ���Ͽ� ������ �߰� 

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
