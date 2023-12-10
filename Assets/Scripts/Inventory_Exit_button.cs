using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Exit_button : MonoBehaviour
{
    public GameObject InvenUI;

    public void Exit()
    {
        if(InvenUI!=null && InvenUI.activeSelf)
        {
            InvenUI.SetActive(false);
            Managers.Sound.Play("Inven_Open");
        }

        return;
    }
}
