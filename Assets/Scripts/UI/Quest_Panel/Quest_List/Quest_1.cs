using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest_Effect/Main/Quest_1_���ο����")]

//���� : �� ���迡 ó������ ���� ���!

/*�� ���迡 ó������ ���� ��� �� ���谡! ��ü �� ���迡�� ������ ��ٸ��� �ִ°��ϱ�? 
  �ϴ� ���� ������ ���̴� ���彽������ ��ƺ����� ����!  
  ���彽������ ��ƺ��鼭 ����� ������ ���� �������� ������ ���� �߿��ϴ�! */

public class Quest_1 : Quest_Effect
{
    public override bool ExecuteRole(QuestType questtype)
    {
        GameObject player = Managers.Game.GetPlayer();

        //����Ʈ ����
        player.GetComponent<PlayerStat>().Gold += QuestDatabase.instance.QuestDB[0].num_1;
        player.GetComponent<PlayerStat>().EXP += QuestDatabase.instance.QuestDB[0].num_2;
        Managers.Sound.Play("Coin");
        return true;
    }
}
