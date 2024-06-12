using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEffect/Equipment/(����)�׷���Ʈ�ҵ�")]
public class Great_Sword_Rare : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {
        GameObject player = Managers.Game.GetPlayer();


        if (player.gameObject.GetComponent<PlayerEquipment>().player_equip.TryGetValue(EquipType.Shield, out Item _shielditem))
        {
            Print_Info_Text.Instance.PrintUserText("���и� ������ ä ��հ��� ������ �� �����ϴ�.");

            return false;
        }

        
        Print_Info_Text.Instance.PrintUserText("���⸦ �����߽��ϴ�.");

        return true;
    }

}
