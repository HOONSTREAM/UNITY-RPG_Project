using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����, ���Ϳ� �ٿ��� Stat ��ü�� �����ϰ� �ʱ�ȭ�ϴ� ������ �� StatFactory Ŭ�����Դϴ�. 
/// �� Ŭ������ �� ���� ������ �°� Stat ��ü�� �����ϰ� ��ȯ�ϴ� �޼��带 ���� �մϴ�.
/// </summary>
public static class Monster_Stat_Factory
{
    #region ������ ����
    private const int SLIME_EXP = 5;
    private const int START_SLIME_LEVEL = 1;
    private const int SLIME_HP = 100;
    private const int SLIME_MAX_HP = 100;
    private const int SLIME_ATTACK = 10;
    private const int SLIME_DEFENSE = 5;
    private const float SLIME_MOVESPEED = 1.0f;
    #endregion

    #region ��ġ�� ����
    private const int PUNCHMAN_EXP = 9;
    private const int START_PUNCHMAN_LEVEL = 3;
    private const int PUNCHMAN_HP = 200;
    private const int PUNCHMAN_MAX_HP = 200;
    private const int PUNCHMAN_ATTACK = 15;
    private const int PUNCHMAN_DEFENCE = 5;
    private const float PUNCHMAN_MOVESPEED = 1.0f;
    #endregion

    /// <summary>
    /// �� ���� ���� ������ �����մϴ�.
    /// </summary>
    /// <param name="monster"></param>
    /// <returns></returns>
    public static Stat CreateStatForMonster(GameObject monster)
    {
        Stat stat = monster.GetComponent<Stat>();
        if (stat == null) return null; // Stat ������Ʈ�� ���� ���

        switch (monster.name)
        {
            case "Slime":
                SetSlimeStats(stat);
                break;
            case "Punch_man":
                SetPunchManStats(stat);
                break;
            default:
                // �⺻�� ���� Ȥ�� ���� ó��
                break;
        }

        return stat;
    }

    private static void SetSlimeStats(Stat stat)
    {       
        stat.Level = START_SLIME_LEVEL;
        stat.Hp = SLIME_HP;
        stat.MaxHp = SLIME_MAX_HP;
        stat.Attack = SLIME_ATTACK;
        stat.Defense = SLIME_DEFENSE;
        stat.MoveSpeed = SLIME_MOVESPEED;
    }

    private static void SetPunchManStats(Stat stat)
    {       
        stat.Level = START_PUNCHMAN_LEVEL;
        stat.Hp = PUNCHMAN_HP;
        stat.MaxHp = PUNCHMAN_MAX_HP;
        stat.Attack = PUNCHMAN_ATTACK;
        stat.Defense = PUNCHMAN_DEFENCE;
        stat.MoveSpeed = PUNCHMAN_MOVESPEED;
    }



    public static int GetExperiencePoints(GameObject monster)
    {
        switch (monster.name)
        {
            case "Slime":
                return SLIME_EXP;
                
            case "Punch_man":
                return PUNCHMAN_EXP;
                
            default:
                // �⺻�� ���� Ȥ�� ���� ó��
                return 0;
        }
    }
    

}



