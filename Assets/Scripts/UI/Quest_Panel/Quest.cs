using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    
    public enum QuestType
    {
        Main,
        Sub,
        Repetition,

    }


    /*����Ƽ �������� �ν����Ϳ��� ����ڰ� ������ Ŭ���� �Ǵ� ����ü�� ������ �ν����Ϳ� ������� ������,
      System���� �����ϴ� Serializable Ű���带 �����Ͽ� �ν����Ϳ� �����ų �� ����.*/

    [System.Serializable]
    public class Quest
    {
        public int Quest_ID;
        public QuestType questtype;
        public string quest_name;
        public string reward_1;
        public string reward_2;
        public int num_1;
        public int num_2;
        public string Description;
        public string summing_up_Description;
        public Sprite quest_image; //Main or Sub
        public bool is_complete; //����Ʈ �ϷῩ��
        public int monster_counter;
        public bool npc_meet;
        public bool is_achievement_of_conditions; // ����Ʈ �Ϸ����� �޼�����

        public List<Quest_Effect> efts;


        public virtual bool Quest_Clear()
        {
            bool isUsed = false;

            foreach (Quest_Effect effect in efts)
            {
                isUsed = effect.ExecuteRole(questtype);
            }
            return isUsed;
        }


        /*Ŭ������ �⺻������ ���� �����̱⶧���� ����Ʈ�� ���̷�Ʈ�� ����Ʈ������ �߰��ϰ� �Ǹ� �����ּҰ� ���� ���� ����Ű�� �ȴ�. Ŭ���Լ��� �ذ� */
        public Quest Clone()
        {
            Quest quest = new Quest();
            quest.Quest_ID = this.Quest_ID;
            quest.questtype = this.questtype;
            quest.quest_name = this.quest_name;
            quest.reward_1 = this.reward_1;
            quest.reward_2 = this.reward_2;
            quest.num_1 = this.num_1;
            quest.num_2 = this.num_2;
            quest.Description = this.Description;
            quest.summing_up_Description = this.summing_up_Description;
            quest.efts = this.efts;
            quest.quest_image = this.quest_image;
            quest.is_complete = this.is_complete;
            quest.monster_counter = this.monster_counter;
            quest.npc_meet = this.npc_meet;
            quest.is_achievement_of_conditions = this.is_achievement_of_conditions;


           return quest;  
        
        }
    }
