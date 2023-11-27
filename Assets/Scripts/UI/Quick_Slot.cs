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
        
        itemicon.gameObject.SetActive(false); //초기화 (아이콘 표시 안함)
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
            stat.PrintUserText("퀵슬롯에 아이템이 없습니다.");

            return;
        }
        if (item.itemtype == ItemType.Consumables)
        {
           bool isUsed =  this.item.Use();

            if (isUsed)
            
            {
                for(int i = 0; i< playerslots.Length; i++)
                {
                    if (playerslots[i].item == this.item) //인벤토리에 같은 아이템이 있는지 검사하고 그 같은아이템도 삭제(invoke 포함됨)
                    {
                        PlayerInventory.Instance.RemoveItem(playerslots[i].slotnum);
                        break; //한번 만족했으면 반복문을 빠져나가야 한다. (선택된 퀵슬롯 기준 뒷 퀵슬롯 전부 사라지는 문제 해결)
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