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

    public override bool ExecuteRole(SkillType skilltype) //TODO : 쿨타임 설정 
    {
        

        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();

        if (skillusing == true)
        {
            stat.PrintUserText("쿨타임 입니다.");

            return false;

        }

        skillusing = true;


        stat.PrintUserText("공격력을 일시적으로 강화합니다.");


        stat.buff_damage += 50;

        Managers.Sound.Play("spell", Define.Sound.Effect);

        GameObject effect = Managers.Resources.Instantiate("Skill_Effect/Lightning aura");
        effect.transform.parent = Managers.Game.GetPlayer().transform; // 부모설정
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
