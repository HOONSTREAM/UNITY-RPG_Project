using Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillEffect/Buff/Advanced_Attack")]
public class Advanced_Attack : SkillEffect
{

     public bool skillusing = false;
     public int skill_sustainment_time = 30;

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

        Destroy(effect, skill_sustainment_time);

        stat.SetStat(stat.Level);
        stat.onchangestat.Invoke();

        DelayedAction();
       

        return true;
    }


    public async Task DelayedAction()
    {
        await Task.Delay(30000); // 30 second        
        Debuff_update();
    }


    private void Debuff_update()
    {

         GameObject player = Managers.Game.GetPlayer();
         PlayerStat stat = player.GetComponent<PlayerStat>();

         stat.buff_damage -= 50;
         stat.SetStat(stat.Level);
         stat.onchangestat.Invoke();
         skillusing = false;

        return;
    }

    
}
