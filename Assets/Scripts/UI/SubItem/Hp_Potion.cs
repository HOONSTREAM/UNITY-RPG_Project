using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(소모품)/Health")]
public class Hp_Potion : ItemEffect
{
   
    public override bool ExecuteRole(ItemType itemtype)
    {
        
        Debug.Log("플레이어의 체력 100 을 회복합니다.");

        GameObject go = GameObject.Find("UnityChan").gameObject;
        PlayerStat stat = go.GetComponent<PlayerStat>();
        stat.Hp += 100;
        GetAtk();
        return true;
    }

    public override int GetAtk()
    {
        return 0;
    }
}
