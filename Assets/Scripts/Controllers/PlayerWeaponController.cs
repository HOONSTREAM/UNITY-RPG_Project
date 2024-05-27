using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public Item Equip_Weapon;

    public GameObject One_Hand_Wepaon_Long_Sword;
    public GameObject Two_Hand_Weapon_Great_Sword;


    public Item Get_request_Change_Weapon_EquipType(Item item)
    {
        return Equip_Weapon = item;
    }

    public void Change_Weapon_Prefabs()
    {

        if (Equip_Weapon == null) return;

        switch (Equip_Weapon.itemname)
        {
            case "�ռҵ�":
                One_Hand_Wepaon_Long_Sword.gameObject.SetActive(true);
                break;
            case "�׷���Ʈ�ҵ�":
                Two_Hand_Weapon_Great_Sword.gameObject.SetActive(true);
                break;


        }
    }

    public void Change_No_Weapon()
    {
        One_Hand_Wepaon_Long_Sword.gameObject.SetActive(false);
        Two_Hand_Weapon_Great_Sword.gameObject.SetActive(false);
    }

}
