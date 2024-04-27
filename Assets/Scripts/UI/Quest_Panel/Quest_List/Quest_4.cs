using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Quest_Effect/Main/Quest_4_촌장과대화")]


public class Quest_4 : Quest_Effect
{
    public override bool ExecuteRole(QuestType questtype)
    {
        GameObject player = Managers.Game.GetPlayer();

        //퀘스트 보상
        QuestDatabase.instance.QuestDB[3].is_complete = true;
        player.GetComponent<PlayerStat>().Gold += QuestDatabase.instance.QuestDB[3].num_1;
        player.GetComponent<PlayerStat>().EXP += QuestDatabase.instance.QuestDB[3].num_2;
        player.GetComponent<PlayerStat>().onchangestat.Invoke();
        Managers.Sound.Play("Coin");
        return true;
    }
}
