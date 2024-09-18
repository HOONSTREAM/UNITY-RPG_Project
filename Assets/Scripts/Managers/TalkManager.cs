using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TalkManager : MonoBehaviour
{

    private const int PLAYER_OBJ_DATA_ID = 9999;
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
        TalkData.Add(PLAYER_OBJ_DATA_ID, new string[] {"어느때나 다름없는 하루가 시작되었다..","어렸을 적에 부모님을 여의어 부모의 존재는 거의 기억나지 않는다. 루키스 선생님 말로는 내 부모님이 아르카디아 대륙을 멸망시키려 했던 극악무도한 흑룡 <샤르덴>을 봉인하는 과정에서 희생 당하셨고, 나는 루키스 선생님께 거두어 키워졌다고 한다.",
        "그리고, 우리 부모님이 <용의 검> 이라는 검의 첫 주인이었다고 얘기해주셨다. ",
        "<용의 검>이 대체 무엇이길래... ",
        "어제 루키스 수련관님이 시켰던 슬라임이나 잡으러 가봐야 하나.. 귀찮네.. 무슨 내가 세계를 구할 영웅이 될것도 아니고.. 그냥 평범하게 살고 싶은데.. 쳇.."});
        TalkData.Add(RUDENCIAN_HELKEN_NPC_ID, new string[] { "반갑다. 나는 기사 헬켄이라고 한다.", "나에게 볼 일이 있는가?" });
        TalkData.Add(RUDENCIAN_INN_NPC_ID, new string[] { " 체력을 회복 해 드리겠습니다. 체력회복이 필요하면, 언제든 저를 찾아오십시오." });
        TalkData.Add(RUDENCIAN_HOUSE_CHIEF_NPC_ID, new string[] { "만나서 반갑네. 나는 이 루덴시안 마을의 촌장, 월터라고 하네." });
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[]{"어이, 지크, 머뭇거릴 시간이 있으면, 가서 슬라임이라도 한마리 더 잡아서 강해져라!"});
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
                                                                 "... 꺼내고싶지 않은 이야기지만, 너의 기억 속에는 없을 수 있지만 너의 부모님이 목숨을 바쳐 봉인한 <샤르덴>이 다시 깨어나서, 아르카디아 대륙 전체를 반드시 멸망시키겠다고 했다더군..",
                                                                 "나도 정말 놀랐던 이야기지만, 샤르덴이 깨어난 소문이 사실이라면, 우리는 당연히 더욱 더 강화된 준비태세를 갖추어야한다.. 각 마을이 비상체제에 돌입해서 경계중이지만, 가장 중요한 것은 샤르덴을 다시는 깨어나지 못하게 영원히 제거해야 한다.",
                                                                 "샤르덴이 봉인 되었을 당시 그 영웅이 사용했던 전설로만 전해져 내려오던 <용의 검>이 있다고 하더군. 샤르덴을 상대하려면 그 검이 반드시 있어야 할 것이다.",
                                                                 "샤르덴을 봉인하기 위해선, 너의 부모님이 처음이자 마지막의 주인이었던, <용의 검>을 찾는게 최 우선이어야 할 것이다..",
                                                                 "어려운 부탁임을 나는 알고있고, 자네도 곤란하게 생각할 지도 모르지만, 우리 루덴시안, 더 크게는 아르카디아 대륙을 위해 자네가 두번째로 전설적인 영웅으로 남아보는건 어떤가..",
                                                                 "촌장으로서 부탁하네. 루덴시안, 그리고 이 아르카디아 대륙을 꼭 자네가 구해주었으면 하네.",
                                                                 "수련관 루키스를 다시 찾아가보게. 잘 일러둘터이니 이제, 정식적인 훈련절차를 밟아보도록 하게나."});


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
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[] {"촌장님께는 얘기를 전해들었다. 정식적인 훈련을 받고싶다고 ? ", 
                                                             "강해지는 것은 당연히 쉽지 않지.. 하지만 충분히 자신을 단련시키다보면 매우 강해진 자신을 볼 수 있을 것이다.",
                                                             "여러 무기를 자신이 선택해서 단련시킬 수 있으니, 상황에 따라 유연하게 대처가 가능하려면 가능한 여러 종류의 무기를 숙달시키는게 좋다. 하지만 이것은 권장일 뿐, 선택은 너의 몫이다.",
                                                             "루덴시안 남쪽 필드로 가서, 펀치맨을 잡아보도록 해. 20마리를 잡고서, 나에게 돌아와서 보고해."});

    }

    public void Additional_Talk_Rudencian_training_officer_RooKiss_Quest2()
    {

        for (int k = 0; k < Player_Quest.Instance.PlayerQuest.Count; k++)
        {
            if (Player_Quest.Instance.PlayerQuest[k].Quest_ID == 6 && Player_Quest.Instance.PlayerQuest[k].npc_meet == true) // 퀘스트가 이미 완료되었으면 리턴 
            {
                return;
            }

            Player_Quest.Instance.PlayerQuest[k].npc_meet = true;
        }

        TalkData.Remove(RUDENCIAN_ROOKISS_NPC_ID);
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[] {"펀치맨을 제거하는 것 까지 정말 잘해주었군! , 처음보다 실력이 조금씩 늘어가는 것이 눈에 보이는군!",
         "이제 본격적인 이야기를 조금씩 해도 될것 같군... <용의 검>이 봉인되어 있는 곳의 또다른 차원을 여는 방법을 너에게 알려주마.",
         "<용의 검>은 너가 모험을 하면서, 이 대륙에 흩어져있는 <4개의 정령석>을 모아서 차원을 열게 되면, 그 차원에 검이 봉인되어있다고한다.",
         "정령석이 정확히 어디에 있는지 정확히 알 순없지만, 너의 부모님도 성공적으로 검을 찾아냈 듯, 너에게 신의 축복이 따른다면, 정령석을 어렵지 않게 구할 수 있을거라 믿는다.",
         "그러고 보니, 네 놈은 달랑 낡은 검 하나만 들고 무엇을 하려고 하는거냐 ! , 어서 상점으로 가서, [머리, 겉옷, 신발] 종류를 구매해서, 장비부터 갖춰보도록 해라."});

    }
    public void Additional_Talk_Rudencian_training_officer_RooKiss_Quest3()
    {

        for (int k = 0; k < Player_Quest.Instance.PlayerQuest.Count; k++)
        {
            if (Player_Quest.Instance.PlayerQuest[k].Quest_ID == 10 && Player_Quest.Instance.PlayerQuest[k].npc_meet == true) // 퀘스트가 이미 완료되었으면 리턴 
            {
                return;
            }

            Player_Quest.Instance.PlayerQuest[k].npc_meet = true;
        }

        TalkData.Remove(RUDENCIAN_ROOKISS_NPC_ID);
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[] {"지크, 그래도 처음보다는 확실히 강해진 느낌이 많이 드는구나.", 
         "하지만 앞으로 펼쳐질 너의 모험을 위해서는 끝없이 너의 기술을 연마하고, 또 연마해서 정밀도를 끝없이 높여야한다.",
         "나는 너가 이 아르카디아 대륙을 구해낼 영웅이 될 것이라 굳게 믿고있다. 너의 부모님이 그러했듯이 ...",
         "너에게 마지막 임무를 주려고한다. 이제 더 큰 곳으로 나아가게 될 초석이 될 임무일 것이다. <루덴시안 깊은 곳>에 가서, <킹슬라임>을 제거해라.",
         "부족하다고 느낄지도 모르는 너의 능력을 믿어라. 특히, C키를 누르면 활용할 수 있는 텔레포트 기술을 활용해 완벽한 타이밍에 피하면, 피해를 입지 않을 수 있다.",
         "그리고, 너에게 특별히 기술을 하나 알려주도록 하마. <스노우 슬래쉬>. 어빌리티를 수련하는 데엔 도움이 되지않으나, 데미지가 강력해 강한 적을 상대할 때 적절할 것이다.",
         "승리할 것이라고 믿는다. 이제 더 큰 모험의 세계로 나아가라! 지크! 어서 출발해라!"});

    }

    public void Additional_Talk_Rudencian_training_officer_RooKiss_Quest_Last()
    {

        for (int k = 0; k < Player_Quest.Instance.PlayerQuest.Count; k++)
        {
            if (Player_Quest.Instance.PlayerQuest[k].Quest_ID == 12 && Player_Quest.Instance.PlayerQuest[k].npc_meet == true) // 퀘스트가 이미 완료되었으면 리턴 
            {
                return;
            }

            Player_Quest.Instance.PlayerQuest[k].npc_meet = true;
        }

        TalkData.Remove(RUDENCIAN_ROOKISS_NPC_ID);
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[] {"지크, 킹슬라임을 처치한건가? 짧은 시간에 정말 많이 성장했구나 ! ",
         "이것이 정령석이군.. 나도 정령석이라는 것은 엄청 많이 얘기를 들었지만, 실제로 정령석을 내 눈으로 보는 것은 처음이다.",
         "다시 한번 얘기하지만, 나는 너가 이 아르카디아 대륙을 구해낼 영웅이 될 것이라 굳게 믿고있다. 너의 부모님이 그러했듯이 ...",
         "이제, 더이상 나는 너에게 가르칠 것이 없다. <에필레니아>에 가서, 수련관 디스커버를 찾아가라. 편지를 미리 보내놓았으니, 바로 가면 너를 알아볼 것이다.",
         "고생많았다. 너는 반드시 샤르덴을 처치 할 영웅이 되리라 믿는다. 기회가 된다면, 술이라도 한잔 사도록 하마. 허허.. \n 그리고, 에필레니아로 바로 이동할 수 있는 깃털을 줄테니, 사용해서 바로 이동하거라. 조심히 가거라!",
         });

    }
    public void Reset_TalkData()
    {
        GenerateData();

        return;

    }

    

   
}
