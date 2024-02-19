using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest_Effect/Main/Quest_1_새로운시작")]

//내용 : 이 세계에 처음으로 발을 딛다!

/*이 세계에 처음으로 발을 딛게 된 모험가! 대체 이 세계에는 무엇이 기다리고 있는것일까? 
  일단 먼저 가까이 보이는 레드슬라임을 잡아보도록 하자!  
  레드슬라임을 잡아보면서 사냥의 감각을 익혀 생존법을 익히는 것이 중요하다! */

public class Quest_1 : Quest_Effect
{
    public override bool ExecuteRole(QuestType questtype)
    {
        GameObject player = Managers.Game.GetPlayer();
        
        //퀘스트 보상
        Managers.Sound.Play("Coin");
        return true;
    }
}
