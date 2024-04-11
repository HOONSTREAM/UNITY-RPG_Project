using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 플레이어의 타격대상이 된 몬스터의 정보를 저장하고 관리하는 매니저 입니다.
/// </summary>
public class Monster_Info_Manager 
{

    public  event Action<GameObject> OnMonsterUpdated;

    private GameObject monster_Info;


    public void Set_Monster_Info (GameObject monster)
    {
        monster_Info = monster;
        OnMonsterUpdated?.Invoke(monster);
    }

    public GameObject Get_Monster_Info()
    {
        if (monster_Info == null)
        {
            return null;
        }
          
        return monster_Info;
    }

}
