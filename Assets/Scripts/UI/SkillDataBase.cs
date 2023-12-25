using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataBase : MonoBehaviour
{
    public static SkillDataBase instance;

    public List<Skill> SkillDB = new List<Skill>();

   
    private void Awake()
    {
        instance = this;      
    }

}
