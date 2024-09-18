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
        TalkData.Add(PLAYER_OBJ_DATA_ID, new string[] {"������� �ٸ����� �Ϸ簡 ���۵Ǿ���..","����� ���� �θ���� ���Ǿ� �θ��� ����� ���� ��ﳪ�� �ʴ´�. ��Ű�� ������ ���δ� �� �θ���� �Ƹ�ī��� ����� �����Ű�� �ߴ� �ؾǹ����� ��� <������>�� �����ϴ� �������� ��� ���ϼ̰�, ���� ��Ű�� �����Բ� �ŵξ� Ű�����ٰ� �Ѵ�.",
        "�׸���, �츮 �θ���� <���� ��> �̶�� ���� ù �����̾��ٰ� ������̴ּ�. ",
        "<���� ��>�� ��ü �����̱淡... ",
        "���� ��Ű�� ���ð����� ���״� �������̳� ������ ������ �ϳ�.. ������.. ���� ���� ���踦 ���� ������ �ɰ͵� �ƴϰ�.. �׳� ����ϰ� ��� ������.. ��.."});
        TalkData.Add(RUDENCIAN_HELKEN_NPC_ID, new string[] { "�ݰ���. ���� ��� �����̶�� �Ѵ�.", "������ �� ���� �ִ°�?" });
        TalkData.Add(RUDENCIAN_INN_NPC_ID, new string[] { " ü���� ȸ�� �� �帮�ڽ��ϴ�. ü��ȸ���� �ʿ��ϸ�, ������ ���� ã�ƿ��ʽÿ�." });
        TalkData.Add(RUDENCIAN_HOUSE_CHIEF_NPC_ID, new string[] { "������ �ݰ���. ���� �� �絧�þ� ������ ����, ���Ͷ�� �ϳ�." });
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[]{"����, ��ũ, �ӹ��Ÿ� �ð��� ������, ���� �������̶� �Ѹ��� �� ��Ƽ� ��������!"});
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
                                                                 "... ��������� ���� �̾߱�����, ���� ��� �ӿ��� ���� �� ������ ���� �θ���� ����� ���� ������ <������>�� �ٽ� �����, �Ƹ�ī��� ��� ��ü�� �ݵ�� �����Ű�ڴٰ� �ߴٴ���..",
                                                                 "���� ���� ����� �̾߱�����, �������� ��� �ҹ��� ����̶��, �츮�� �翬�� ���� �� ��ȭ�� �غ��¼��� ���߾���Ѵ�.. �� ������ ���ü���� �����ؼ� �����������, ���� �߿��� ���� �������� �ٽô� ����� ���ϰ� ������ �����ؾ� �Ѵ�.",
                                                                 "�������� ���� �Ǿ��� ��� �� ������ ����ߴ� �����θ� ������ �������� <���� ��>�� �ִٰ� �ϴ���. �������� ����Ϸ��� �� ���� �ݵ�� �־�� �� ���̴�.",
                                                                 "�������� �����ϱ� ���ؼ�, ���� �θ���� ó������ �������� �����̾���, <���� ��>�� ã�°� �� �켱�̾�� �� ���̴�..",
                                                                 "����� ��Ź���� ���� �˰��ְ�, �ڳ׵� ����ϰ� ������ ���� ������, �츮 �絧�þ�, �� ũ�Դ� �Ƹ�ī��� ����� ���� �ڳװ� �ι�°�� �������� �������� ���ƺ��°� ���..",
                                                                 "�������μ� ��Ź�ϳ�. �絧�þ�, �׸��� �� �Ƹ�ī��� ����� �� �ڳװ� �����־����� �ϳ�.",
                                                                 "���ð� ��Ű���� �ٽ� ã�ư�����. �� �Ϸ������̴� ����, �������� �Ʒ������� ��ƺ����� �ϰԳ�."});


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
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[] {"����Բ��� ��⸦ ���ص����. �������� �Ʒ��� �ް�ʹٰ� ? ", 
                                                             "�������� ���� �翬�� ���� ����.. ������ ����� �ڽ��� �ܷý�Ű�ٺ��� �ſ� ������ �ڽ��� �� �� ���� ���̴�.",
                                                             "���� ���⸦ �ڽ��� �����ؼ� �ܷý�ų �� ������, ��Ȳ�� ���� �����ϰ� ��ó�� �����Ϸ��� ������ ���� ������ ���⸦ ���޽�Ű�°� ����. ������ �̰��� ������ ��, ������ ���� ���̴�.",
                                                             "�絧�þ� ���� �ʵ�� ����, ��ġ���� ��ƺ����� ��. 20������ ���, ������ ���ƿͼ� ������."});

    }

    public void Additional_Talk_Rudencian_training_officer_RooKiss_Quest2()
    {

        for (int k = 0; k < Player_Quest.Instance.PlayerQuest.Count; k++)
        {
            if (Player_Quest.Instance.PlayerQuest[k].Quest_ID == 6 && Player_Quest.Instance.PlayerQuest[k].npc_meet == true) // ����Ʈ�� �̹� �Ϸ�Ǿ����� ���� 
            {
                return;
            }

            Player_Quest.Instance.PlayerQuest[k].npc_meet = true;
        }

        TalkData.Remove(RUDENCIAN_ROOKISS_NPC_ID);
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[] {"��ġ���� �����ϴ� �� ���� ���� �����־���! , ó������ �Ƿ��� ���ݾ� �þ�� ���� ���� ���̴±�!",
         "���� �������� �̾߱⸦ ���ݾ� �ص� �ɰ� ����... <���� ��>�� ���εǾ� �ִ� ���� �Ǵٸ� ������ ���� ����� �ʿ��� �˷��ָ�.",
         "<���� ��>�� �ʰ� ������ �ϸ鼭, �� ����� ������ִ� <4���� ���ɼ�>�� ��Ƽ� ������ ���� �Ǹ�, �� ������ ���� ���εǾ��ִٰ��Ѵ�.",
         "���ɼ��� ��Ȯ�� ��� �ִ��� ��Ȯ�� �� ��������, ���� �θ�Ե� ���������� ���� ã�Ƴ� ��, �ʿ��� ���� �ູ�� �����ٸ�, ���ɼ��� ����� �ʰ� ���� �� �����Ŷ� �ϴ´�.",
         "�׷��� ����, �� ���� �޶� ���� �� �ϳ��� ��� ������ �Ϸ��� �ϴ°ų� ! , � �������� ����, [�Ӹ�, �ѿ�, �Ź�] ������ �����ؼ�, ������ ���纸���� �ض�."});

    }
    public void Additional_Talk_Rudencian_training_officer_RooKiss_Quest3()
    {

        for (int k = 0; k < Player_Quest.Instance.PlayerQuest.Count; k++)
        {
            if (Player_Quest.Instance.PlayerQuest[k].Quest_ID == 10 && Player_Quest.Instance.PlayerQuest[k].npc_meet == true) // ����Ʈ�� �̹� �Ϸ�Ǿ����� ���� 
            {
                return;
            }

            Player_Quest.Instance.PlayerQuest[k].npc_meet = true;
        }

        TalkData.Remove(RUDENCIAN_ROOKISS_NPC_ID);
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[] {"��ũ, �׷��� ó�����ٴ� Ȯ���� ������ ������ ���� ��±���.", 
         "������ ������ ������ ���� ������ ���ؼ��� ������ ���� ����� �����ϰ�, �� �����ؼ� ���е��� ������ �������Ѵ�.",
         "���� �ʰ� �� �Ƹ�ī��� ����� ���س� ������ �� ���̶� ���� �ϰ��ִ�. ���� �θ���� �׷��ߵ��� ...",
         "�ʿ��� ������ �ӹ��� �ַ����Ѵ�. ���� �� ū ������ ���ư��� �� �ʼ��� �� �ӹ��� ���̴�. <�絧�þ� ���� ��>�� ����, <ŷ������>�� �����ض�.",
         "�����ϴٰ� �������� �𸣴� ���� �ɷ��� �Ͼ��. Ư��, CŰ�� ������ Ȱ���� �� �ִ� �ڷ���Ʈ ����� Ȱ���� �Ϻ��� Ÿ�ֿ̹� ���ϸ�, ���ظ� ���� ���� �� �ִ�.",
         "�׸���, �ʿ��� Ư���� ����� �ϳ� �˷��ֵ��� �ϸ�. <����� ������>. �����Ƽ�� �����ϴ� ���� ������ ����������, �������� ������ ���� ���� ����� �� ������ ���̴�.",
         "�¸��� ���̶�� �ϴ´�. ���� �� ū ������ ����� ���ư���! ��ũ! � ����ض�!"});

    }

    public void Additional_Talk_Rudencian_training_officer_RooKiss_Quest_Last()
    {

        for (int k = 0; k < Player_Quest.Instance.PlayerQuest.Count; k++)
        {
            if (Player_Quest.Instance.PlayerQuest[k].Quest_ID == 12 && Player_Quest.Instance.PlayerQuest[k].npc_meet == true) // ����Ʈ�� �̹� �Ϸ�Ǿ����� ���� 
            {
                return;
            }

            Player_Quest.Instance.PlayerQuest[k].npc_meet = true;
        }

        TalkData.Remove(RUDENCIAN_ROOKISS_NPC_ID);
        TalkData.Add(RUDENCIAN_ROOKISS_NPC_ID, new string[] {"��ũ, ŷ�������� óġ�Ѱǰ�? ª�� �ð��� ���� ���� �����߱��� ! ",
         "�̰��� ���ɼ��̱�.. ���� ���ɼ��̶�� ���� ��û ���� ��⸦ �������, ������ ���ɼ��� �� ������ ���� ���� ó���̴�.",
         "�ٽ� �ѹ� ���������, ���� �ʰ� �� �Ƹ�ī��� ����� ���س� ������ �� ���̶� ���� �ϰ��ִ�. ���� �θ���� �׷��ߵ��� ...",
         "����, ���̻� ���� �ʿ��� ����ĥ ���� ����. <���ʷ��Ͼ�>�� ����, ���ð� ��Ŀ���� ã�ư���. ������ �̸� ������������, �ٷ� ���� �ʸ� �˾ƺ� ���̴�.",
         "������Ҵ�. �ʴ� �ݵ�� �������� óġ �� ������ �Ǹ��� �ϴ´�. ��ȸ�� �ȴٸ�, ���̶� ���� �絵�� �ϸ�. ����.. \n �׸���, ���ʷ��ϾƷ� �ٷ� �̵��� �� �ִ� ������ ���״�, ����ؼ� �ٷ� �̵��ϰŶ�. ������ ���Ŷ�!",
         });

    }
    public void Reset_TalkData()
    {
        GenerateData();

        return;

    }

    

   
}
