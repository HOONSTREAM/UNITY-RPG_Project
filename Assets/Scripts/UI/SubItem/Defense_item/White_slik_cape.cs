using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/DEFENSE_Item/ȭ��Ʈ��ũ������")]
public class White_slik_cape : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {
        GameObject player = Managers.Game.GetPlayer();
        Print_Info_Text.Instance.PrintUserText("���並 �����߽��ϴ�.");

        return true;
    }
}
