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
    private const int RUDENCIAN_ROOKISS_NPC_ID = 1007;

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
        TalkData.Add(RUDENCIAN_HELKEN_NPC_ID, new string[] { "반갑다. 나는 기사 헬켄이라고 한다.", "나에게 볼 일이 있는가?" });
        TalkData.Add(RUDENCIAN_INN_NPC_ID, new string[] { " 체력을 회복 해 드리겠습니다." });
        TalkData.Add(RUDENCIAN_HOUSE_CHIEF_NPC_ID, new string[] { "만나서 반갑습니다. 모험가님.. 저는 촌장 월터라고 합니다." });
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[]{"강해지고 싶다면 나를 찾아와서 훈련을 받아보는건 어떤가?"});
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
        TalkData.Add(RUDENCIAN_HELKEN_NPC_ID, new string[] {"전해 듣던 모험가가 당신이군...나는 기사 헬켄이라고 한다.","너는 잘 모르겠지만, 루덴시안에 큰 위기가 찾아왔다.", 
                                                  "자세한 설명은 마을 촌장님이 급하게 찾고계시니, 얼른 찾아가서 설명을 들어보도록 해.","나는 지시를 받고 루덴시안 전체를 사주경계 하고 있으니, 너에게 따로 내려주실 지시사항이 있을 것이다." });

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
        TalkData.Add(RUDENCIAN_HOUSE_CHIEF_NPC_ID, new string[] {"헬켄에게 전해들었네. 자네로군... ",
                                                                 "......지금 루덴시안에 큰 위기가 찾아왔다. 나는 이 마을의 촌장이니 루덴시안의 안위를 가장 크게 신경써야 하는것이 맞긴하지만, 루덴시안을 비롯해 이 아르카디아 대륙이 큰 위험에 빠졌다.",
                                                                 "전설로만 전해지던 한 영웅이 봉인한 <샤르덴>이 깨어나서, 아르카디아 대륙 전체를 멸망시키겠다고 했다더군..",
                                                                 "나도 정말 놀랐던 이야기지만, 샤르덴이 깨어난 소문이 사실이라면, 우리는 당연히 준비태세를 갖추어야한다.. 각 마을이 비상체제에 돌입해서 경계중이지만, 가장 중요한 것은 샤르덴을 다시는 깨어나지 못하게 영원히 제거해야 한다.",
                                                                 "샤르덴이 봉인 되었을 당시 그 영웅이 사용했던 전설로만 전해져 내려오던 <용의 검>이 있다고 하더군. 샤르덴을 상대하려면 그 검이 반드시 있어야 할 것이다.",
                                                                 "샤르덴을 봉인하기 위해선, 그 <용의 검>을 찾는게 최 우선이어야 할 것이다..",
                                                                 "어려운 부탁임을 나는 알고있고, 자네도 곤란하게 생각할 지도 모르지만, 우리 루덴시안, 더 크게는 아르카디아 대륙을 위해 자네가 두번째로 전설적인 영웅으로 남아보는건 어떤가..",
                                                                 "촌장으로서 부탁하네. 루덴시안, 그리고 이 아르카디아 대륙을 꼭 자네가 구해주었으면 하네.",
                                                                 "수련관 루키스를 찾아가보게. 자네를 훈련시켜 줄 사람이 있네."});


        return;

    }

    public void Additional_Talk_Rudencian_training_officer_RooKiss()
    {

        for (int k = 0; k < Player_Quest.Instance.PlayerQuest.Count; k++)
        {
            if (Player_Quest.Instance.PlayerQuest[k].Quest_ID == 5 && Player_Quest.Instance.PlayerQuest[k].npc_meet == true) // 퀘스트가 이미 완료되었으면 리턴 
            {
                return;
            }

            Player_Quest.Instance.PlayerQuest[k].npc_meet = true;
        }

        TalkData.Remove(RUDENCIAN_ROOKISS_NPC_ID);
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[] {"반가워 모험가. 나는 수련관 루키스라고 한다. 촌장님께는 얘기를 전해들었다. 훈련을 받고싶다고 ? ", 
                                                             "강해지는 것은 당연히 쉽지 않지.. 하지만 충분히 자신을 단련시키다보면 매우 강해진 자신을 볼 수 있을 것이다.",
                                                             "여러 무기를 자신이 선택해서 단련시킬 수 있으니, 상황에 따라 유연하게 대처가 가능하려면 가능한 여러 종류의 무기를 숙달시키는게 좋다. 하지만 이것은 권장일 뿐, 선택은 너의 몫이다.",
                                                             "루덴시안 남쪽 필드로 가서, 펀치맨을 잡아보도록 해. 20마리를 잡고서, 나에게 돌아와서 보고해."});

    }


    public void Additional_Talk_Rudencian_training_officer_RooKiss_Quest2()
    {

        for (int k = 0; k < Player_Quest.Instance.PlayerQuest.Count; k++)
        {
            if (Player_Quest.Instance.PlayerQuest[k].Quest_ID == 7 && Player_Quest.Instance.PlayerQuest[k].npc_meet == true) // 퀘스트가 이미 완료되었으면 리턴 
            {
                return;
            }

            Player_Quest.Instance.PlayerQuest[k].npc_meet = true;
        }

        TalkData.Remove(RUDENCIAN_ROOKISS_NPC_ID);
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[] {"장비착용 퀘스트 8로 진행하기 위한 테스트 진행중 "});

    }



    public void Reset_TalkData()
    {
        GenerateData();

        return;

    }

    

   
}
