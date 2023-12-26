using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillEffect/Buff/Advanced_Attack")]
public class Advanced_Attack : SkillEffect
{
    
    bool skillusing = false;
    public override bool ExecuteRole(SkillType skilltype) //TODO : ��Ÿ�� ���� 
    {
        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();

        if (skillusing == true)
        {
            stat.PrintUserText("��Ÿ�� �Դϴ�.");

            return false;

        }

        skillusing = true;


        stat.PrintUserText("���ݷ��� �Ͻ������� ��ȭ�մϴ�.");


        stat.buff_damage += 50;

        Managers.Sound.Play("spell", Define.Sound.Effect);

        GameObject effect = Managers.Resources.Instantiate("Skill_Effect/Lightning aura");
        effect.transform.parent = Managers.Game.GetPlayer().transform; // �θ���
        effect.transform.position = Managers.Game.GetPlayer().gameObject.transform.position;

        Destroy(effect, 5.0f);

        stat.SetStat(stat.Level);
        stat.onchangestat.Invoke();

        Debuff_update();

        return true;
    }

    void Debuff_update()
    {

        float _nowtime = 0;
        float _skilltime = 10.0f;

        while (true)
        {
            _nowtime += Time.deltaTime;
            if(_nowtime > _skilltime)
            {
                GameObject player = Managers.Game.GetPlayer();
                PlayerStat stat = player.GetComponent<PlayerStat>();

                stat.buff_damage -= 50;
                stat.SetStat(stat.Level);
                stat.onchangestat.Invoke();
                skillusing = false;

                break;
            }
        }

        return;
    }

    
}
