using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/DEFENSE_Item/���ǵ�")]

public class Wood_shield : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {
        GameObject player = Managers.Game.GetPlayer();


        // ���з��� �����ϱ�����, ��չ��⸦ �����ϰ� �ִ��� �˻��մϴ�.
        if (player.gameObject.GetComponent<PlayerEquipment>().player_equip.TryGetValue(EquipType.Weapon, out Item _attackitem) && _attackitem.weapontype == WeaponType.Two_Hand)
        {
            GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("��հ��� ���и� ������ �� �����ϴ�.");

            return false;
        }
   
        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("���� �����߽��ϴ�.");

        return true;
    }
}
