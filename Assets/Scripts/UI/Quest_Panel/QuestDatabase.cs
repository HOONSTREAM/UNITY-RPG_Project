using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{
    public static QuestDatabase instance;

    public List<Quest> QuestDB = new List<Quest>();


    private void Awake()
    {
        instance = this;
    }


   public void KillSlimeForQuest()
    {

        for (int i = 0; i < QuestDatabase.instance.QuestDB.Count; i++)
        {
            if (QuestDatabase.instance.QuestDB[i].Quest_ID == 1)
            {
                if (QuestDatabase.instance.QuestDB[i].is_complete == false)
                {
                    QuestDatabase.instance.QuestDB[i].monster_counter++;
                    Player_Quest.Instance.onChangequest.Invoke(); // 카운터 증가 즉시 반영
                }

                break;
            }
        }
    }

}
