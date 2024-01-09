using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(menuName = "SkillEffect/Buff/Advanced_Defense")]
public class Advanced_Defense : SkillEffect
{

    public bool skillusing = false;
    public int skill_sustainment_time = 25;
    public Buff_Slot[] buff_slots;
    public Transform buff_slot_holder;

    private void Init()
    {
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
    public override bool ExecuteRole(SkillType skilltype) //TODO : 쿨타임 설정 
    {
        Init();

        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();

        
        if (skillusing == true)
        {
            GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("쿨타임 입니다.");

            return false;

        }

        skillusing = true;


        GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("방어력을 일시적으로 강화합니다.");


        stat.buff_defense += 50; // TODO

        Managers.Sound.Play("spell", Define.Sound.Effect);

        GameObject effect = Managers.Resources.Instantiate("Skill_Effect/Unlock_FX_7");
        effect.transform.parent = Managers.Game.GetPlayer().transform; // 부모설정
        effect.transform.position = Managers.Game.GetPlayer().gameObject.transform.position + new Vector3(0.0f, 2.2f, 0.0f); ;

        Destroy(effect, skill_sustainment_time);

        stat.SetStat(stat.Level);
        stat.onchangestat.Invoke();



        _ =DelayedAction();


        return true;
    }


    public async Task DelayedAction()
    {
        await Task.Delay(25000); // 25 second        
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
