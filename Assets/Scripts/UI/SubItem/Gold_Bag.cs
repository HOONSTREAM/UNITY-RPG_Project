using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(¼Ò¸ðÇ°)/GoldBag")]
public class Gold_Bag : ItemEffect
{
    
    public override bool ExecuteRole(ItemType itemtype)
    {
        
        Debug.Log("°ñµå¸¦ È¹µæÇÕ´Ï´Ù.");

        GameObject go = GameObject.Find("UnityChan").gameObject;
        PlayerStat stat = go.GetComponent<PlayerStat>();
        Managers.Sound.Play("CoinEffect");
        stat.Gold += 1000;
        stat.PrintUserText("1000°ñµå¸¦ È¹µæÇÏ¿´½À´Ï´Ù.");
        GetAtk();
        return true;
    }

    public override int GetAtk()
    {
        return 0;
    }
}
