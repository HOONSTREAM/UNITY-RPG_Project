using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using static UnityEditor.Progress;

public class QuickSlot_Script : MonoBehaviour
{
    PlayerQuickSlot quickslot; //�÷��̾� ������ ����
    PlayerStat stat; //�÷��̾� ���� ���� (��������Ʈ)

    public Item item;
    public Quick_Slot[] quick_slot;
    public Transform quickslot_holder;
    public Slot[] Player_slots;
    public Transform player_slotHolder;

   

    void Start()
    {
        stat = GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
        quickslot = PlayerQuickSlot.Instance;
        quick_slot = quickslot_holder.GetComponentsInChildren<Quick_Slot>();
        Player_slots = player_slotHolder.GetComponentsInChildren<Slot>();

        quickslot.onChangeItem += RedrawSlotUI;  // Invoke �Լ� ��� �̺�Ʈ �߻����� �Լ� ȣ��

        RedrawSlotUI();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (PlayerQuickSlot.Instance.quick_slot_item.Count == 0)
            {
                GameObject player = Managers.Game.GetPlayer();
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("�����Կ� �������� �����ϴ�.");
                return;
            }

            item = PlayerQuickSlot.Instance.quick_slot_item[0];
            
            Item_Use(item);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (PlayerQuickSlot.Instance.quick_slot_item.Count == 0)
            {
                GameObject player = Managers.Game.GetPlayer();
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("�����Կ� �������� �����ϴ�.");
                return;
            }

            item = PlayerQuickSlot.Instance.quick_slot_item[1];

            Item_Use(item);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (PlayerQuickSlot.Instance.quick_slot_item.Count == 0)
            {
                GameObject player = Managers.Game.GetPlayer();
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("�����Կ� �������� �����ϴ�.");
                return;
            }

            item = PlayerQuickSlot.Instance.quick_slot_item[2];

            Item_Use(item);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (PlayerQuickSlot.Instance.quick_slot_item.Count == 0)
            {
                GameObject player = Managers.Game.GetPlayer();
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("�����Կ� �������� �����ϴ�.");
                return;
            }

            item = PlayerQuickSlot.Instance.quick_slot_item[3];

            Item_Use(item);
        }


    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < quick_slot.Length; i++)
        {
            quick_slot[i].slotnum = i;
        }

        for (int i = 0; i < quick_slot.Length; i++) //�� �о������
        {
            quick_slot[i].RemoveSlot();
        }

        for (int i = 0; i < quickslot.quick_slot_item.Count; i++) //����Ʈ�迭�� ����Ǿ��ִ� �κ��丮�� ������������ �޾ƿ� �ٽ� ������ 
        {
            quick_slot[i].item = quickslot.quick_slot_item[i];
            quick_slot[i].UpdateSlotUI();

        }
    }

    public void Item_Use(Item item)
    {
        
        if (item.itemtype == ItemType.Consumables)
        {
            bool isUsed = this.item.Use();

            if (isUsed)

            {
                for (int i = 0; i < Player_slots.Length; i++)
                {
                    if (Player_slots[i].item == this.item) //�κ��丮�� ���� �������� �ִ��� �˻��ϰ� �� ���������۵� ����(invoke ���Ե�)
                    {
                        PlayerInventory.Instance.RemoveItem(Player_slots[i].slotnum);
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
