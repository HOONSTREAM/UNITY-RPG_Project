using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillEffect/Buff/Advanced_Attack")]
public class Advanced_Attack : SkillEffect
{
    public override bool ExecuteRole(SkillType skilltype) //TODO : ��Ÿ�� ���� 
    {

        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();
        stat.PrintUserText("���ݷ��� �Ͻ������� ��ȭ�մϴ�.");

        stat.Attack += 50;

        Managers.Sound.Play("spell", Define.Sound.Effect);

        GameObject effect = Managers.Resources.Instantiate("Skill_Effect/Lightning aura");
        effect.transform.parent = Managers.Game.GetPlayer().transform; // �θ���
        effect.transform.position = Managers.Game.GetPlayer().gameObject.transform.position;

        Destroy(effect, 5.0f);
        
        stat.onchangestat.Invoke();
        
        return true;
    }
}
