using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/DEFENSE_Item/������")]
public class Wood_Helmet : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {
        GameObject player = Managers.Game.GetPlayer();
        Print_Info_Text.Instance.PrintUserText("���� �����߽��ϴ�.");

        return true;
    }
}
