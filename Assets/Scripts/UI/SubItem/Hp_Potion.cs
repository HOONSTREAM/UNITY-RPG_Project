using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(�Ҹ�ǰ)/Health")]
public class Hp_Potion : ItemEffect
{
   
    public override bool ExecuteRole(ItemType itemtype)
    {
       
        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();
        stat.Hp += 100;
        Managers.Sound.Play("�ܲ� �ܲ�", Define.Sound.Effect);
        Managers.Game.PrintUserText($"�Ҹ�ǰ�� ����Ͽ� HP 100 �� ȸ���մϴ�. ");

        return true;
    }

   
}
