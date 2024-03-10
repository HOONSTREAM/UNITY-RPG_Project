using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTimeManager : MonoBehaviour 
{ 


    //private bool can_attack = false;
    #region Monster_Cool_Time

    private int Slime_Cool_Time = 2000;
    private int Punch_man_Cool_Time = 6000;

    #endregion

    public List<GameObject> monsters = new List<GameObject>();

    void Start()
    {
        monsters.Add(Managers.Resources.Load<GameObject>("PreFabs/Slime"));
        monsters.Add(Managers.Resources.Load<GameObject>("PreFabs/Punch_man"));
    }

    void Update()
    {
        
    }

    public void Monster_cool_time()
    {

    }

 

}
