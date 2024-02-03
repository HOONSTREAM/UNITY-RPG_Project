using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TalkManager : MonoBehaviour
{

    public const int Helken_NPC_ID = 1003;

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

        TalkData.Add(1000, new string[] { "�ȳ�?", "�� ���� ó�� �Ա���?" ,"NPC ó�� Ŭ���غ���?"}); //��ȭ �ϳ����� ���������� ��������Ƿ� �迭�� ���
        TalkData.Add(100, new string[] { "������Ż�� ��������, ��� �̵��ϴ����� �����غ��� �� �� ����." }); //��ȭ �ϳ����� ���������� ��������Ƿ� �迭�� ���
        TalkData.Add(1001, new string[] { "���� 1001�� NPC, �׽�Ʈ������", "�׽�Ʈ�� �������̾����� ���ڴ�." });
        TalkData.Add(Helken_NPC_ID, new string[] { "�ȳ�, ���� ��� �����̾�.", "�׽�Ʈ�� �������ΰɱ�?" });
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
        TalkData.Remove(Helken_NPC_ID);
        TalkData.Add(Helken_NPC_ID, new string[] {"�絧�þƿ��� ������ �ϳ� �����������´ٴ���. ���� ������ ã�ư���.", 
                                                  "���� ������� ã�ư����� ���� �ܼ��� ���� �� �������� ����."});
    }

    public void Reset_TalkData()
    {
        GenerateData();

        return;

    }

   
}
