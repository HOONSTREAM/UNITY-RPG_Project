using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest_Effect/Main/Quest_12 ��Ű�� ������ ��ȭ")]
public class Quest_12 : Quest_Effect
{
    public override bool ExecuteRole(QuestType questtype)
    {
        GameObject player = Managers.Game.GetPlayer();

        //����Ʈ ����
        QuestDatabase.instance.QuestDB[11].is_complete = true;
        player.GetComponent<PlayerStat>().Gold += QuestDatabase.instance.QuestDB[11].num_1;
        player.GetComponent<PlayerStat>().EXP += QuestDatabase.instance.QuestDB[11].num_2;
        player.GetComponent<PlayerStat>().onchangestat.Invoke();
        Managers.Sound.Play("Coin");

        return true;
    }
}
