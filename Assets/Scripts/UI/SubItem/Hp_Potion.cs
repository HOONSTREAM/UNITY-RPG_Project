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
        if(stat.Hp > stat.MaxHp)
        {
            stat.Hp = stat.MaxHp;
        }
        Managers.Sound.Play("�ܲ� �ܲ�", Define.Sound.Effect);
        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText($"�Ҹ�ǰ�� ����Ͽ� HP 100 �� ȸ���մϴ�. ");

        return true;
    }

   
}
