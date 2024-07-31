using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 먼저, 몬스터에 붙여질 Stat 객체를 생성하고 초기화하는 역할을 할 StatFactory 클래스입니다. 
/// 이 클래스는 각 몬스터 유형에 맞게 Stat 객체를 설정하고 반환하는 메서드를 포함 합니다.
/// </summary>
public class Monster_Stat_Factory
{
    #region 슬라임 스텟
    private const int SLIME_EXP = 5;
    private const int START_SLIME_LEVEL = 1;
    private const int SLIME_HP = 30;
    private const int SLIME_MAX_HP = 30;
    private const int SLIME_ATTACK = 4;
    private const int SLIME_DEFENSE = 0;
    private const float SLIME_MOVESPEED = 1.0f;
    #endregion

    #region 펀치맨 스텟
    private const int PUNCHMAN_EXP = 9;
    private const int START_PUNCHMAN_LEVEL = 3;
    private const int PUNCHMAN_HP = 60;
    private const int PUNCHMAN_MAX_HP = 60;
    private const int PUNCHMAN_ATTACK = 7;
    private const int PUNCHMAN_DEFENCE = 0;
    private const float PUNCHMAN_MOVESPEED = 1.0f;
    #endregion

    #region 터틀슬라임 스텟
    private const int START_TURTLE_SLIME_EXP = 15;
    private const int START_TURTLE_SLIME_LEVEL = 5;
    private const int START_TURTLE_SLIME_HP = 90;
    private const int START_TURTLE_SLIME_MAX_HP = 90;
    private const int START_TURTLE_SLIME_ATTACK = 10;
    private const int START_TURTLE_SLIME_DEFENCE = 1;
    private const float START_TURTLE_SLIME_MOVESPEED = 1.0f;
    #endregion

    #region 킹슬라임 스텟
    private const int START_KING_SLIME_EXP = 80;
    private const int START_KING_SLIME_LEVEL = 10;
    private const int START_KING_SLIME_HP = 500;
    private const int START_KING_SLIME_MAX_HP = 500;
    private const int START_KING_SLIME_ATTACK = 20;
    private const int START_KING_SLIME_DEFENCE = 5;
    private const float START_KING_SLIME_MOVESPEED = 1.0f;
    #endregion

    /// <summary>
    /// 각 몬스터 별로 스텟을 세팅합니다.
    /// </summary>
    /// <param name="monster"></param>
    /// <returns></returns>
    public Stat CreateStatForMonster(GameObject monster)
    {
        Stat stat = monster.GetComponent<Stat>();
        if (stat == null) return null; // Stat 컴포넌트가 없는 경우

        switch (monster.name)
        {
            case "Slime":
                SetSlimeStats(stat);
                break;
            case "Punch_man":
                SetPunchManStats(stat);
                break;
            case "Turtle_Slime":
                Set_Turtle_Slime_Stats(stat);
                break;
            case "King_Slime":
                Set_King_Slime_Stats(stat);
                break;
            default:
                // 기본값 설정 혹은 예외 처리
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

    private void Set_Turtle_Slime_Stats(Stat stat)
    {
        stat.LEVEL = START_TURTLE_SLIME_LEVEL;
        stat.Hp = START_TURTLE_SLIME_HP;
        stat.MAXHP = START_TURTLE_SLIME_MAX_HP;
        stat.ATTACK = START_TURTLE_SLIME_ATTACK;
        stat.DEFENSE = START_TURTLE_SLIME_DEFENCE;
        stat.MOVESPEED = START_TURTLE_SLIME_MOVESPEED;
    }

    private void Set_King_Slime_Stats(Stat stat)
    {
        stat.LEVEL = START_KING_SLIME_LEVEL;
        stat.Hp = START_KING_SLIME_HP;
        stat.MAXHP = START_KING_SLIME_MAX_HP;
        stat.ATTACK = START_KING_SLIME_ATTACK;
        stat.DEFENSE = START_KING_SLIME_DEFENCE;
        stat.MOVESPEED = START_KING_SLIME_MOVESPEED;
    }



    public int GetExperiencePoints(GameObject monster)
    {
        switch (monster.name)
        {
            case "Slime":
                return SLIME_EXP;
                
            case "Punch_man":
                return PUNCHMAN_EXP;

            case "Turtle_Slime":
                return START_TURTLE_SLIME_EXP;


            default:
                // 기본값 설정 혹은 예외 처리
                return 0;
        }
    }
    

}



