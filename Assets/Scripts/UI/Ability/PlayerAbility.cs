using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerEquipment;
using static UnityEditor.Progress;

public class PlayerAbility : MonoBehaviour
{


    #region �÷��̾� �����Ƽ ���� ����
    [System.Serializable]
    public class AbilityData
    {
        public List<Skill> skills;

        public AbilityData(List<Skill> player_skills)
        {
            this.skills = player_skills;
        }
    }
    #endregion

    public static PlayerAbility Instance;
    public List<Skill> PlayerSkill;
   

    public delegate void OnChangeSkill();
    public OnChangeSkill onChangeSkill;


    #region �޼��� �����Ƽ ���� ����
    public void Save_Ability_Info()
    {
        AbilityData data = new AbilityData(PlayerSkill);
        ES3.Save("Player_Ability", data);



        Debug.Log("Player_Ability saved using EasySave3");
    }

    public void Load_Ability_Info()
    {
        if (ES3.KeyExists("Player_Ability"))
        {
            AbilityData data = ES3.Load<AbilityData>("Player_Ability");
            PlayerSkill = data.skills;
            Debug.Log("Player_Ability loaded using EasySave3");
        }
        else
        {
            Debug.Log("No Player_Ability data found, creating a new one.");
        }
    }

    #endregion
    private void Awake()
    {
       
        Instance = this;
        PlayerSkill = new List<Skill>();
             
    }

    public bool AddSkill(Skill _skill) //��ųâ�� �������� ����
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
        if(PlayerSkill.Count > 0) //Null Crash ����
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
