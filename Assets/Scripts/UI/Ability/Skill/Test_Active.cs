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
        effect.transform.position = Managers.Game.GetPlayer().transform.position + new Vector3(0, 1.0f, 0);
        Managers.Game.GetPlayer().transform.LookAt(Managers.Game.GetPlayer().transform);
        Destroy(effect, 2.0f);


        return true;

    }


}

