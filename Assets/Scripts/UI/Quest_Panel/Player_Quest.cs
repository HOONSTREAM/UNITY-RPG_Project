using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Quest : MonoBehaviour
{

    public static Player_Quest Instance;

    private PlayerStat stat;
    public List<Quest> PlayerQuest;


    public delegate void OnChangeQuest();
    public OnChangeQuest onChangequest;
    



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
        if (PlayerQuest.Count > 0) //Null Crash ¹æÁö
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
