using System.Collections;
using System.Collections.Generic;
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
 
    void Start()
    {
        equiped_image.gameObject.SetActive(false); //�ʱ�ȭ (üũǥ�� ����)
        itemicon.gameObject.SetActive(false); //�ʱ�ȭ (������ ǥ�� ����)

    }
    public void UpdateSlotUI()
    {
        itemicon.sprite = item.itemImage;
        itemicon.gameObject.SetActive(true);

        if (item.Equip) //������ üũ(����ǥ��) ��˻� 
        {
            equiped_image.gameObject.SetActive(true);

        }
    }
       
   
    public void RemoveSlot()
    {
        item = null;
        itemicon.gameObject.SetActive(false);
        equiped_image.gameObject.SetActive(false); //üũǥ�� ��ü���� (������Ʈ)
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        bool isUse = item.Use();

        if (isUse)
        {
            if (item.itemtype == ItemType.Equipment)
            {
                if (item.Equip) //�̹� �������ΰ�� �ٽ� ������� ��������
                {
                    PlayerEquipment.Instance.UnEquipItem(item);
                    return;
                }

               PlayerEquipment.Instance.EquipItem(item); //������ ���� �Լ�
                if (item.Equip)
                {
                    equiped_image.gameObject.SetActive(true); //üũǥ��
                }
                else //������ �� ������� (������ ��ĥ���)
                {
                    equiped_image.gameObject.SetActive(false); //üũǥ�� ����
                }
               
       
            }

            else //�Ҹ�ǰ�ϰ��
            {

                PlayerInventory.Instance.RemoveItem(slotnum);
            }

        }
           
            
        }
    }

