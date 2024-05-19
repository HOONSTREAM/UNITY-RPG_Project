using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffect : ScriptableObject //추상클래스
{
    public float Active_Skill_cooldown_Time; // 스킬 쿨타임

    public abstract bool ExecuteRole(SkillType skilltype);

}
