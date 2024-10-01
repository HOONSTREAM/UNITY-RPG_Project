using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /*각 무기의 어빌리티 및 스펠 전부 통틀어 Skill로 칭한다.*/
    public enum SkillType
    {
        Active,
        Buff,
        Passive,
        Ability
    }

  
    /*유니티 에디터의 인스펙터에는 사용자가 정의한 클래스 또는 구조체의 정보가 인스펙터에 노출되지 않지만,
      System에서 제공하는 Serializable 키워드를 지정하여 인스펙터에 노출시킬 수 있음.*/

    [System.Serializable]
    public class Skill
    {
        public int Skill_ID;
        public SkillType skilltype;      
        public string skill_name;
        public string stat_1;
        public string stat_2;
        public int num_1;
        public int num_2;
        public string Description;
        public Sprite skill_image;
        public double Ability = 0.00;
        public int skill_cool_time = 0;
        public int skill_duration_time = 0;
        public int Ability_Grade = 0;
  
        public List<SkillEffect> efts;
        public bool CanUseSkill = true;

        public virtual bool Skill_Use()
        {
            bool isUsed = false;

            foreach (SkillEffect effect in efts)
            {
            if (CanUseSkill) // 스킬을 사용할 수 있는지 검사
            {
                isUsed = effect.ExecuteRole(skilltype);
            }
               
            }
            return isUsed;

        }


   


    /*클래스는 기본적으로 참조 형태이기때문에 리스트에 다이렉트로 리스트정보를 추가하게 되면 참조주소가 같은 곳을 가리키게 된다. 클론함수로 해결 */
    public Skill Clone()
        {
            Skill skill = new Skill();
           skill.Skill_ID = this.Skill_ID;
           skill.skilltype = this.skilltype;          
           skill.skill_name = this.skill_name;
           skill.stat_1 = this.stat_1;
           skill.stat_2 = this.stat_2;
           skill.num_1 = this.num_1;
           skill.num_2 = this.num_2;
           skill.Description = this.Description;
           skill.skill_image = this.skill_image;          
           skill.efts = this.efts;
           skill.Ability = this.Ability;
           skill.skill_cool_time = this.skill_cool_time;
           skill.skill_duration_time = this.skill_duration_time;
           skill.CanUseSkill = this.CanUseSkill;

            return skill;
        }

}
