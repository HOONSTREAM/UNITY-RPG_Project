using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;
// TODO ���� Ȯ�强�� ���� ����� �ؾ���.
public class Shop : MonoBehaviour
{

    public GameObject ShopPanel; //RectTransform���� �ص� ��.
    public TextMeshProUGUI TotalGoldText;

    public TextMeshProUGUI ScrollViewText;
    



    GameObject Player;
    PlayerStat stat;
    ShopSlot[] slots;
    public Transform slotHolder;
    public List<Item> shopitemDB;
    Slot[] playerslots;
    public Transform playerslotHolder;
    public string NPCname;


    void Start()
    {
        NPCname = "���彽���� �ɳ�";
        shopitemDB = ItemDataBase.instance.itemDB; //�����ͺ��̽� ���� 
        TotalGoldText.text = "0";
        Player = GameObject.Find("UnityChan").gameObject;
        stat =  Player.GetComponent<PlayerStat>();
        slots = slotHolder.GetComponentsInChildren<ShopSlot>();
        playerslots = playerslotHolder.GetComponentsInChildren<Slot>(); //�÷��̾� ���� ���� (����� �Ҹ��� �˻�)

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
            slots[i].shopitem = shopitemDB[i]; //�� ���Կ� �������۵����� ��� 
        }
     
    }

    void Update()
    {
        PrintTotalGold(); 
    }

    public void Enter()
    {      
        ShopPanel.SetActive(true);
        
        for(int i = 0; i<playerslots.Length; i++)
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

                        #region ���� ����
                        /*����Ʈ���� Ŭ������ �� ������ ����ȭ�� ������ ����Ʈ�� ����Ǵ� ���� �ƴ϶� �����ּҰ� ����Ǵ� ���̱� ������ ���� ���������� �ν��ϴ� ���� �߻� : 
                          for(int i0 = 0; i0<quan ; i0++)  
                        {
                            PlayerInventory.Instance.AddItem(shopitemDB[0]); 
                            PlayerInventory.Instance.onChangeItem.Invoke();
                        }                       

                        ��������/ �������� ����, Ŭ������ �⺻������ ��������̴�. Ŭ���Լ��� ���� �ذ� 
                        
                         */
                        #endregion

                        for (int i0 = 0; i0<quan ; i0++) 
                        {
                            PlayerInventory.Instance.AddItem(shopitemDB[0].Clone()); 
                            PlayerInventory.Instance.onChangeItem.Invoke();
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
                            PlayerInventory.Instance.AddItem(shopitemDB[1].Clone());
                            PlayerInventory.Instance.onChangeItem.Invoke();

                        }
                        totalquantity = 0; //���� �� ��Ż���� �ʱ�ȭ
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
                            PlayerInventory.Instance.AddItem(shopitemDB[2].Clone());
                            PlayerInventory.Instance.onChangeItem.Invoke();

                        }
                        totalquantity = 0; //���� �� ��Ż���� �ʱ�ȭ
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
