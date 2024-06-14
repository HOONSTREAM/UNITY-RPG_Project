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
        TalkData.Add(RUDENCIAN_HELKEN_NPC_ID, new string[] { "�ݰ���. ���� ��� �����̶�� �Ѵ�.", "������ �� ���� �ִ°�?" });
        TalkData.Add(RUDENCIAN_INN_NPC_ID, new string[] { " ü���� ȸ�� �� �帮�ڽ��ϴ�." });
        TalkData.Add(RUDENCIAN_HOUSE_CHIEF_NPC_ID, new string[] { "������ �ݰ����ϴ�. ���谡��.. ���� ���� ���Ͷ�� �մϴ�." });
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[]{"�������� �ʹٸ� ���� ã�ƿͼ� �Ʒ��� �޾ƺ��°� ���?"});
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
            if (Player_Quest.Instance.PlayerQuest[k].Quest_ID == 3 && Player_Quest.Instance.PlayerQuest[k].npc_meet == true) // ����Ʈ �Ϸ����� 
            {
                return;
            }

            Player_Quest.Instance.PlayerQuest[k].npc_meet = true;
        }

        TalkData.Remove(RUDENCIAN_HELKEN_NPC_ID);
        TalkData.Add(RUDENCIAN_HELKEN_NPC_ID, new string[] {"���� ��� ���谡�� ����̱�...���� ��� �����̶�� �Ѵ�.","�ʴ� �� �𸣰�����, �絧�þȿ� ū ���Ⱑ ã�ƿԴ�.", 
                                                  "�ڼ��� ������ ���� ������� ���ϰ� ã���ô�, �� ã�ư��� ������ ������ ��.","���� ���ø� �ް� �絧�þ� ��ü�� ���ְ�� �ϰ� ������, �ʿ��� ���� �����ֽ� ���û����� ���� ���̴�." });

        return;

    }

    public void Additional_Talk_Rudencian_House_Chief()
    {
       
        for (int k = 0; k < Player_Quest.Instance.PlayerQuest.Count; k++)
        {
            if (Player_Quest.Instance.PlayerQuest[k].Quest_ID == 4 && Player_Quest.Instance.PlayerQuest[k].npc_meet == true) // ����Ʈ �Ϸ����� 
            {
                return;
            }

            Player_Quest.Instance.PlayerQuest[k].npc_meet = true;
        }

        TalkData.Remove(RUDENCIAN_HOUSE_CHIEF_NPC_ID);
        TalkData.Add(RUDENCIAN_HOUSE_CHIEF_NPC_ID, new string[] {"���˿��� ���ص����. �ڳ׷α�... ",
                                                                 "......���� �絧�þȿ� ū ���Ⱑ ã�ƿԴ�. ���� �� ������ �����̴� �絧�þ��� ������ ���� ũ�� �Ű��� �ϴ°��� �±�������, �絧�þ��� ����� �� �Ƹ�ī��� ����� ū ���迡 ������.",
                                                                 "�����θ� �������� �� ������ ������ <������>�� �����, �Ƹ�ī��� ��� ��ü�� �����Ű�ڴٰ� �ߴٴ���..",
                                                                 "���� ���� ����� �̾߱�����, �������� ��� �ҹ��� ����̶��, �츮�� �翬�� �غ��¼��� ���߾���Ѵ�.. �� ������ ���ü���� �����ؼ� �����������, ���� �߿��� ���� �������� �ٽô� ����� ���ϰ� ������ �����ؾ� �Ѵ�.",
                                                                 "�������� ���� �Ǿ��� ��� �� ������ ����ߴ� �����θ� ������ �������� <���� ��>�� �ִٰ� �ϴ���. �������� ����Ϸ��� �� ���� �ݵ�� �־�� �� ���̴�.",
                                                                 "�������� �����ϱ� ���ؼ�, �� <���� ��>�� ã�°� �� �켱�̾�� �� ���̴�..",
                                                                 "����� ��Ź���� ���� �˰��ְ�, �ڳ׵� ����ϰ� ������ ���� ������, �츮 �絧�þ�, �� ũ�Դ� �Ƹ�ī��� ����� ���� �ڳװ� �ι�°�� �������� �������� ���ƺ��°� ���..",
                                                                 "�������μ� ��Ź�ϳ�. �絧�þ�, �׸��� �� �Ƹ�ī��� ����� �� �ڳװ� �����־����� �ϳ�.",
                                                                 "���ð� ��Ű���� ã�ư�����. �ڳ׸� �Ʒý��� �� ����� �ֳ�."});


        return;

    }

    public void Additional_Talk_Rudencian_training_officer_RooKiss()
    {

        for (int k = 0; k < Player_Quest.Instance.PlayerQuest.Count; k++)
        {
            if (Player_Quest.Instance.PlayerQuest[k].Quest_ID == 5 && Player_Quest.Instance.PlayerQuest[k].npc_meet == true) // ����Ʈ�� �̹� �Ϸ�Ǿ����� ���� 
            {
                return;
            }

            Player_Quest.Instance.PlayerQuest[k].npc_meet = true;
        }

        TalkData.Remove(RUDENCIAN_ROOKISS_NPC_ID);
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[] {"�ݰ��� ���谡. ���� ���ð� ��Ű����� �Ѵ�. ����Բ��� ��⸦ ���ص����. �Ʒ��� �ް�ʹٰ� ? ", 
                                                             "�������� ���� �翬�� ���� ����.. ������ ����� �ڽ��� �ܷý�Ű�ٺ��� �ſ� ������ �ڽ��� �� �� ���� ���̴�.",
                                                             "���� ���⸦ �ڽ��� �����ؼ� �ܷý�ų �� ������, ��Ȳ�� ���� �����ϰ� ��ó�� �����Ϸ��� ������ ���� ������ ���⸦ ���޽�Ű�°� ����. ������ �̰��� ������ ��, ������ ���� ���̴�.",
                                                             "�絧�þ� ���� �ʵ�� ����, ��ġ���� ��ƺ����� ��. 20������ ���, ������ ���ƿͼ� ������."});

    }


    public void Additional_Talk_Rudencian_training_officer_RooKiss_Quest2()
    {

        for (int k = 0; k < Player_Quest.Instance.PlayerQuest.Count; k++)
        {
            if (Player_Quest.Instance.PlayerQuest[k].Quest_ID == 7 && Player_Quest.Instance.PlayerQuest[k].npc_meet == true) // ����Ʈ�� �̹� �Ϸ�Ǿ����� ���� 
            {
                return;
            }

            Player_Quest.Instance.PlayerQuest[k].npc_meet = true;
        }

        TalkData.Remove(RUDENCIAN_ROOKISS_NPC_ID);
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[] {"������� ����Ʈ 8�� �����ϱ� ���� �׽�Ʈ ������ "});

    }



    public void Reset_TalkData()
    {
        GenerateData();

        return;

    }

    

   
}
