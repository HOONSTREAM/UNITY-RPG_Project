using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using static Player_Quest;

public class QuestDatabase : MonoBehaviour
{
    #region ����Ʈ �����ͺ��̽� ���� ����
    [System.Serializable]
    public class Quest_DataBase_SaveInfo
    {
        public List<Quest> quest_db;

        public Quest_DataBase_SaveInfo(List<Quest> quest_DB)
        {
            this.quest_db = quest_DB;
        }

    }
    #endregion

    public static QuestDatabase instance;

    public List<Quest> QuestDB = new List<Quest>();

    #region ����Ʈ �����ͺ��̽� ���� ����
    public void Save_QuestDB_Info()
    {
        Quest_DataBase_SaveInfo data = new Quest_DataBase_SaveInfo(QuestDB);
        ES3.Save("Player_QuestDB", data);



        Debug.Log("Player_QuestDB saved using EasySave3");
    }

    public void Load_QuestDB_Info()
    {
        if (ES3.KeyExists("Player_QuestDB"))
        {
            Quest_DataBase_SaveInfo data = ES3.Load<Quest_DataBase_SaveInfo>("Player_QuestDB");
            QuestDB = data.quest_db;
            
            // �Ϸ���� ���� ù ��° ����Ʈ�� �ε�
            Quest firstIncompleteQuest = QuestDB.Find(quest => !quest.is_complete);
            if (firstIncompleteQuest != null)
            {
                Player_Quest.Instance.PlayerQuest = new List<Quest> { firstIncompleteQuest };
                Player_Quest.Instance.onChangequest?.Invoke();
            }
            else
            {
                Debug.Log("��� ����Ʈ�� �Ϸ�Ǿ����ϴ�.");
            }

            Debug.Log("Player_QuestDB loaded using EasySave3");
        }
        else
        {
            Debug.Log("No Player_QuestDB data found, creating a new one.");
        }
    }
    #endregion
    private void Awake()
    {
        instance = this;
    }


    

}
