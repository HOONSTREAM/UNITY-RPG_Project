using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Equipment_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject equipment_panel;
    [SerializeField]
    private GameObject Inventory_canvas;

    private PlayerEquipment _player_now_equip; //플레이어 장비창 참조
    private PlayerStat stat; //플레이어 스텟 참조 
    public Equip_Slot[] upper_equip_slots; // Necklace,Head,Head_Deco
    public Equip_Slot[] middle_equip_slots; // Weapon, Shield
    public Equip_Slot[] middle2_equip_slots; // Ring 1 , Ring 2
    public Equip_Slot[] bottom_equip_slots; // chest , pants, outter_plate
    public Equip_Slot[] bottom2_equip_slots; // shoes, cape , vehicle

    public Transform upper_equip_slot_holder;
    public Transform middle_equip_slot_holder;
    public Transform middle2_equip_slot_holder;
    public Transform bottom_equip_slot_holder;
    public Transform bottom2_equip_slot_holder;

    #region 장비창 플레이어 스텟창 인스턴스변수

    private GameObject nametxt;
    private GameObject jobtxt;
    private GameObject guildtxt;
    private GameObject fametxt;
    private GameObject atktxt;
    private GameObject hptxt;
    private GameObject mptxt;
    private GameObject mvspeedtxt;
    private GameObject deftxt;


    #endregion

    #region 인벤토리 참조
    public Slot[] slots;
    public Transform slotHolder;
    #endregion

    public bool active_equipment_panel = false;

    private void Start()
    {
        nametxt = GameObject.Find("stat_name").gameObject;
        jobtxt = GameObject.Find("stat_job").gameObject;
        guildtxt = GameObject.Find("stat_guild").gameObject;
        fametxt = GameObject.Find("stat_fame").gameObject;
        atktxt = GameObject.Find("stat_atk").gameObject;
        hptxt = GameObject.Find("stat_hp").gameObject;
        mptxt = GameObject.Find("stat_mp").gameObject;
        mvspeedtxt = GameObject.Find("stat_speed").gameObject;
        deftxt = GameObject.Find("stat_def").gameObject;

        stat = Managers.Game.GetPlayer().GetComponent<PlayerStat>(); //골드 업데이트를 위한 플레이어 스텟 참조
        _player_now_equip = PlayerEquipment.Instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        equipment_panel.SetActive(active_equipment_panel);
        _player_now_equip.onChangeEquip += RedrawSlotUI;
        Managers.UI.SetCanvas(Inventory_canvas, true);
        //인벤토리 드래그 가능하도록 하는 이벤트
        UI_Base.BindEvent(equipment_panel, (PointerEventData data) => { equipment_panel.transform.position = data.position; }, Define.UIEvent.Drag);

        #region 장비창 슬롯 (구분)
        upper_equip_slots = upper_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        middle_equip_slots = middle_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        middle2_equip_slots = middle2_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        bottom_equip_slots = bottom_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        bottom2_equip_slots = bottom2_equip_slot_holder.GetComponentsInChildren<Equip_Slot>();
        #endregion

    }

    public void OnUpdateEquip_Stat_Panel_UI()
    {
        nametxt.GetComponent<TextMeshProUGUI>().text = Managers.Game.GetPlayer().name;
        jobtxt.GetComponent<TextMeshProUGUI>().text = "무직"; //TODO 
        guildtxt.GetComponent<TextMeshProUGUI>().text = "길드없음";
        fametxt.GetComponent<TextMeshProUGUI>().text = "0"; //TODO
        atktxt.GetComponent<TextMeshProUGUI>().text = $"{(stat.ATTACK * 0.8)} ~ {(stat.ATTACK * 1.1)}";
        hptxt.GetComponent<TextMeshProUGUI>().text = $"{stat.MaxHp}";
        mptxt.GetComponent<TextMeshProUGUI>().text = "0";
        mvspeedtxt.GetComponent<TextMeshProUGUI>().text = $"{(stat.MoveSpeed)*20}%";
        deftxt.GetComponent<TextMeshProUGUI>().text = $"{stat.Defense}";

    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            active_equipment_panel = !active_equipment_panel;
            equipment_panel.SetActive(active_equipment_panel);
            Managers.UI.SetCanvas(Inventory_canvas, true);
            RedrawSlotUI(); // 아이템이 아무것도 없을 때, 툴팁이 뜨는 것을 방지 
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
        Managers.UI.SetCanvas(Inventory_canvas, true);
        Managers.Sound.Play("Inven_Open");

    }

    private void RedrawSlotUI()
    {
      
        #region 슬롯 넘버 할당
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
        #region 슬롯 전부 제거
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


        foreach (EquipType equipType in _player_now_equip.player_equip.Keys) // 현재 장착중인 장비의 딕셔너리 조사 
        {
            bool equip_boolean = _player_now_equip.player_equip.TryGetValue(equipType, out Item item);

            Debug.Log($"{equipType}: {equip_boolean}");

            switch (item.equiptype)
            {
                case EquipType.Head:
                    upper_equip_slots[1].item = item;
                    upper_equip_slots[1].UpdateSlotUI();
                    break;
                case EquipType.necklace:
                    upper_equip_slots[0].item = item;
                    upper_equip_slots[0].UpdateSlotUI();
                    break;
                case EquipType.Head_decoration:
                    upper_equip_slots[2].item = item;
                    upper_equip_slots[2].UpdateSlotUI();
                    break;
                case EquipType.Weapon:
                    middle_equip_slots[0].item = item;
                    middle_equip_slots[0].UpdateSlotUI();
                    break;
                case EquipType.Shield:
                    middle_equip_slots[1].item = item;
                    middle_equip_slots[1].UpdateSlotUI();
                    break;
                case EquipType.Chest:
                    bottom_equip_slots[0].item = item;
                    bottom_equip_slots[0].UpdateSlotUI();
                    break;
                case EquipType.pants:
                    bottom_equip_slots[1].item = item;
                    bottom_equip_slots[1].UpdateSlotUI();
                    break;
                case EquipType.Ring:
                    if (middle2_equip_slots[0] == null)
                    {
                        middle2_equip_slots[0].item = item;
                        middle2_equip_slots[0].UpdateSlotUI();
                    }
                    else // 0번칸에 아이템이 있으면 1번칸에 장착
                    {
                        middle2_equip_slots[1].item = item;
                        middle2_equip_slots[1].UpdateSlotUI();
                    }
                    break;
                case EquipType.outter_plate:
                    bottom_equip_slots[2].item = item;
                    bottom_equip_slots[2].UpdateSlotUI();
                    break;
                case EquipType.cape:
                    bottom2_equip_slots[1].item = item;
                    bottom2_equip_slots[1].UpdateSlotUI();
                    break;
                case EquipType.vehicle:
                    bottom2_equip_slots[2].item = item;
                    bottom2_equip_slots[2].UpdateSlotUI();
                    break;
                case EquipType.shoes:
                    bottom2_equip_slots[0].item = item;
                    bottom2_equip_slots[0].UpdateSlotUI();
                    break;

            }

        }


        return;
    }

}
