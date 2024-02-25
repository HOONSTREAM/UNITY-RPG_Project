using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest_Effect/Main/Quest_3_케넨과대화하자")]


public class Quest_3 : Quest_Effect
{
    public override bool ExecuteRole(QuestType questtype)
    {
        GameObject player = Managers.Game.GetPlayer();

        //퀘스트 보상
        player.GetComponent<PlayerStat>().Gold += QuestDatabase.instance.QuestDB[2].num_1;
        player.GetComponent<PlayerStat>().EXP += QuestDatabase.instance.QuestDB[2].num_2;
        player.GetComponent<PlayerStat>().onchangestat.Invoke();
        Managers.Sound.Play("Coin");
        return true;
    }
}
