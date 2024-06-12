using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEffect/Equipment/(레어)그레이트소드")]
public class Great_Sword_Rare : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {
        GameObject player = Managers.Game.GetPlayer();


        if (player.gameObject.GetComponent<PlayerEquipment>().player_equip.TryGetValue(EquipType.Shield, out Item _shielditem))
        {
            Print_Info_Text.Instance.PrintUserText("방패를 장착한 채 양손검을 장착할 수 없습니다.");

            return false;
        }

        
        Print_Info_Text.Instance.PrintUserText("무기를 장착했습니다.");

        return true;
    }

}
