using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(menuName = "SkillEffect/Buff/Advanced_DEFENSE")]
public class Advanced_DEFENSE : SkillEffect
{
    private const int ADVANCED_DEFENSE_BUFF_AMOUNT = 50;
    private const string SKILL_EFFECT_PATH = "Skill_Effect/Unlock_FX_7";
    private const string SKILL_SOUND_PATH = "spell";
    private int SKILL_MAGIC_POINT_CONSUMPTION;

    public bool skillusing = false;
    public int skill_sustainment_time;
    public Buff_Slot[] buff_slots;
    public Transform buff_slot_holder;

    private void Init()
    {
        SKILL_MAGIC_POINT_CONSUMPTION = SkillDataBase.instance.SkillDB[3].num_2;
        skill_sustainment_time = SkillDataBase.instance.SkillDB[3].skill_cool_time;
        buff_slot_holder = GameObject.Find("skill_coolTime_Content").gameObject.transform;
        buff_slots = buff_slot_holder.GetComponentsInChildren<Buff_Slot>();

        for (int i = 0; i < buff_slots.Length; i++) //스킬 사용중인지 검사
        {

            if (buff_slots[i].skill != null)
            {
                if (buff_slots[i].skill.skill_name == "난공불락")
                {
                    skillusing = true;

                    return;
                }

            }


        }

        skillusing = false;

        return;
    }
    public override bool ExecuteRole(SkillType skilltype) 
    {
        Init();

        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();

        
        if (skillusing == true)
        {
            Print_Info_Text.Instance.PrintUserText("쿨타임 입니다.");

            return false;

        }

        if (stat.Mp <= SKILL_MAGIC_POINT_CONSUMPTION)
        {
            Print_Info_Text.Instance.PrintUserText("마나가 부족합니다.");

            return false;
        }

        skillusing = true;


        Print_Info_Text.Instance.PrintUserText("방어력을 일시적으로 강화합니다.");


        stat.DEFENSE += ADVANCED_DEFENSE_BUFF_AMOUNT;
        stat.buff_DEFENSE += ADVANCED_DEFENSE_BUFF_AMOUNT;
        stat.Mp -= SKILL_MAGIC_POINT_CONSUMPTION;
        stat.onchangestat.Invoke();

        Managers.Sound.Play(SKILL_SOUND_PATH , Define.Sound.Effect);

        GameObject effect = Managers.Resources.Instantiate(SKILL_EFFECT_PATH);
        effect.transform.parent = Managers.Game.GetPlayer().transform; // 부모설정
        effect.transform.position = Managers.Game.GetPlayer().gameObject.transform.position + new Vector3(0.0f, 2.2f, 0.0f); ;

        Destroy(effect, skill_sustainment_time);

        _ =DelayedAction();


        return true;
    }


    private async Task DelayedAction()
    {
        await Task.Delay(skill_sustainment_time*1000); // 25 second        
        Debuff_update();
    }


    private void Debuff_update()
    {

        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();

        stat.DEFENSE -= ADVANCED_DEFENSE_BUFF_AMOUNT;
        stat.buff_DEFENSE -= ADVANCED_DEFENSE_BUFF_AMOUNT;
        stat.onchangestat.Invoke();
        skillusing = false;

        return;
    }

}
