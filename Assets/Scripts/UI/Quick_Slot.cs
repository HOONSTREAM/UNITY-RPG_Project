using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Quick_Slot : MonoBehaviour, IPointerUpHandler
{
    public int slotnum;

    public Item item;
    public Image itemicon;
    public TextMeshProUGUI amount_text;
    public Slot[] playerslots;
    public Transform playerslot_holder;

    void Awake()
    {
        
        itemicon.gameObject.SetActive(false); //�ʱ�ȭ (������ ǥ�� ����)
        amount_text.text = "";
        playerslots = playerslot_holder.GetComponentsInChildren<Slot>();
    }


    public void UpdateSlotUI()
    {
        itemicon.sprite = item.itemImage;
        itemicon.gameObject.SetActive(true);

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
        amount_text.text = "";
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(this.item == null)
        {
            GameObject player = Managers.Game.GetPlayer();
            PlayerStat stat = player.GetComponent<PlayerStat>();
            stat.PrintUserText("�����Կ� �������� �����ϴ�.");

            return;
        }
        if (item.itemtype == ItemType.Consumables)
        {
           bool isUsed =  this.item.Use();

            if (isUsed)
            
            {
                for(int i = 0; i< playerslots.Length; i++)
                {
                    if (playerslots[i].item == this.item) //�κ��丮�� ���� �������� �ִ��� �˻��ϰ� �� ���������۵� ����(invoke ���Ե�)
                    {
                        PlayerInventory.Instance.RemoveItem(playerslots[i].slotnum);
                        break; //�ѹ� ���������� �ݺ����� ���������� �Ѵ�. (���õ� ������ ���� �� ������ ���� ������� ���� �ذ�)
                    }
                    
                }
                //PlayerQuickSlot.Instance.Quick_slot_RemoveItem(this.slotnum);
                PlayerQuickSlot.Instance.onChangeItem.Invoke();
                               
            }
           
            return;
        }

        else
        {
            return;
        }
    }










}