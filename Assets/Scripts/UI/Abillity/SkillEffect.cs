using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffect : ScriptableObject //추상클래스
{
    public abstract bool ExecuteRole(SkillType skilltype);

}
