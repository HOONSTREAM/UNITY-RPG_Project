using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Button_Script : MonoBehaviour
{

    public GameObject Inventory;
    bool activeinventory = false;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Inventory = Managers.Resources.Load<GameObject>("PreFabs/UI/Scene/NewInvenUI");

        return;
    }

    public void Inventory_Open()
    {
        activeinventory = !activeinventory;
        Managers.Sound.Play("Inven_Open");
        Inventory.SetActive(activeinventory);
    }
}
