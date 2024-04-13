using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����, ���Ϳ� �ٿ��� Stat ��ü�� �����ϰ� �ʱ�ȭ�ϴ� ������ �� StatFactory Ŭ�����Դϴ�. 
/// �� Ŭ������ �� ���� ������ �°� Stat ��ü�� �����ϰ� ��ȯ�ϴ� �޼��带 ���� �մϴ�.
/// </summary>
public class Monster_Stat_Factory
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
    public Stat CreateStatForMonster(GameObject monster)
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

    private void SetSlimeStats(Stat stat)
    {       
        stat.LEVEL = START_SLIME_LEVEL;
        stat.Hp = SLIME_HP;
        stat.MAXHP = SLIME_MAX_HP;
        stat.ATTACK = SLIME_ATTACK;
        stat.DEFENSE = SLIME_DEFENSE;
        stat.MOVESPEED = SLIME_MOVESPEED;
    }

    private void SetPunchManStats(Stat stat)
    {       
        stat.LEVEL = START_PUNCHMAN_LEVEL;
        stat.Hp = PUNCHMAN_HP;
        stat.MAXHP = PUNCHMAN_MAX_HP;
        stat.ATTACK = PUNCHMAN_ATTACK;
        stat.DEFENSE = PUNCHMAN_DEFENCE;
        stat.MOVESPEED = PUNCHMAN_MOVESPEED;
    }



    public int GetExperiencePoints(GameObject monster)
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



