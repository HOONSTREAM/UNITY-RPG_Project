using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quick_Slot : MonoBehaviour
{
    public static Quick_Slot instance;
    public int slotnum;

    public Quick_Slot[] slots;
    public Item item;
    public Image itemicon;
    public List<Item> quick_slot; 

    void Awake()
    {
        slots = GetComponentsInChildren<Quick_Slot>();
        instance = this;
        quick_slot = new List<Item>();
    }

    public void registerQuickSlot(Item item)
    {
        quick_slot.Add(item);
        itemicon.sprite = item.itemImage;
    }

    //public void registerQuickSlot(Skill skill)
    //{

    //}

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1�� ����");           
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2�� ����");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("3�� ����");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("4�� ����");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("5�� ����");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("6�� ����");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Debug.Log("7�� ����");
        }

        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Debug.Log("8�� ����");
        }


        return;
    }
    void Update()
    {
        Execute();
    }
}
