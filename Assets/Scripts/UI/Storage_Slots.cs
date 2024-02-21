using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Storage_Slots : MonoBehaviour, IPointerUpHandler
{
    public int slotnum;

    public Item item;
    public Image itemicon;
    public GameObject Unique_Particle;
    public Image equiped_image;
    public TextMeshProUGUI amount_text;
    public bool isShopMode = false;
    public bool isStorageMode = false;
    public GameObject WithDrawPanel;
    
    void Start()
    {
        equiped_image.gameObject.SetActive(false); //�ʱ�ȭ (üũǥ�� ����)
        itemicon.gameObject.SetActive(false); //�ʱ�ȭ (������ ǥ�� ����)
        Unique_Particle.gameObject.SetActive(false);
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

            if (item.itemrank == ItemRank.Rare || item.itemrank == ItemRank.Unique || item.itemrank == ItemRank.Legend) // ������ ��ũ�� ���� �̻��̸�, ��ƼŬ Ȱ��ȭ
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

       if(item.itemtype == ItemType.Consumables || item.itemtype == ItemType.Etc)
        {
   
            if(item.amount > 1)
            {

                GameObject GUI = GameObject.Find("GUI_Console").gameObject;
                Storage_WithDraw_Console storage = GUI.GetComponent<Storage_WithDraw_Console>();
                storage.Get_Slotnum(slotnum); //slot�� ���� ������ storage_input_console ��ũ��Ʈ�� �Ѱ���
                Managers.Sound.Play("Coin");
                WithDrawPanel.SetActive(true);
            }

            else
            {
                PlayerInventory.Instance.AddItem(item.Clone());
                PlayerStorage.Instance.RemoveItem(this.slotnum);              
            }

            return;
        }

       else if(item.itemtype == ItemType.Equipment)
        {
            

            PlayerInventory.Instance.AddItem(item.Clone());
            PlayerStorage.Instance.RemoveItem(this.slotnum);

            return;
        }


    }

}
