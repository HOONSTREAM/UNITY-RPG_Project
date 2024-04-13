using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEffect/Consumable(¼Ò¸ðÇ°)/HP_Egg")]
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
        Managers.Sound.Play("²Ü²© ²Ü²©", Define.Sound.Effect);
        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText($"¼º¼ö¸¦ »ç¿ëÇÏ¿© HP {HP_RECOVERY_FIGURES_EGG} À» È¸º¹ÇÕ´Ï´Ù. ");

        return true;
    }

}
