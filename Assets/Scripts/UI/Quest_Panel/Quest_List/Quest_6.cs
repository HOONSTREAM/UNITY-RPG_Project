using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Quest_Effect/Main/Quest_6 ��ġ�� ����")]
public class Quest_6 : Quest_Effect
{
    public override bool ExecuteRole(QuestType questtype)
    {
        GameObject player = Managers.Game.GetPlayer();

        //����Ʈ ����
        QuestDatabase.instance.QuestDB[5].is_complete = true;
        player.GetComponent<PlayerStat>().Gold += QuestDatabase.instance.QuestDB[5].num_1;
        player.GetComponent<PlayerStat>().EXP += QuestDatabase.instance.QuestDB[5].num_2;
        player.GetComponent<PlayerStat>().onchangestat.Invoke();
        Managers.Sound.Play("Coin");

        return true;
    }


}
