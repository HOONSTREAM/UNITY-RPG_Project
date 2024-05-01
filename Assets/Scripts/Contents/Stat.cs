using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;


/// <summary>
/// 몬스터 혹은 플레이어 스텟을 관리하기 위한 기초클래스 입니다. 
/// </summary>

public class Stat : MonoBehaviour
{
    #region 기본 스텟 (몬스터 포함, 플레이어 상속)
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxhp;
    [SerializeField]
    protected int _mp;
    [SerializeField] 
    protected int _maxMp;
    [SerializeField]
    protected float _movespeed;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    #endregion

    public GameObject Fielditem; // Monster 사망 후, 필드에 드랍되는 아이템 스프라이트

    public int LEVEL { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MAXHP { get { return _maxhp; } set { _maxhp = value; } }
    public int Mp { get { return _mp; } set { _mp = value; } }
    public int MaxMp { get { return _maxMp; } set { _maxMp = value; } }
    public int ATTACK { get { return _attack; } set { _attack = value; } }
    public int DEFENSE { get { return _defense; } set { _defense = value; } }
    public float MOVESPEED { get { return _movespeed; } set { _movespeed = value; } }


    private void Start()
    {
        Managers.StatFactory.CreateStatForMonster(gameObject); 
        Fielditem = Managers.Resources.Load<GameObject>("PreFabs/UI/SubItem/FieldItem");

    }


    /// <summary>
    ///  공격자(가해자)로 부터 받는 데미지를 계산하여 나의 체력을 깎습니다.
    /// <param name="attacker">첫 번째 인자 : 공격자의 스텟 </param>
    /// </summary>
    public virtual void OnAttacked(Stat attacker) 

    {
        // (가해자의 공격력 - 나의 방어력)
        int total_damage = Random.Range((int)((attacker.ATTACK - DEFENSE) * 0.8), (int)((attacker.ATTACK - DEFENSE) * 1.1)); // 능력치의 80% ~ 110%       

        if (total_damage < 0)
        {
            total_damage = 0;
        }

        Hp -= total_damage;


        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }

    }

    /// <summary>
    ///  사망 후의 처리를 하는 메서드 입니다. 사냥조건의 퀘스트가 있다면, 같이 처리합니다.
    /// <param name="attacker">첫 번째 인자 : 공격자의 스텟 </param>
    /// </summary>
    protected virtual void OnDead(Stat attacker)
    {
        if (gameObject.name == "Slime")
        {
            PlayerStat playerstat = attacker as PlayerStat;
            if (playerstat != null)
            {
                playerstat.EXP += Managers.StatFactory.GetExperiencePoints(gameObject);              
                playerstat.onchangestat.Invoke();
                GameObject dropitem = Fielditem.GetComponent<FieldItem>().SlimeDropFieldItem();
                dropitem.transform.position = transform.position; //드랍아이템 위치
                dropitem.transform.position += new Vector3(0, 0.4f, 0); //2D 스프라이트 잘림방지

                QuestDatabase.instance.Kill_Slime_For_Main_Quest();
            }

            StartCoroutine("MonsterDead");
        
        }

        else if (gameObject.name == "Punch_man")
        {
            PlayerStat playerstat = attacker as PlayerStat; // 캐스팅

            if (playerstat != null)
            {
                playerstat.EXP += Managers.StatFactory.GetExperiencePoints(gameObject);
                playerstat.onchangestat.Invoke();
                GameObject dropitem = Fielditem.GetComponent<FieldItem>().PunchmanDropFieldItem();
                dropitem.transform.position = transform.position; //드랍아이템 위치
                dropitem.transform.position += new Vector3(0, 0.4f, 0); //2D 스프라이트 잘림방지

                if (QuestDatabase.instance.QuestDB[4].is_complete == true)
                {
                    QuestDatabase.instance.Kill_Punch_man_For_Main_Quest();
                }
                
            }

            StartCoroutine("MonsterDead");

        }

        else if (gameObject.name == "Turtle_Slime")
        {
            PlayerStat playerstat = attacker as PlayerStat; // 캐스팅

            if (playerstat != null)
            {
                playerstat.EXP += Managers.StatFactory.GetExperiencePoints(gameObject);
                playerstat.onchangestat.Invoke();
                GameObject dropitem = Fielditem.GetComponent<FieldItem>().Turtle_Slime_DropFieldItem();
                dropitem.transform.position = transform.position; //드랍아이템 위치
                dropitem.transform.position += new Vector3(0, 0.4f, 0); //2D 스프라이트 잘림방지

            }

            StartCoroutine("MonsterDead");

        }

    }

    IEnumerator MonsterDead()
    {
        yield return new WaitForSeconds(0.25f);
        Managers.Game.DeSpawn(gameObject);
    }


}
 
    

   
