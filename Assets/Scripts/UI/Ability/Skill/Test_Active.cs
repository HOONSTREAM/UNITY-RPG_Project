using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(menuName = "SkillEffect/Active/TEST_ACTIVE")]


public class Test_Active : SkillEffect
{
    public override bool ExecuteRole(SkillType skilltype)
    {
        Managers.Sound.Play("hit22", Define.Sound.Effect);

        GameObject effect = Managers.Resources.Instantiate("Skill_Effect/Active/Snow slash");
        Vector3 skill_dir = Managers.Game.GetPlayer().transform.forward * 1.0f;
        Vector3 skill_effect_pos = Managers.Game.GetPlayer().transform.position + skill_dir + new Vector3(0, 1.0f, 0);

        effect.transform.position = skill_effect_pos;
        effect.transform.position = skill_effect_pos; effect.transform.rotation = Managers.Game.GetPlayer().transform.rotation;

        Destroy(effect, 2.0f);


        return true;

    }


}

