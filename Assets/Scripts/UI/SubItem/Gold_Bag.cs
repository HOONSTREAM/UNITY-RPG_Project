using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(�Ҹ�ǰ)/GoldBag")]
public class Gold_Bag : ItemEffect
{
    
    public override bool ExecuteRole(ItemType itemtype)
    {
      
        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();
        Managers.Sound.Play("CoinEffect");
        stat.Gold += 1000;
        stat.onchangestat.Invoke();
        Managers.Game.PrintUserText("1000��带 ȹ���Ͽ����ϴ�.");
       
        return true;
    }

}
