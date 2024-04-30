using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Quest_Effect/Main/Quest_4_�������ȭ")]


public class Quest_4 : Quest_Effect
{
    public override bool ExecuteRole(QuestType questtype)
    {
        GameObject player = Managers.Game.GetPlayer();

        //����Ʈ ����
        QuestDatabase.instance.QuestDB[3].is_complete = true;
        player.GetComponent<PlayerStat>().Gold += QuestDatabase.instance.QuestDB[3].num_1;
        player.GetComponent<PlayerStat>().EXP += QuestDatabase.instance.QuestDB[3].num_2;
        player.GetComponent<PlayerStat>().onchangestat.Invoke();
        Managers.Sound.Play("Coin");



        Managers.Sound.Clear();
        Managers.Sound.Play("Quest Start DRUMS BIG", Define.Sound.Effect);
        GameObject go = Managers.Resources.Instantiate("Chapter_Start");
        Destroy(go, 5.0f);
        

        return true;
    }
}
