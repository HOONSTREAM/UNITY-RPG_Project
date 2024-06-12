using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(¼Ò¸ðÇ°)/GoldBag")]
public class Gold_Bag : ItemEffect
{
    private const int GET_GOLD_AMOUNT = 1000;
    public override bool ExecuteRole(ItemType itemtype)
    {
      
        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();
        Managers.Sound.Play("CoinEffect");
        stat.Gold += GET_GOLD_AMOUNT;
        stat.onchangestat.Invoke();
        Print_Info_Text.Instance.PrintUserText($"{GET_GOLD_AMOUNT}°ñµå¸¦ È¹µæÇÏ¿´½À´Ï´Ù.");
       
        return true;
    }

}
