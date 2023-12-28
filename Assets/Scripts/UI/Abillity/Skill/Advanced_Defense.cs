using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(menuName = "SkillEffect/Buff/Advanced_Defense")]
public class Advanced_Defense : SkillEffect
{
    public bool skillusing = false;
    public int skill_sustainment_time = 5;

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


        stat.PrintUserText("������ �Ͻ������� ��ȭ�մϴ�.");


        stat.buff_defense += 50; // TODO

        Managers.Sound.Play("spell", Define.Sound.Effect);

        GameObject effect = Managers.Resources.Instantiate("Skill_Effect/Buff");
        effect.transform.parent = Managers.Game.GetPlayer().transform; // �θ���
        effect.transform.position = Managers.Game.GetPlayer().gameObject.transform.position;

        Destroy(effect, skill_sustainment_time);

        stat.SetStat(stat.Level);
        stat.onchangestat.Invoke();

        DelayedAction();


        return true;
    }


    public async Task DelayedAction()
    {
        await Task.Delay(5000); // 10 second        
        Debuff_update();
    }


    private void Debuff_update()
    {

        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();

        stat.buff_defense -= 50;
        stat.SetStat(stat.Level);
        stat.onchangestat.Invoke();
        skillusing = false;

        return;
    }

}
