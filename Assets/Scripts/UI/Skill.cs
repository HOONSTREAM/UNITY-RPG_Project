using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    /*�� ������ �����Ƽ �� ���� ���� ��Ʋ�� Skill�� Ī�Ѵ�.*/
    public enum SkillType
    {
        Active,
        Buff,
        Passive,
        Abillity
    }

  
    /*����Ƽ �������� �ν����Ϳ��� ����ڰ� ������ Ŭ���� �Ǵ� ����ü�� ������ �ν����Ϳ� ������� ������,
      System���� �����ϴ� Serializable Ű���带 �����Ͽ� �ν����Ϳ� �����ų �� ����.*/

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
        public double abillity = 0.00;
  
        public List<SkillEffect> efts;


        public virtual bool Skill_Use()
        {
            bool isUsed = false;

            foreach (SkillEffect effect in efts)
            {
                isUsed = effect.ExecuteRole(skilltype);
            }
            return isUsed;
        }

 
        /*Ŭ������ �⺻������ ���� �����̱⶧���� ����Ʈ�� ���̷�Ʈ�� ����Ʈ������ �߰��ϰ� �Ǹ� �����ּҰ� ���� ���� ����Ű�� �ȴ�. Ŭ���Լ��� �ذ� */
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
           skill.abillity = this.abillity;
            return skill;
        }
    }
