using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Equipment/Axe")]
public class Axe : ItemEffect
{
   
    public override bool ExecuteRole(ItemType itemtype)
    {
 
        GameObject player = Managers.Game.GetPlayer();
        Managers.Game.PrintUserText("무기를 장착했습니다.");

        return true;
    }

 
}
