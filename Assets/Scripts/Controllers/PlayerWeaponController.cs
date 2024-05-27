using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public Item Equip_Weapon;

    public GameObject One_Hand_Wepaon_Long_Sword;
    public GameObject Two_Hand_Weapon_Great_Sword;




    private void Start()
    {
        One_Hand_Wepaon_Long_Sword = GameObject.Find("Sword1_R").gameObject;
        Two_Hand_Weapon_Great_Sword = GameObject.Find("Sword2_R").gameObject;
        One_Hand_Wepaon_Long_Sword.gameObject.SetActive(false);
        Two_Hand_Weapon_Great_Sword.gameObject.SetActive(false);

        Change_Weapon_Prefabs();
    }
    public Item Get_request_Change_Weapon_EquipType(Item item)
    {
        return Equip_Weapon = item;
    }

    public void Change_Weapon_Prefabs()
    {

        if (Equip_Weapon == null) return;

        switch (Equip_Weapon.itemname)
        {
            case "롱소드":
                One_Hand_Wepaon_Long_Sword.gameObject.SetActive(true);
                break;
            case "그레이트소드":
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
