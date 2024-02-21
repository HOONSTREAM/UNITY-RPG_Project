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
    public GameObject Sell_Panel; // 상점창이 열려있을 때 아이템을 팔 수 있는 창
    public GameObject Equip_Drop_Panel; // 장비아이템을 눌렀을 때 열리는 창
    public GameObject Use_Drop_Panel; // 소모아이템을 눌렀을 때 열리는 창
    public GameObject Drop_Panel; // 기타아이템을 눌렀을 때 열리는 창
    public GameObject Storage_Input_Console;


    void Start()
    {       
        equiped_image.gameObject.SetActive(false); //초기화 (체크표시 안함)                                                 
        amount_text.text = "";      
    }

    public void UpdateSlotUI()
    {
        
        itemicon.sprite = item.itemImage;
        itemicon.gameObject.SetActive(true);
        Unique_Particle.gameObject.SetActive(false);
        

        if (item.Equip) //아이템 체크(장착표시) 재검사 
        {
            equiped_image.gameObject.SetActive(true);

        }
        if (item.itemtype == ItemType.Equipment)
        {
            amount_text.text = "";

         if(item.itemrank == ItemRank.Rare || item.itemrank == ItemRank.Unique || item.itemrank == ItemRank.Legend) // 아이템 랭크가 레어 이상이면, 파티클 활성화
            {
                Unique_Particle.gameObject.SetActive(true);
            }
        
        }

        else if (item.itemtype == ItemType.Consumables || item.itemtype == ItemType.Etc)
        {
            amount_text.text = item.amount.ToString();

            if (item.itemrank == ItemRank.Rare || item.itemrank == ItemRank.Unique || item.itemrank == ItemRank.Legend) // 아이템 랭크가 레어 이상이면, 파티클 활성화
            {
                Unique_Particle.gameObject.SetActive(true);
            }
           
        }
    
    }
       
    public void RemoveSlot()
    {
        item = null;
        itemicon.gameObject.SetActive(false);
        equiped_image.gameObject.SetActive(false); //체크표시 전체해제 (업데이트)
        Unique_Particle.gameObject.SetActive(false);
        amount_text.text = "";
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       

        if (isShopMode) //상점창이 열려있을경우 진행하는 로직
        {          
            if (item.Equip)
            {
                GameObject player = Managers.Game.GetPlayer();
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("장착중인 장비는 판매할 수 없습니다.");
                return;
            }

            GameObject GUI = GameObject.Find("GUI_Console").gameObject;
            Sell_Console sell = GUI.GetComponent<Sell_Console>();
            sell.Get_Slotnum(slotnum); //slot에 대한 정보를 sellconsole 스크립트에 넘겨줌
            Managers.Sound.Play("Coin");
            Sell_Panel.gameObject.SetActive(true);           
            return;

        }

        else if (isStorageMode) //창고모드인경우 (금고창이 열려있을 경우)
        {
            
            if(item == null)
            {
                return;
            }

            if (item.Equip) //장착중인 장비는 금고에 맡길 수 없음.
            {
                GameObject player = Managers.Game.GetPlayer();
                GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("장착중인 장비는 맡길 수 없습니다.");
                return;
            }
            
            
            if(item.itemtype == ItemType.Consumables)
            {
                if(item.amount > 1)
                {
                    GameObject GUI = GameObject.Find("GUI_Console").gameObject;
                    Storage_Input_Console storage = GUI.GetComponent<Storage_Input_Console>();
                    storage.Get_Slotnum(slotnum); //slot에 대한 정보를 storage_input_console 스크립트에 넘겨줌
                    Managers.Sound.Play("Coin");
                    Storage_Input_Console.SetActive(true); //갯수입력창 오픈 후 갯수입력창 스크립트에서 로직 처리
                    return;
                }

                else
                {
                    PlayerStorage.Instance.AddItem(this.item);
                    PlayerInventory.Instance.RemoveItem(this.slotnum);
                    return;
                }
            }
            // 장비인 경우

            PlayerStorage.Instance.AddItem(this.item);
            PlayerInventory.Instance.RemoveItem(this.slotnum);

            return;
        }

        else //상점모드가 아닌경우 장착/해제 담당
        {
            if(PlayerInventory.Instance.player_items.Count == 0)
            {
                return;
            }

            if(this.item.itemtype == ItemType.Equipment) //장비아이템인경우
            {
                GameObject go = GameObject.Find("NewInvenUI").gameObject;
                Slot_Equip_Drop equip_drop = go.GetComponent<Slot_Equip_Drop>();
                equip_drop.Get_Slotnum(slotnum); //slot에 대한 정보를 sellconsole 스크립트에 넘겨줌
                Equip_Drop_Panel.SetActive(true);
                Equip_Drop_Panel.transform.position = Input.mousePosition;
            }
            else if(this.item.itemtype == ItemType.Consumables)// 소모품인경우 
            {
                GameObject go = GameObject.Find("NewInvenUI").gameObject;
                Slot_Equip_Drop equip_drop = go.GetComponent<Slot_Equip_Drop>();
                equip_drop.Get_Slotnum(slotnum); //slot에 대한 정보를 sellconsole 스크립트에 넘겨줌
                Use_Drop_Panel.SetActive(true);
                Use_Drop_Panel.transform.position = Input.mousePosition;

            }

            else // 기타아이템
            {
                GameObject go = GameObject.Find("NewInvenUI").gameObject;
                Slot_Equip_Drop equip_drop = go.GetComponent<Slot_Equip_Drop>();
                equip_drop.Get_Slotnum(slotnum); //slot에 대한 정보를 sellconsole 스크립트에 넘겨줌
                Drop_Panel.SetActive(true);
                Drop_Panel.transform.position = Input.mousePosition;
            }

        }
      
    }

   
}

