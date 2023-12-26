using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillEffect/Buff/Advanced_Attack")]
public class Advanced_Attack : SkillEffect
{
    public override bool ExecuteRole(SkillType skilltype) //TODO : 쿨타임 설정 
    {

        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();
        stat.PrintUserText("공격력을 일시적으로 강화합니다.");

        stat.Attack += 50;

        Managers.Sound.Play("spell", Define.Sound.Effect);

        GameObject effect = Managers.Resources.Instantiate("Skill_Effect/Lightning aura");
        effect.transform.parent = Managers.Game.GetPlayer().transform; // 부모설정
        effect.transform.position = Managers.Game.GetPlayer().gameObject.transform.position;

        Destroy(effect, 5.0f);
        
        stat.onchangestat.Invoke();
        
        return true;
    }
}
