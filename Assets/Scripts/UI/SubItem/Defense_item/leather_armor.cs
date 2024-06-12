using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/DEFENSE_Item/°¡Á×°©¿Ê")]
public class leather_armor : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {
        GameObject player = Managers.Game.GetPlayer();
        Print_Info_Text.Instance.PrintUserText("¹æ¾î±¸¸¦ ÀåÂøÇß½À´Ï´Ù.");

        return true;
    }
}
