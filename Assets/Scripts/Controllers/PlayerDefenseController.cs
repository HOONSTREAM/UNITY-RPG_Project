using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 플레이어의 방어구 장착 시, 플레이어 애니메이션 변경을 담당하는 클래스입니다.
/// </summary>
public class PlayerDefenseController : MonoBehaviour
{
    [SerializeField]
    public Item Equip_Defense;

    #region 머리장식

    #endregion

    #region 투구
    [SerializeField]
    public GameObject Wood_Helmet;

    #endregion

    #region 상의


    #endregion

    #region 하의

    #endregion

    #region 겉갑옷
    
    #endregion

    #region 방패

    #endregion

    #region 신발

    #endregion

    #region 탈것

    #endregion

    #region 망토

    #endregion

    public Item Get_request_Change_Defense_EquipType(Item item)
    {
        return Equip_Defense = item;
    }

    public void Change_Defense_Gear_Prefabs()
    {

        if (Equip_Defense == null) return;

        switch (Equip_Defense.equiptype)
        {

            case EquipType.Head_decoration:
                Change_Head_Decoration_PreFabs(Equip_Defense);
                break;
            case EquipType.Head:
                Change_Head_PreFabs(Equip_Defense);
                break;
            case EquipType.Chest:
                Change_Chest_PreFabs(Equip_Defense);
                break;
            case EquipType.pants:
                Change_Pants_PreFabs(Equip_Defense);
                break;
            case EquipType.shoes:
                Change_Shoes_PreFabs(Equip_Defense);
                break;
            case EquipType.outter_plate:
                Change_Outter_Plate_PreFabs(Equip_Defense);
                break;
            case EquipType.Shield:
                Change_Shield_PreFabs(Equip_Defense);
                break;
            case EquipType.vehicle:
                Change_Vehicle_PreFabs(Equip_Defense);
                break;
            case EquipType.cape:
                Change_Cape_PreFabs(Equip_Defense);
                break;

        }
    }

    private void Change_Head_Decoration_PreFabs(Item Equip_Defense)
    {

    }

    private void Change_Head_PreFabs(Item Equip_Defense)
    {
        if (Equip_Defense == null) return;

        switch (Equip_Defense.itemname)
        {
            case "우드 헬멧":
                Wood_Helmet.gameObject.SetActive(true);
                break;
          
        }
    }

    private void Change_Chest_PreFabs(Item Equip_Defense)
    {

    }

    private void Change_Pants_PreFabs(Item Equip_Defense)
    {

    }

    private void Change_Shoes_PreFabs(Item Equip_Defense)
    {

    }

    private void Change_Shield_PreFabs(Item Equip_Defense)
    {

    }

    private void Change_Outter_Plate_PreFabs(Item Equip_Defense)
    {

    }

    private void Change_Vehicle_PreFabs(Item Equip_Defense)
    {

    }

    private void Change_Cape_PreFabs(Item Equip_Defense)
    {

    }

    public void Change_No_Defense_Gear()
    {
        Wood_Helmet.gameObject.SetActive(false);       
    }
}
