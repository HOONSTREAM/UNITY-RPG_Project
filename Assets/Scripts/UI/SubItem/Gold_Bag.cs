using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(¼Ò¸ðÇ°)/GoldBag")]
public class Gold_Bag : ItemEffect
{
    
    public override bool ExecuteRole(ItemType itemtype)
    {
      
        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();
        Managers.Sound.Play("CoinEffect");
        stat.Gold += 1000;
        stat.onchangestat.Invoke();
        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("1000°ñµå¸¦ È¹µæÇÏ¿´½À´Ï´Ù.");
       
        return true;
    }

}
