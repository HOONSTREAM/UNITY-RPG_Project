using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerJewelInven : MonoBehaviour
{
    


    #region 테스트코드 인벤토리 저장
    [System.Serializable]
    public class JEWEL_UI_Data
    {
        public List<Item> jewel_items;

        public JEWEL_UI_Data(List<Item> items)
        {
            this.jewel_items = items;
        }
    }
    #endregion

    public static PlayerJewelInven Instance;
    public GameObject _player_Jewel_Inven_Content; // 플레이어 인벤토리의 Content (모든슬롯)
    public List<Item> player_jewel_items;
    public JEWEL_Slot slot;

    public delegate void OnChangeJEWEL();
    public OnChangeJEWEL onChangejewel; // 델리게이트 인스턴스 생성



    private void Awake()
    {
        Instance = this;
        player_jewel_items = new List<Item>();
        
        slot = gameObject.GetComponent<JEWEL_Slot>();

    }


    #region 테스트 메서드 인벤토리 저장
    public void Save_JEWEL_Inventory()
    {
        JEWEL_UI_Data data = new JEWEL_UI_Data(player_jewel_items);
        ES3.Save("player_jewel_inventory", data);

       

        Debug.Log("Inventory saved using EasySave3");
    }

    public void Load_JEWEL_Inventory()
    {
        if (ES3.KeyExists("player_jewel_inventory"))
        {
            JEWEL_UI_Data data = ES3.Load<JEWEL_UI_Data>("player_jewel_inventory");
            player_jewel_items = data.jewel_items;
            Debug.Log("Inventory loaded using EasySave3");
        }
        else
        {
            Debug.Log("No inventory data found, creating a new one.");
        }
    }

    #endregion

    public bool Add_JEWEL_Item(Item _item)
    {
        player_jewel_items.Add(_item); 

        if (onChangejewel != null)
        {
            onChangejewel.Invoke();
            return true;
        }

        return false;
    }
    
   
}
