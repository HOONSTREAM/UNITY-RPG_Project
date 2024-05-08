using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    
    public enum QuestType
    {
        Main,
        Sub,
        Repetition,

    }


    /*유니티 에디터의 인스펙터에는 사용자가 정의한 클래스 또는 구조체의 정보가 인스펙터에 노출되지 않지만,
      System에서 제공하는 Serializable 키워드를 지정하여 인스펙터에 노출시킬 수 있음.*/

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
        public bool is_complete; //퀘스트 완료여부
        public int monster_counter;
        public bool npc_meet;
        public bool is_achievement_of_conditions; // 퀘스트 완료조건 달성여부

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


        /*클래스는 기본적으로 참조 형태이기때문에 리스트에 다이렉트로 리스트정보를 추가하게 되면 참조주소가 같은 곳을 가리키게 된다. 클론함수로 해결 */
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
