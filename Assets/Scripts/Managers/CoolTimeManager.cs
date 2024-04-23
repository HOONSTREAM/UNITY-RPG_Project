using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CoolTimeManager : MonoBehaviour 
{ 
    public int monster_attack_cooltime (string name)
    {
        Monsters monsters = new Monsters ();
        
        switch (name)
        {
            case "Slime":

                return monsters.Slime_Cool_Time();

            case "Punch_man":

                return monsters.Punch_man_Cool_Time();

            case "Turtle_Slime":
                return monsters.Turtle_Slime_Cool_Time();

            default:
                return 0;
        }

    }
   

 

}
