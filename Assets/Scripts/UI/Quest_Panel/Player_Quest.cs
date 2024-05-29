using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerSkillQuickSlot;

public class Player_Quest : MonoBehaviour
{

    #region 플레이어 퀘스트 정보 저장
    [System.Serializable]
    public class Player_Quest_Data
    {
        public List<Quest> quests;

        public Player_Quest_Data(List<Quest> player_quest_data)
        {
            this.quests = player_quest_data;
        }
    }
    #endregion


    public static Player_Quest Instance;

    private PlayerStat stat;
    public List<Quest> PlayerQuest;


    public delegate void OnChangeQuest();
    public OnChangeQuest onChangequest;


    #region 메서드 퀘스트 정보 저장
    public void Save_Player_Quest_Info()
    {
        Player_Quest_Data data = new Player_Quest_Data(PlayerQuest);
        ES3.Save("Player_Quest_Data", data);



        Debug.Log("Player_Quest_Data saved using EasySave3");
    }

    public void Load_Player_Quest_Info()
    {
        if (ES3.KeyExists("Player_Quest_Data"))
        {
            Player_Quest_Data data = ES3.Load<Player_Quest_Data>("Player_Quest_Data");
            PlayerQuest = data.quests;
            Debug.Log("Player_Quest_Data loaded using EasySave3");
        }
        else
        {
            Debug.Log("No Player_Quest_Data data found, creating a new one.");
        }
    }
    #endregion

    private void Awake()
    {

        Instance = this;
        PlayerQuest = new List<Quest>();
        stat = GetComponent<PlayerStat>();

    }


    public bool AddQuest(Quest _quest) 
    {
        PlayerQuest.Add(_quest);

        if (onChangequest != null)
        {
            onChangequest.Invoke();

        }

        return true;
    }

    public void RemoveQuest(int index)
    {
        if (PlayerQuest.Count > 0) //Null Crash 방지
        {
            PlayerQuest.RemoveAt(index);
            onChangequest.Invoke();

        }

        else
        {
            return;
        }

        return;
    }



}
