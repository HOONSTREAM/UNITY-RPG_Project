using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEffect/Consumable(�Ҹ�ǰ)/HP_Egg")]
public class HP_Egg : ItemEffect
{
    private const int HP_RECOVERY_FIGURES_EGG = 200;
    public override bool ExecuteRole(ItemType itemtype)
    {

        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();
        stat.Hp += HP_RECOVERY_FIGURES_EGG;
        if (stat.Hp > stat.MAXHP)
        {
            stat.Hp = stat.MAXHP;
        }
        Managers.Sound.Play("�ܲ� �ܲ�", Define.Sound.Effect);
        Print_Info_Text.Instance.PrintUserText($"������ ����Ͽ� HP {HP_RECOVERY_FIGURES_EGG} �� ȸ���մϴ�. ");

        return true;
    }

}
