using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(소모품)/Health")]
public class Hp_Potion : ItemEffect
{
   
    public override bool ExecuteRole(ItemType itemtype)
    {
       
        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();
        stat.Hp += 100;
        Managers.Sound.Play("꿀꺽 꿀꺽", Define.Sound.Effect);
        Managers.Game.PrintUserText($"소모품을 사용하여 HP 100 을 회복합니다. ");

        return true;
    }

   
}
