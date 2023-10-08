using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Equipment/Axe")]
public class Axe : ItemEffect
{
   
    public override bool ExecuteRole(ItemType itemtype)
    {
 
        GameObject go = GameObject.Find("UnityChan").gameObject;
        PlayerStat stat = go.GetComponent<PlayerStat>();
        

        return true;
    }

    public override int GetAtk()
    {
        return 75;
    }
}
