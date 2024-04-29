using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest_Effect/Main/Quest_1_���ο����")]


public class Quest_1 : Quest_Effect
{
    public override bool ExecuteRole(QuestType questtype)
    {
        GameObject player = Managers.Game.GetPlayer();

        //����Ʈ ����
        QuestDatabase.instance.QuestDB[0].is_complete = true;
        player.GetComponent<PlayerStat>().Gold += QuestDatabase.instance.QuestDB[0].num_1;
        player.GetComponent<PlayerStat>().EXP += QuestDatabase.instance.QuestDB[0].num_2;
        player.GetComponent<PlayerStat>().onchangestat.Invoke();
        Managers.Sound.Play("Coin");



        return true;
    }
}
