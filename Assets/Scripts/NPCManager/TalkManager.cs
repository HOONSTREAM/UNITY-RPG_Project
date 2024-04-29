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
        TalkData.Add(RUDENCIAN_HELKEN_NPC_ID, new string[] { "�ȳ�, ���� ��� �����̾�.", "������ �� ���� �־�?" });
        TalkData.Add(RUDENCIAN_INN_NPC_ID, new string[] { " ü���� ȸ�� �� �帮�ڽ��ϴ�." });
        TalkData.Add(RUDENCIAN_HOUSE_CHIEF_NPC_ID, new string[] { "������ �ݰ���. ���� ���� ���Ͷ�� �մϴ�." });
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
        TalkData.Add(RUDENCIAN_HELKEN_NPC_ID, new string[] {"ó�� ���� ���谡�� �Ա�. �ݰ���. ���� ��� �ɳ��̶�� ��. �絧�þƿ��� ������ �ϳ� �����������´ٴ���. ���� ������ ã�ư���.", 
                                                  "���� ������� ã�ư����� ���� �ܼ��� ���� �� �������� ����." });

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
        TalkData.Add(RUDENCIAN_HOUSE_CHIEF_NPC_ID, new string[] {"ó�� ���� ���谡�� �Ա�. �ݰ���. ���� ���� �����̶�� ��. �絧�þƿ��� ������ �ϳ� �����������´ٴ���. ���� ������ ã�ư���.",
                                                  "���� ������� ã�ư����� ���� �ܼ��� ���� �� �������� ����." });


        return;

    }


    public void Reset_TalkData()
    {
        GenerateData();

        return;

    }

   
}
