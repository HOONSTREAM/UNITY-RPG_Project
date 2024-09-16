using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadController : MonoBehaviour,PlayerDefenseController.Defense_Gear_Controller_Interface
{
    [SerializeField]
    public Item Equip_Defense;

    [SerializeField]
    public GameObject Wood_Helmet;

    public Item Get_request_Change_Defense_EquipType(Item item)
    {
        return Equip_Defense = item;
    }

    public void Change_Defense_Gear_Prefabs()
    {
        Change_Head_PreFabs(Equip_Defense);
    }

    private void Change_Head_PreFabs(Item Equip_Defense)
    {
        if (Equip_Defense == null) return;

        switch (Equip_Defense.itemname)
        {
            case "¿ìµå Çï¸ä":
                Wood_Helmet.gameObject.SetActive(true);
                break;

        }
    }

    public void Change_No_Defense_Gear()
    {
        Wood_Helmet.gameObject.SetActive(false);
    }

}
