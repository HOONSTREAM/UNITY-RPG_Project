using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(소모품)/Health")]
public class Hp_Potion : ItemEffect
{
    private const int HP_RECOVERY_FIGURES_HP_POTION = 100;
    public override bool ExecuteRole(ItemType itemtype)
    {
       
        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();
        stat.Hp += HP_RECOVERY_FIGURES_HP_POTION;
        if(stat.Hp > stat.MaxHp)
        {
            stat.Hp = stat.MaxHp;
        }
        Managers.Sound.Play("꿀꺽 꿀꺽", Define.Sound.Effect);
        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText($"소모품을 사용하여 HP {HP_RECOVERY_FIGURES_HP_POTION} 을 회복합니다. ");

        return true;
    }



}
