using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Storage_Input_Console : MonoBehaviour
{
    public TMP_InputField inputamounttext;
    public int inputamount = 0; // 맡길 갯수
    private GameObject canvas; //스크립트가 등록되어있는 캔버스
    public Slot[] slots; //플레이어 슬롯 참조
    public Item slot_item; // 슬롯에 해당하는 아이템 참조
    public int slot_number; // 슬롯의 번호 참조
    public GameObject Input_Console; //해당 인풋콘솔
    void Awake()
    {
        canvas = GameObject.Find("GUI").gameObject;
        GameObject go = GameObject.Find("NewInvenUI").gameObject;
        slots = go.GetComponentsInChildren<Slot>();
        
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
    }
    public Item Get_Slotnum(int slotnum) //슬롯에 있는 아이템을 참조받아 private Item 변수에 저장해두고, 그 슬롯의 넘버도 보관
    {
        slot_number = slotnum;     
        return slot_item = slots[slotnum].item;
    }

    public void ButtonInput()
    {
        Input_Console.SetActive(false);

        if (slot_item == null)
        {          
            return;
        }
     
        inputamount = int.Parse(inputamounttext.text);

        
        if (inputamount > slot_item.amount)
        {
            GameObject player = Managers.Game.GetPlayer();
            PlayerStat stat = player.GetComponent<PlayerStat>();
            stat.PrintUserText("맡기려는 갯수에 비해 소지중인 물품 갯수가 적습니다.");
            return;
        }


        for(int i = 0; i< inputamount; i++) // 클론함수(같은것을 참조방지)를 이용하여 입력된 갯수만큼 창고에 생성
        {
            Debug.Log("스토리지 for문 시작");
            PlayerStorage.Instance.AddItem(slot_item.Clone());           
        }
        for(int i = 0; i<inputamount; i++) // 입력된 갯수만큼 인벤토리에서 제거
        {
            Debug.Log("인벤토리 for문 시작");
            PlayerInventory.Instance.RemoveItem(slot_number);         
        }


        inputamount = 0; // 저장된 갯수 초기화
        inputamounttext.text = " ";

        return;

    }
  
}
