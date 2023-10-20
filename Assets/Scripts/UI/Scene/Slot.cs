using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerUpHandler
{
    
    public int slotnum;
    
    public Item item;
    public Image itemicon;
    public Image equiped_image;
    public TextMeshProUGUI amount_text;
    public bool isShopMode = false;
    public GameObject Sell_Panel;
    
    

    void Start()
    {
        
        equiped_image.gameObject.SetActive(false); //�ʱ�ȭ (üũǥ�� ����)
        itemicon.gameObject.SetActive(false); //�ʱ�ȭ (������ ǥ�� ����)
        amount_text.text = "";
        
        

    }
    public void UpdateSlotUI()
    {
        itemicon.sprite = item.itemImage;
        itemicon.gameObject.SetActive(true);

        if (item.Equip) //������ üũ(����ǥ��) ��˻� 
        {
            equiped_image.gameObject.SetActive(true);

        }
        if (item.itemtype == ItemType.Equipment)
        {
            amount_text.text = "";
        }

        else if (item.itemtype == ItemType.Consumables)
        {
            amount_text.text = item.amount.ToString();
        }

       
      
    }
       
    public void RemoveSlot()
    {
        item = null;
        itemicon.gameObject.SetActive(false);
        equiped_image.gameObject.SetActive(false); //üũǥ�� ��ü���� (������Ʈ)
        amount_text.text = "";
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isShopMode) 
        {
            Debug.Log("������ �Ǹ��մϴ�.");
            if (item.Equip)
            {
                GameObject gos = GameObject.Find("UnityChan").gameObject;
                PlayerStat stats = gos.GetComponent<PlayerStat>();
                stats.PrintUserText("�������� ���� �Ǹ��� �� �����ϴ�.");
                return;
            }

            GameObject GUI = GameObject.Find("GUI").gameObject;
            Sell_Console sell = GUI.GetComponent<Sell_Console>();
            sell.Get_Slotnum(slotnum); //slot�� ���� ������ sellconsole ��ũ��Ʈ�� �Ѱ���
            Managers.Sound.Play("Coin");
            Sell_Panel.gameObject.SetActive(true);
            
            return;
        }

        else //������尡 �ƴѰ�� ����/���� ���
        {
            bool isUse = item.Use();

            if (isUse)
            {
                if (item.itemtype == ItemType.Equipment)
                {
                    if (item.Equip) //�̹� �������ΰ�� �ٽ� ������� ��������
                    {
                        PlayerEquipment.Instance.UnEquipItem(this);
                        equiped_image.gameObject.SetActive(false); //�������� �� üũǥ�� ����

                        return;
                    }

                    else
                    {
                        PlayerEquipment.Instance.EquipItem(this); //������ ���� �Լ�
                        if (item.Equip)
                        {
                            equiped_image.gameObject.SetActive(true); //üũǥ��
                          
                        }
                        else //������ �� ������� (������ ��ĥ���)
                        {
                            equiped_image.gameObject.SetActive(false); //üũǥ�� ����
                        }

                    }

                }

                else //�Ҹ�ǰ�ϰ��
                {

                    PlayerInventory.Instance.RemoveItem(slotnum);

                    if (item == null)
                    {
                        return;
                    }
                }

            }
        
        }

      

    }

   

}

