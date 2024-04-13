using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(�Ҹ�ǰ)/HP_holyWater")]

public class HP_holyWater : ItemEffect
{
    private const int HP_RECOVERY_FIGURES_HOLY_WATER = 500;
    public override bool ExecuteRole(ItemType itemtype)
    {

        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();
        stat.Hp += HP_RECOVERY_FIGURES_HOLY_WATER;
        if (stat.Hp > stat.MAXHP)
        {
            stat.Hp = stat.MAXHP;
        }
        Managers.Sound.Play("�ܲ� �ܲ�", Define.Sound.Effect);
        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText($"������ ����Ͽ� HP {HP_RECOVERY_FIGURES_HOLY_WATER} �� ȸ���մϴ�. ");

        return true;
    }
}

