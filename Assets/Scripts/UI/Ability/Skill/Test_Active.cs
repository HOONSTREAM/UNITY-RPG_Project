using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(menuName = "SkillEffect/Active/TEST_ACTIVE")]


public class Test_Active : SkillEffect
{

    
    public override bool ExecuteRole(SkillType skilltype)
    {

        if (PlayerEquipment.Instance.player_equip.TryGetValue(EquipType.Weapon, out Item value2) && value2.weapontype == WeaponType.One_Hand)
        {
            

            SkillDataBase.instance.SkillDB[4].CanUseSkill = false;
            GameObject effect = Managers.Resources.Instantiate("Skill_Effect/Active/Snow slash");
            GameObject casting_player = Managers.Resources.Instantiate("Skill_Effect/Active/Freeze circle");

            Vector3 skill_dir = Managers.Game.GetPlayer().transform.forward * 1.0f;
            Vector3 skill_effect_pos = Managers.Game.GetPlayer().transform.position + skill_dir + new Vector3(0, 1.0f, 0);

            effect.transform.position = skill_effect_pos;
            effect.transform.position = skill_effect_pos; effect.transform.rotation = Managers.Game.GetPlayer().transform.rotation;
            casting_player.transform.position = Managers.Game.GetPlayer().transform.position;

            
            Managers.Sound.Play("ICE_HIT/ice_blast_projectile_spell_04", Define.Sound.Effect);


            Destroy(effect, 2.0f);
            Destroy(casting_player, 0.8f);

            _ = Delayed_Skill_Action(); //discard ������: _�� DelayedAction�� ����� �����ϴ� �� ���Ǹ�,
                                        //�̴� �۾��� �ϷḦ ��ٸ� �ʿ䰡 ������ ��Ÿ��.




            return true;
        }

        else
        {
            GameObject.Find("GUI_User_Interface").gameObject.GetComponent<Print_Info_Text>().PrintUserText("�Ѽհ��� �����ؾ� ����� �� �ֽ��ϴ�.");

            return false;
        }

    }

    private async Task Delayed_Skill_Action()
    {
        await Task.Delay(SkillDataBase.instance.SkillDB[4].skill_cool_time * 500); // 1 second       
        Managers.Game.GetPlayer().GetComponent<PlayerController>().State = Define.State.Idle;
        SkillDataBase.instance.SkillDB[4].CanUseSkill = true;
    }

}

