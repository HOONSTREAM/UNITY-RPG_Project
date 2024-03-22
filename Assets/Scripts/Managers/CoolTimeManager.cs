using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CoolTimeManager : MonoBehaviour 
{ 

    #region Monster_Cool_Time

    private int Slime_Cool_Time = 3000; // ½½¶óÀÓ ÄðÅ¸ÀÓ
    private int Punch_man_Cool_Time = 6000; // ÆÝÄ¡¸Ç ÄðÅ¸ÀÓ 
    private bool monster_name_exist = false;

    [SerializeField]
    private List<GameObject> monster;
    #endregion

    private void Start()
    {

        monster.Add(Managers.Resources.Load<GameObject>("PreFabs/Slime"));
        monster.Add(Managers.Resources.Load<GameObject>("PreFabs/Punch_man"));

    }
    public int monster_attack_cooltime (string name)
    {
        for(int i = 0; i< monster.Count; i++)
        {
            if (monster[i].name == name)
            {
                monster_name_exist = true;
            }

            if(monster_name_exist == true)
            {
                break;
            }
        }
        
        if(monster_name_exist == false)
        {
            return 0;
        }

        switch (name)
        {
            case "Slime":

                return Slime_Cool_Time;

                
            case "Punch_man":

                return Punch_man_Cool_Time;

            default:
                return 0;
        }


    }
   

 

}
