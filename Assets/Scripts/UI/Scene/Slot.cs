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
    public GameObject Equip_Drop_Panel;
    public GameObject Use_Drop_Panel;



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
            if(PlayerInventory.Instance.player_items.Count == 0)
            {
                return;
            }

            if(this.item.itemtype == ItemType.Equipment) //���������ΰ��
            {
                GameObject go = GameObject.Find("NewInvenUI").gameObject;
                Slot_Equip_Drop equip_drop = go.GetComponent<Slot_Equip_Drop>();
                equip_drop.Get_Slotnum(slotnum); //slot�� ���� ������ sellconsole ��ũ��Ʈ�� �Ѱ���
                Equip_Drop_Panel.SetActive(true);
                Equip_Drop_Panel.transform.position = Input.mousePosition;
            }
            else // �Ҹ�ǰ�ΰ�� 
            {
                GameObject go = GameObject.Find("NewInvenUI").gameObject;
                Slot_Equip_Drop equip_drop = go.GetComponent<Slot_Equip_Drop>();
                equip_drop.Get_Slotnum(slotnum); //slot�� ���� ������ sellconsole ��ũ��Ʈ�� �Ѱ���
                Use_Drop_Panel.SetActive(true);
                Use_Drop_Panel.transform.position = Input.mousePosition;

            }


        }
      

    }

   

}

