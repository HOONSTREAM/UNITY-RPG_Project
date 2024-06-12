using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(�Ҹ�ǰ)/Health")]
public class Hp_Potion : ItemEffect
{
    private const int HP_RECOVERY_FIGURES_HP_POTION = 100;
    public override bool ExecuteRole(ItemType itemtype)
    {
       
        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();
        stat.Hp += HP_RECOVERY_FIGURES_HP_POTION;
        if(stat.Hp > stat.MAXHP)
        {
            stat.Hp = stat.MAXHP;
        }
        Managers.Sound.Play("�ܲ� �ܲ�", Define.Sound.Effect);
        Print_Info_Text.Instance.PrintUserText($"�Ҹ�ǰ�� ����Ͽ� HP {HP_RECOVERY_FIGURES_HP_POTION} �� ȸ���մϴ�. ");

        return true;
    }



}
