using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Quest_Effect/Main/Quest_8 ÀåºñÀåÂø")]

public class Quest_8 : Quest_Effect

{
    public override bool ExecuteRole(QuestType questtype)
    {
        GameObject player = Managers.Game.GetPlayer();

        //Äù½ºÆ® º¸»ó
        QuestDatabase.instance.QuestDB[7].is_complete = true;
        player.GetComponent<PlayerStat>().Gold += QuestDatabase.instance.QuestDB[7].num_1;
        player.GetComponent<PlayerStat>().EXP += QuestDatabase.instance.QuestDB[7].num_2;
        player.GetComponent<PlayerStat>().onchangestat.Invoke();
        Managers.Sound.Play("Coin");

        return true;
    }

}

   

