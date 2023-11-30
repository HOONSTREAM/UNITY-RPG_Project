using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Button_Script : MonoBehaviour
{

    public GameObject Inventory;
    bool activeinventory = false;

  
    public void Inventory_Open()
    {
        activeinventory = !activeinventory;
        Managers.Sound.Play("Inven_Open");
        Inventory.SetActive(activeinventory);
    }
}
