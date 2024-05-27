using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorage : MonoBehaviour
{


    #region 장비장착정보 저장
    [System.Serializable]
    public class StorageData
    {
        public List<Item> storage_items;

        public StorageData(List<Item> items)
        {
            this.storage_items = items;
        }
    }
    #endregion

    #region 메서드 장착정보 저장
    public void Save_Storage()
    {
        StorageData data = new StorageData(storage_item);
        ES3.Save("Player_Storage", data);
        ES3.Save("Storage_Gold", Storage_Gold);


        Debug.Log("Player_Storage saved using EasySave3");
    }

    public void Load_Storage()
    {
        if (ES3.KeyExists("Player_Storage"))
        {
            StorageData data = ES3.Load<StorageData>("Player_Storage");
            storage_item = data.storage_items;
            int gold_data = ES3.Load<int>("Storage_Gold");
            GameObject.Find("Storage CANVAS").gameObject.GetComponent<Gold_Deposit_Console>().Load_Storage_Gold(gold_data);
            Debug.Log("Player_Storage loaded using EasySave3");
        }
        else
        {
            Debug.Log("No inventory data found, creating a new one.");
        }
    }

    #endregion

    public static PlayerStorage Instance;

    PlayerStat stat;
    public List<Item> storage_item;
    public Slot slot;
    private int _storage_gold;
    public int Storage_Gold { get { return _storage_gold; } set { _storage_gold = value; } }


    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

   

    private void Awake()
    {
        Instance = this;
        storage_item = new List<Item>();
        stat = GetComponent<PlayerStat>();
        slot = GetComponent<Slot>();
    }

    public bool AddItem(Item _item)
    {

        if (storage_item.Count < 20)  //아이템 추가할때 슬롯보다 작을때만 아이템 추가
        {

            if (_item.IsStackable())
            {
                bool ItemAlreadyInInventory = false;
                foreach (Item InventoryItem in storage_item)
                {
                    if (InventoryItem.ItemID == _item.ItemID)
                    {
                        InventoryItem.amount++;
                        ItemAlreadyInInventory = true;

                        if (onChangeItem != null)
                        {
                            onChangeItem.Invoke(); //소모품스택 갯수 업데이트                          
                        }
                    }

                }

                if (!ItemAlreadyInInventory)
                {
                    storage_item.Add(_item);

                    if (onChangeItem != null)
                    {
                        onChangeItem.Invoke();
                        return true;
                    }
                }

            }

            else
            {
                storage_item.Add(_item);

                if (onChangeItem != null)
                {
                    onChangeItem.Invoke();
                    return true;
                }

            }

            return true;
        }

        Managers.Sound.Play("Coin");
        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("창고가 가득찼습니다.");
        return false;
    }

    public void RemoveItem(int index) // 갯수 파악하여 스택 사용 후 1개 남으면 아이템 삭제
    {
        if (storage_item[index].itemtype == ItemType.Consumables)
        {
            if (storage_item[index].amount > 1)
            {
                storage_item[index].amount -= 1; // 사용 시 1개 감소 
                onChangeItem.Invoke();
            }

            else if (storage_item[index].amount == 1)
            {
                storage_item.RemoveAt(index);
                onChangeItem.Invoke();
            }

        }
        else
        {
            storage_item.RemoveAt(index);
            onChangeItem.Invoke();
        }


    }
}
