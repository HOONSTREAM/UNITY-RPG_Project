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
        public Sprite quest_image; //Main or Sub
       

        public List<Quest_Effect> efts;


        public virtual bool Quest_Use()
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
            quest.efts = this.efts;
            quest.quest_image = this.quest_image;


          return quest;  
        
        }
    }
