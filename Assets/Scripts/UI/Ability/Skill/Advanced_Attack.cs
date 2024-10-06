using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillEffect/Buff/Advanced_Attack")]
public class Advanced_Attack : SkillEffect
{
    private const int ADVANCED_ATTACK_BUFF_AMOUNT = 50;
    private const string SKILL_EFFECT_PATH = "Skill_Effect/Unlock_FX_2";
    private const string SKILL_SOUND_PATH = "spell";
    private int SKILL_MAGIC_POINT_CONSUMPTION;

    public bool skillusing = false;
    public int skill_duration_time; 
    public Buff_Slot[] buff_slots;
    public Transform buff_slot_holder;

    private void Init()
    {
        SKILL_MAGIC_POINT_CONSUMPTION = SkillDataBase.instance.SkillDB[2].num_2;
        skill_duration_time = SkillDataBase.instance.SkillDB[2].skill_duration_time;
        buff_slot_holder = GameObject.Find("skill_coolTime_Content").gameObject.transform;
        buff_slots = buff_slot_holder.GetComponentsInChildren<Buff_Slot>();

        

        for (int i = 0; i < buff_slots.Length; i++) //��ų ��������� �˻�
        {
            
            if (buff_slots[i].skill != null)
            {
                if (buff_slots[i].skill.skill_name == "���꽺�����")
                {
                    skillusing = true;

                    return;
                }
                
            }

          
        }

        return;
    }
    public override bool ExecuteRole(SkillType skilltype) 
    {

        Init();

        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();

        // ��ų�� ��������� �˻��մϴ�.
        if (skillusing == true)
        {
            Print_Info_Text.Instance.PrintUserText("��Ÿ�� �Դϴ�.");

            return false;

        }

        //������ ������� �˻��մϴ�.
        if(stat.Mp <= SKILL_MAGIC_POINT_CONSUMPTION)
        {
            Print_Info_Text.Instance.PrintUserText("������ �����մϴ�.");

            return false;
        }


        skillusing = true;


        Print_Info_Text.Instance.PrintUserText("���ݷ��� �Ͻ������� ��ȭ�մϴ�.");


        stat.ATTACK += ADVANCED_ATTACK_BUFF_AMOUNT;
        stat.buff_damage += ADVANCED_ATTACK_BUFF_AMOUNT;
        stat.Mp -= SKILL_MAGIC_POINT_CONSUMPTION;
        stat.onchangestat.Invoke();

        Managers.Sound.Play(SKILL_SOUND_PATH, Define.Sound.Effect);

        GameObject effect = Managers.Resources.Instantiate(SKILL_EFFECT_PATH);

        
        effect.transform.parent = Managers.Game.GetPlayer().transform; // �θ���
        effect.transform.position = Managers.Game.GetPlayer().gameObject.transform.position + new Vector3 (0.0f,2.2f,0.0f);

        Destroy(effect, skill_duration_time);
      
        

        _ = DelayedAction();
       

        return true;
    }


    private async Task DelayedAction()
    {
        await Task.Delay(skill_duration_time * 1000); // 10 second        
        Debuff_update();

        await Task.Delay(((SkillDataBase.instance.SkillDB[2].skill_cool_time)-(skill_duration_time)) * 1000); // 30-10 = 20second

        skillusing = false;

    }


    private void Debuff_update()
    {

         GameObject player = Managers.Game.GetPlayer();
         PlayerStat stat = player.GetComponent<PlayerStat>();
        
         stat.ATTACK -= ADVANCED_ATTACK_BUFF_AMOUNT;
        stat.buff_damage -= ADVANCED_ATTACK_BUFF_AMOUNT;
        stat.onchangestat.Invoke();
       

        return;
    }

    
}
