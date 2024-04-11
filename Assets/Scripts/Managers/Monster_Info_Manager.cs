using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// �÷��̾��� Ÿ�ݴ���� �� ������ ������ �����ϰ� �����ϴ� �Ŵ��� �Դϴ�.
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
