using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JEWEL_Slot_Use_Cancel : MonoBehaviour
{
    public GameObject Spiritual_stone_use_cancel_panel;
    public Item slot_item; // 슬롯에 해당하는 아이템 참조
    public int slot_number; // 슬롯의 번호 참조
    public JEWEL_Slot[] slots; //플레이어 슬롯 참조
    public PlayerStat stat;

    private const int WATER_SPRITUAL_STONE_ID = 12;


    void Start()
    {
        GameObject go = GameObject.Find("JEWEL_UI").gameObject;
        slots = go.GetComponentsInChildren<JEWEL_Slot>();
        stat = Managers.Game.GetPlayer().gameObject.GetComponent<PlayerStat>();       
    }

    public Item Get_Slotnum(int slotnum) //슬롯에 있는 아이템을 참조받아 private Item 변수에 저장해두고, 그 슬롯의 넘버도 보관
    {
        slot_number = slotnum;
        return slot_item = slots[slotnum].item;
    }

    public void Spiritual_Stone_Use()
    {
        if (slot_item == null)
        {
            Debug.Log("테스트 : 슬롯에 아무것도 없음");
            Spiritual_stone_use_cancel_panel.SetActive(false);
            return;
        }

        Spiritual_stone_use_cancel_panel.SetActive(false);

        switch (slot_item.ItemID)
        {
            case WATER_SPRITUAL_STONE_ID:
                Debug.Log("물안개의 정령석을 사용하였음.");
                break;

        }
       
    }
    public void consoleExit() => Spiritual_stone_use_cancel_panel.gameObject.SetActive(false);


}
