using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffect/Consumable(¼Ò¸ðÇ°)/Health")]
public class Hp_Potion : ItemEffect
{
   
    public override bool ExecuteRole(ItemType itemtype)
    {
       
        GameObject go = GameObject.Find("UnityChan").gameObject;
        PlayerStat stat = go.GetComponent<PlayerStat>();
        stat.Hp += 100;
        Managers.Sound.Play("²Ü²© ²Ü²©", Define.Sound.Effect);
        GetAtk();
        return true;
    }

    public override int GetAtk()
    {
        return 0;
    }
}
