using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �÷��̾��� �� ���� ��, �÷��̾� �ִϸ��̼� ������ ����ϴ� Ŭ�����Դϴ�.
/// </summary>
public class PlayerDefenseController : MonoBehaviour
{
    [SerializeField]
    public Item Equip_Defense;

    #region �Ӹ����

    #endregion

    #region ����
    [SerializeField]
    public GameObject Wood_Helmet;

    #endregion

    #region ����


    #endregion

    #region ����

    #endregion

    #region �Ѱ���
    
    #endregion

    #region ����

    #endregion

    #region �Ź�

    #endregion

    #region Ż��

    #endregion

    #region ����

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
            case "��� ���":
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
