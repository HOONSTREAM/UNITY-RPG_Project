using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillEffect/Buff/Spritual_Stone_Water")]

public class Spritual_Stone_Water : SkillEffect
{
    private const int MAGIC_POINT_RECOVERY_AMOUNT = 50;
    private const string SKILL_EFFECT_PATH = "Skill_Effect/Spritual_Stone_Water";
    private const string SKILL_SOUND_PATH = "Player_TelePort_2";

    public bool skillusing = false;   
    public Buff_Slot[] buff_slots;
    public Transform buff_slot_holder;

    private void Init()
    {
        
        buff_slot_holder = GameObject.Find("skill_coolTime_Content").gameObject.transform;
        buff_slots = buff_slot_holder.GetComponentsInChildren<Buff_Slot>();

        for (int i = 0; i < buff_slots.Length; i++) //��ų ��������� �˻�
        {

            if (buff_slots[i].skill != null)
            {
                if (buff_slots[i].skill.skill_name == "���Ȱ������ɼ�")
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
            Print_Info_Text.Instance.PrintUserText("���Ȱ��� �ູ ��Ÿ�� �Դϴ�.");

            return false;

        }

        skillusing = true;


        Print_Info_Text.Instance.PrintUserText($"������ {MAGIC_POINT_RECOVERY_AMOUNT} ȸ���մϴ�. ");


        stat.Mp += MAGIC_POINT_RECOVERY_AMOUNT;
        if(stat.Mp >= stat.MaxMp)
        {
            stat.Mp = stat.MaxMp;
        }

        stat.onchangestat.Invoke();

        Managers.Sound.Play(SKILL_SOUND_PATH, Define.Sound.Effect);

        GameObject effect = Managers.Resources.Instantiate(SKILL_EFFECT_PATH);


        effect.transform.parent = Managers.Game.GetPlayer().transform; // �θ���
        effect.transform.position = Managers.Game.GetPlayer().gameObject.transform.position + new Vector3(0.0f, 1.5f, 0.0f);

        Destroy(effect, SkillDataBase.instance.SkillDB[5].skill_cool_time);

        _ = DelayedAction();


        return true;
    }


    private async Task DelayedAction()
    {
        await Task.Delay(SkillDataBase.instance.SkillDB[5].skill_cool_time * 1000);
        Debuff_update();
    }

    private void Debuff_update()
    {

        GameObject player = Managers.Game.GetPlayer();
        PlayerStat stat = player.GetComponent<PlayerStat>();

    
        stat.onchangestat.Invoke();

        skillusing = false;

        return;
    }

}
