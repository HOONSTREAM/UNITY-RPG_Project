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
    public GameObject Unique_Particle;
    public TextMeshProUGUI amount_text;
    public bool isShopMode = false;
    public bool isStorageMode = false;
    public GameObject Sell_Panel; // ����â�� �������� �� �������� �� �� �ִ� â
    public GameObject Equip_Drop_Panel; // ���������� ������ �� ������ â
    public GameObject Use_Drop_Panel; // �Ҹ�������� ������ �� ������ â
    public GameObject Drop_Panel; // ��Ÿ�������� ������ �� ������ â
    public GameObject Storage_Input_Console;


    void Start()
    {       
        equiped_image.gameObject.SetActive(false); //�ʱ�ȭ (üũǥ�� ����)                                                 
        amount_text.text = "";      
    }

    public void UpdateSlotUI()
    {
        
        itemicon.sprite = item.itemImage;
        itemicon.gameObject.SetActive(true);
        Unique_Particle.gameObject.SetActive(false);
        

        if (item.Equip) //������ üũ(����ǥ��) ��˻� 
        {
            equiped_image.gameObject.SetActive(true);

        }
        if (item.itemtype == ItemType.Equipment)
        {
            amount_text.text = "";

         if(item.itemrank == ItemRank.Rare || item.itemrank == ItemRank.Unique || item.itemrank == ItemRank.Legend) // ������ ��ũ�� ���� �̻��̸�, ��ƼŬ Ȱ��ȭ
            {
                Unique_Particle.gameObject.SetActive(true);
            }
        
        }

        else if (item.itemtype == ItemType.Consumables || item.itemtype == ItemType.Etc)
        {
            amount_text.text = item.amount.ToString();

            if (item.itemrank == ItemRank.Rare || item.itemrank == ItemRank.Unique || item.itemrank == ItemRank.Legend) // ������ ��ũ�� ���� �̻��̸�, ��ƼŬ Ȱ��ȭ
            {
                Unique_Particle.gameObject.SetActive(true);
            }
           
        }
    
    }
       
    public void RemoveSlot()
    {
        item = null;
        itemicon.gameObject.SetActive(false);
        equiped_image.gameObject.SetActive(false); //üũǥ�� ��ü���� (������Ʈ)
        Unique_Particle.gameObject.SetActive(false);
        amount_text.text = "";
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       

        if (isShopMode) //����â�� ����������� �����ϴ� ����
        {          
            if (item.Equip)
            {
                GameObject player = Managers.Game.GetPlayer();
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("�������� ���� �Ǹ��� �� �����ϴ�.");
                return;
            }

            GameObject GUI = GameObject.Find("GUI_Console").gameObject;
            Sell_Console sell = GUI.GetComponent<Sell_Console>();
            sell.Get_Slotnum(slotnum); //slot�� ���� ������ sellconsole ��ũ��Ʈ�� �Ѱ���
            Managers.Sound.Play("Coin");
            Sell_Panel.gameObject.SetActive(true);           
            return;

        }

        else if (isStorageMode) //â�����ΰ�� (�ݰ�â�� �������� ���)
        {
            
            if(item == null)
            {
                return;
            }

            if (item.Equip) //�������� ���� �ݰ� �ñ� �� ����.
            {
                GameObject player = Managers.Game.GetPlayer();
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("�������� ���� �ñ� �� �����ϴ�.");
                return;
            }
            
            
            if(item.itemtype == ItemType.Consumables)
            {
                if(item.amount > 1)
                {
                    GameObject GUI = GameObject.Find("GUI_Console").gameObject;
                    Storage_Input_Console storage = GUI.GetComponent<Storage_Input_Console>();
                    storage.Get_Slotnum(slotnum); //slot�� ���� ������ storage_input_console ��ũ��Ʈ�� �Ѱ���
                    Managers.Sound.Play("Coin");
                    Storage_Input_Console.SetActive(true); //�����Է�â ���� �� �����Է�â ��ũ��Ʈ���� ���� ó��
                    return;
                }

                else
                {
                    PlayerStorage.Instance.AddItem(this.item);
                    PlayerInventory.Instance.RemoveItem(this.slotnum);
                    return;
                }
            }
            // ����� ���

            PlayerStorage.Instance.AddItem(this.item);
            PlayerInventory.Instance.RemoveItem(this.slotnum);

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
            else if(this.item.itemtype == ItemType.Consumables)// �Ҹ�ǰ�ΰ�� 
            {
                GameObject go = GameObject.Find("NewInvenUI").gameObject;
                Slot_Equip_Drop equip_drop = go.GetComponent<Slot_Equip_Drop>();
                equip_drop.Get_Slotnum(slotnum); //slot�� ���� ������ sellconsole ��ũ��Ʈ�� �Ѱ���
                Use_Drop_Panel.SetActive(true);
                Use_Drop_Panel.transform.position = Input.mousePosition;

            }

            else // ��Ÿ������
            {
                GameObject go = GameObject.Find("NewInvenUI").gameObject;
                Slot_Equip_Drop equip_drop = go.GetComponent<Slot_Equip_Drop>();
                equip_drop.Get_Slotnum(slotnum); //slot�� ���� ������ sellconsole ��ũ��Ʈ�� �Ѱ���
                Drop_Panel.SetActive(true);
                Drop_Panel.transform.position = Input.mousePosition;
            }

        }
      
    }

   
}

