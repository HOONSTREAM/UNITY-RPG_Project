using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/DEFENSE_Item/우드실드")]

public class Wood_shield : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {
        GameObject player = Managers.Game.GetPlayer();


        // 방패류를 장착하기전에, 양손무기를 장착하고 있는지 검사합니다.
        if (player.gameObject.GetComponent<PlayerEquipment>().player_equip.TryGetValue(EquipType.Weapon, out Item _attackitem) && _attackitem.weapontype == WeaponType.Two_Hand)
        {
            Print_Info_Text.Instance.PrintUserText("양손검은 방패를 장착할 수 없습니다.");

            return false;
        }
   
        Print_Info_Text.Instance.PrintUserText("방어구를 장착했습니다.");

        return true;
    }
}
