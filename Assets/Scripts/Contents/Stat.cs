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
    protected int _maxHp;
    [SerializeField]
    protected float _movespeed;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    #endregion

    public GameObject Fielditem; // Monster 사망 후, 필드에 드랍되는 아이템 스프라이트

    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed { get { return _movespeed; } set { _movespeed = value; } }


    private void Start()
    {

        switch (gameObject.name)
        {
            case "Slime":

                _level = 1;
                _hp = 100;
                _maxHp = 100;
                _attack = 10;
                _defense = 5;
                _movespeed = 1.0f;

                break;

            case "Punch_man":

                _level = 3;
                _hp = 200;
                _maxHp = 200;
                _attack = 15;
                _defense = 5;
                _movespeed = 2f;

                break;


        }
        
        Fielditem = Managers.Resources.Load<GameObject>("PreFabs/UI/SubItem/FieldItem");

    }


    /// <summary>
    ///  공격자(가해자)로 부터 받는 데미지를 계산하여 나의 체력을 깎습니다.
    /// <param name="attacker">첫 번째 인자 : 공격자의 스텟 </param>
    /// </summary>
    public virtual void OnAttacked(Stat attacker) 

    {
        // (가해자의 공격력 - 나의 방어력)
        int total_damage = Random.Range((int)((attacker.Attack - Defense) * 0.8), (int)((attacker.Attack - Defense) * 1.1)); // 능력치의 80% ~ 110%       

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
    ///  사망 후의 처리를 하는 메서드 입니다.
    /// <param name="attacker">첫 번째 인자 : 공격자의 스텟 </param>
    /// </summary>
    protected virtual void OnDead(Stat attacker)
    {
        if (gameObject.name == "Slime")
        {
            PlayerStat playerstat = attacker as PlayerStat;
            if (playerstat != null)
            {
                playerstat.EXP += 5;
                playerstat.Gold += 100;
                playerstat.onchangestat.Invoke();
                GameObject dropitem = Fielditem.GetComponent<FieldItem>().SlimeDropFieldItem();
                dropitem.transform.position = transform.position; //드랍아이템 위치
                dropitem.transform.position += new Vector3(0, 0.4f, 0); //2D 스프라이트 잘림방지

         
                for (int i = 0; i < QuestDatabase.instance.QuestDB.Count; i++)
                {
                    if (QuestDatabase.instance.QuestDB[i].Quest_ID == 1)
                    {
                        if (QuestDatabase.instance.QuestDB[i].is_complete == false)
                        {
                            QuestDatabase.instance.QuestDB[i].monster_counter++;
                            Player_Quest.Instance.onChangequest.Invoke(); // 카운터 증가 즉시 반영
                        }

                        break;
                    }
                }

            }

            StartCoroutine("MonsterDead");

           
        }

        else if (gameObject.name == "Punch_man")
        {
            PlayerStat playerstat = attacker as PlayerStat;

            if (playerstat != null)
            {
                playerstat.EXP += 10;
                playerstat.Gold += 100;
                playerstat.onchangestat.Invoke();
                GameObject dropitem = Fielditem.GetComponent<FieldItem>().PunchmanDropFieldItem();
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
 
    

   
