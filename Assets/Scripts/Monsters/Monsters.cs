using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters 
{
    private const int SLIME_COOL_TIME = 3000; // 슬라임 쿨타임
    private const int PUNCH_MAN_COOL_TIME = 6000; // 펀치맨 쿨타임 
    private const int TURTLE_SLIME_COOL_TIME = 6000; // 터틀슬라임 쿨타임

    [SerializeField]
    private List<GameObject> monster;

    void Start()
    {
        monster.Add(Managers.Resources.Load<GameObject>("PreFabs/Slime"));
        monster.Add(Managers.Resources.Load<GameObject>("PreFabs/Punch_man"));
        monster.Add(Managers.Resources.Load<GameObject>("PreFabs/Turtle_Slime"));
    }

    public int Slime_Cool_Time()
    {
       return SLIME_COOL_TIME;
    }

    public int Punch_man_Cool_Time()
    {
        return PUNCH_MAN_COOL_TIME;
    }

    public int Turtle_Slime_Cool_Time()
    {
        return TURTLE_SLIME_COOL_TIME;
    }


}
