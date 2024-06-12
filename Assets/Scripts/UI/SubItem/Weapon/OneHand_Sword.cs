using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Equipment/Long_Sword")]

public class OneHand_Sword : ItemEffect
{

   
    public override bool ExecuteRole(ItemType itemtype)
    {       
        GameObject player = Managers.Game.GetPlayer();
        Print_Info_Text.Instance.PrintUserText("���⸦ �����߽��ϴ�.");
       
        return true;
    }

 
}
