using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters 
{
    private const int SLIME_COOL_TIME = 3000; // ½½¶óÀÓ ÄðÅ¸ÀÓ
    private const int PUNCH_MAN_COOL_TIME = 6000; // ÆÝÄ¡¸Ç ÄðÅ¸ÀÓ 

    [SerializeField]
    private List<GameObject> monster;

    void Start()
    {
        monster.Add(Managers.Resources.Load<GameObject>("PreFabs/Slime"));
        monster.Add(Managers.Resources.Load<GameObject>("PreFabs/Punch_man"));
    }

    public int Slime_Cool_Time()
    {
       return SLIME_COOL_TIME;
    }

    public int Punch_man_Cool_Time()
    {
        return PUNCH_MAN_COOL_TIME;
    }


}
