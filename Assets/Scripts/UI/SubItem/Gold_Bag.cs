using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(�Ҹ�ǰ)/GoldBag")]
public class Gold_Bag : ItemEffect
{
    
    public override bool ExecuteRole(ItemType itemtype)
    {
        
        Debug.Log("��带 ȹ���մϴ�.");

        GameObject go = GameObject.Find("UnityChan").gameObject;
        PlayerStat stat = go.GetComponent<PlayerStat>();
        Managers.Sound.Play("CoinEffect");
        stat.Gold += 1000;
        stat.PrintUserText("1000��带 ȹ���Ͽ����ϴ�.");
        GetAtk();
        return true;
    }

    public override int GetAtk()
    {
        return 0;
    }
}
