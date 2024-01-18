using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Equipment_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject equipment_panel;
    [SerializeField]
    private GameObject Inventory_canvas;

    private PlayerEquipment _player_now_equip; //�÷��̾� ���â ����
    private PlayerStat stat; //�÷��̾� ���� ���� 
    public Equip_Slot[] upper_equip_slots;
    public Equip_Slot[] middle_equip_slots;
    public Equip_Slot[] middle2_equip_slots;
    public Equip_Slot[] bottom_equip_slots;
    public Equip_Slot[] bottom2_equip_slots;

    public Transform upper_equip_slot_holder;
    public Transform middle_equip_slot_holder;
    public Transform middle2_equip_slot_holder;
    public Transform bottom_equip_slot_holder;
    public Transform bottom2_equip_slot_holder;


    #region �κ��丮 ����
    public Slot[] slots;
    public Transform slotHolder;
    #endregion

    public bool active_equipment_panel = false;

    private void Start()
    {

        stat = GetComponent<PlayerStat>(); //��� ������Ʈ�� ���� �÷��̾� ���� ����
        _player_now_equip = PlayerEquipment.Instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        equipment_panel.SetActive(active_equipment_panel);
        _player_now_equip.onChangeEquip += RedrawSlotUI;
        Managers.UI.SetCanvas(Inventory_canvas, true);
        //�κ��丮 �巡�� �����ϵ��� �ϴ� �̺�Ʈ
        UI_Base.BindEvent(equipment_panel, (PointerEventData data) => { equipment_panel.transform.position = data.position; }, Define.UIEvent.Drag);

        #region ���â ���� (����)
        upper_equip_slots = upper_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        middle_equip_slots = middle_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        middle2_equip_slots = middle2_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        bottom_equip_slots = bottom_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        bottom2_equip_slots = bottom2_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        #endregion

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            active_equipment_panel = !active_equipment_panel;
            equipment_panel.SetActive(active_equipment_panel);
            RedrawSlotUI(); // �������� �ƹ��͵� ���� ��, ������ �ߴ� ���� ���� 
            Managers.Sound.Play("Inven_Open");
        }

    }


    public void Xbutton_Exit()
    {
        if (equipment_panel != null && equipment_panel.activeSelf)
        {
            active_equipment_panel = !active_equipment_panel;
            equipment_panel.SetActive(active_equipment_panel);
            Managers.Sound.Play("Inven_Open");
           
        }

        return;
    }

    public void Equipment_panel_Button_Open()
    {
        active_equipment_panel = !active_equipment_panel;
        equipment_panel.SetActive(active_equipment_panel);
        Managers.Sound.Play("Inven_Open");

    }

    void RedrawSlotUI()
    {
        #region ���� �ѹ� �Ҵ�
        for (int i = 0; i < upper_equip_slots.Length; i++)
        {
            upper_equip_slots[i].slotnum = i;
        }
        for (int i = 0; i < middle_equip_slots.Length; i++)
        {
            middle_equip_slots[i].slotnum = i;
        }
        for (int i = 0; i < middle2_equip_slots.Length; i++)
        {
            middle2_equip_slots[i].slotnum = i;
        }
        for (int i = 0; i < bottom_equip_slots.Length; i++)
        {
            bottom_equip_slots[i].slotnum = i;
        }
        for (int i = 0; i < bottom2_equip_slots.Length; i++)
        {
            bottom2_equip_slots[i].slotnum = i;
        }
        #endregion
        #region ���� ���� ����
        for (int i = 0; i < upper_equip_slots.Length; i++)
        {
            upper_equip_slots[i].RemoveSlot();
        }
        for (int i = 0; i < middle_equip_slots.Length; i++)
        {
            middle_equip_slots[i].RemoveSlot();
        }
        for (int i = 0; i < middle2_equip_slots.Length; i++)
        {
            middle2_equip_slots[i].RemoveSlot();
        }
        for (int i = 0; i < bottom_equip_slots.Length; i++)
        {
            bottom_equip_slots[i].RemoveSlot();
        }
        for (int i = 0; i < bottom2_equip_slots.Length; i++)
        {
            bottom2_equip_slots[i].RemoveSlot();
        }
        #endregion


        for (int i = 0; i < _player_now_equip.player_equip.Count; i++) //����Ʈ�迭�� ����Ǿ��ִ� �κ��丮�� ������������ �޾ƿ� �ٽ� ������ 
        {
            bool upper_equip_boolean = _player_now_equip.player_equip.TryGetValue(EquipType.Head, out Item item);   
            
            if (upper_equip_boolean == false)
            {
                upper_equip_slots[i].item = null;
                upper_equip_slots[i].itemicon.gameObject.SetActive(false);
                upper_equip_slots[i].Unique_Particle.gameObject.SetActive(false);
                upper_equip_slots[i].UpdateSlotUI();
                return;            
            }

            upper_equip_slots[i].item = item;
            upper_equip_slots[i].UpdateSlotUI();
        }

    }

}
