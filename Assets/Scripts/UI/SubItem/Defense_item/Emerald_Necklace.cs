using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/DEFENSE_Item/���޶��� ��Ŭ����")]


public class Emerald_Necklace : ItemEffect
{
    public override bool ExecuteRole(ItemType itemtype)
    {
        GameObject player = Managers.Game.GetPlayer();
        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("����̸� �����߽��ϴ�.");

        return true;
    }
}
