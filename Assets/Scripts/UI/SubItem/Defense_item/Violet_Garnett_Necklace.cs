using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEffect/DEFENSE_Item/바이올렛 가넷 네클리스")]


public class Violet_Garnett_Necklace : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {
        GameObject player = Managers.Game.GetPlayer();
        Print_Info_Text.Instance.PrintUserText("목걸이를 장착했습니다.");

        return true;
    }
}
