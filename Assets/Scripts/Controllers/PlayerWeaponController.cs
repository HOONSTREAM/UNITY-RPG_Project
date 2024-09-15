using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField]
    public Item Equip_Weapon;

    #region 한손검
    [SerializeField]
    public GameObject One_Hand_Wepaon_Long_Sword;
    [SerializeField]
    public GameObject Dev_Long_Sword;
    #endregion

    #region 양손검
    [SerializeField]
    public GameObject Two_Hand_Weapon_Great_Sword;
    
    [SerializeField]
    public GameObject Sliver_Two_Hand_Sword;
    #endregion

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
            case "데브롱소드":
                Dev_Long_Sword.gameObject.SetActive(true);
                break;
            case "실버투핸드소드":
                Sliver_Two_Hand_Sword.gameObject.SetActive(true);
                break;

        }
    }

    public void Change_No_Weapon()
    {
        One_Hand_Wepaon_Long_Sword.gameObject.SetActive(false);
        Two_Hand_Weapon_Great_Sword.gameObject.SetActive(false);
        Dev_Long_Sword.gameObject.SetActive(false);
        Sliver_Two_Hand_Sword.gameObject.SetActive(false);
    }

}
