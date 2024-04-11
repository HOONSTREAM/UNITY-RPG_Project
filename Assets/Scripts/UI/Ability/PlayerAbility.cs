using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerAbility : MonoBehaviour
{
    public static PlayerAbility Instance;

    private PlayerStat stat;
    public List<Skill> PlayerSkill;
   

    public delegate void OnChangeSkill();
    public OnChangeSkill onChangeSkill;



    private void Awake()
    {
       
        Instance = this;
        PlayerSkill = new List<Skill>();
        stat = GetComponent<PlayerStat>();
        
    }

    public bool AddSkill(Skill _skill) //스킬창은 갯수제한 없음
    {
        PlayerSkill.Add(_skill);

        if (onChangeSkill != null)
        {
            onChangeSkill.Invoke();
           
        }

        return true;
    }

    public void RemoveSkill(int index)
    {
        if(PlayerSkill.Count > 0) //Null Crash 방지
        {
            PlayerSkill.RemoveAt(index);
            onChangeSkill.Invoke();
            
        }

        else
        {
            return;
        }

        return;
    }


   

}
