using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffect : ScriptableObject //�߻�Ŭ����
{
    public float Active_Skill_cooldown_Time; // ��ų ��Ÿ��

    public abstract bool ExecuteRole(SkillType skilltype);

}
