using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TalkManager : MonoBehaviour
{

    private const int RUDENCIAN_HELKEN_NPC_ID = 1003;
    private const int RUDENCIAN_INN_NPC_ID = 1004;
    private const int RUDENCIAN_HOUSE_CHIEF_NPC_ID = 1006;

    public interface Additional_Talking
    {
        void Additional_Talk();

    }

    Dictionary<int, string[]> TalkData;
   
    void Awake()
    {
        TalkData = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData()
    {
        TalkData.Clear();       
        TalkData.Add(RUDENCIAN_HELKEN_NPC_ID, new string[] { "안녕, 나는 기사 헬켄이야.", "나에게 볼 일이 있어?" });
        TalkData.Add(RUDENCIAN_INN_NPC_ID, new string[] { " 체력을 회복 해 드리겠습니다." });
        TalkData.Add(RUDENCIAN_HOUSE_CHIEF_NPC_ID, new string[] { "만나서 반갑소. 나는 촌장 월터라고 합니다." });
    }

    public string GetTalk(int id, int talkIndex)
    {

        if (talkIndex == TalkData[id].Length)
        {           
            return null;
        } 
            
        else
        {          
            return TalkData[id][talkIndex];
        }
        

    }

    public void Additional_Talk_Helken()
    {
        for (int k = 0; k < Player_Quest.Instance.PlayerQuest.Count; k++)
        {
            if (Player_Quest.Instance.PlayerQuest[k].Quest_ID == 3 && Player_Quest.Instance.PlayerQuest[k].npc_meet == true) // 퀘스트 완료조건 
            {
                return;
            }

            Player_Quest.Instance.PlayerQuest[k].npc_meet = true;
        }

        TalkData.Remove(RUDENCIAN_HELKEN_NPC_ID);
        TalkData.Add(RUDENCIAN_HELKEN_NPC_ID, new string[] {"처음 보는 모험가가 왔군. 반가워. 나는 기사 케넨이라고 해. 루덴시아에는 전설이 하나 전해져내려온다더군. 마을 촌장을 찾아가봐.", 
                                                  "마을 촌장님을 찾아가보면 무언가 단서를 얻을 수 있을지도 몰라." });

        return;

    }

    public void Additional_Talk_Rudencian_House_Chief()
    {
        for (int k = 0; k < Player_Quest.Instance.PlayerQuest.Count; k++)
        {
            if (Player_Quest.Instance.PlayerQuest[k].Quest_ID == 4 && Player_Quest.Instance.PlayerQuest[k].npc_meet == true) // 퀘스트 완료조건 
            {
                return;
            }

            Player_Quest.Instance.PlayerQuest[k].npc_meet = true;
        }

        TalkData.Remove(RUDENCIAN_HOUSE_CHIEF_NPC_ID);
        TalkData.Add(RUDENCIAN_HOUSE_CHIEF_NPC_ID, new string[] {"처음 보는 모험가가 왔군. 반가워. 나는 촌장 월터이라고 해. 루덴시아에는 전설이 하나 전해져내려온다더군. 마을 촌장을 찾아가봐.",
                                                  "마을 촌장님을 찾아가보면 무언가 단서를 얻을 수 있을지도 몰라." });


        return;

    }


    public void Reset_TalkData()
    {
        GenerateData();

        return;

    }

   
}
