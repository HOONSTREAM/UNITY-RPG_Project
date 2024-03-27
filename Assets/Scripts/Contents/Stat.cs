using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;


/// <summary>
/// ���� Ȥ�� �÷��̾� ������ �����ϱ� ���� ����Ŭ���� �Դϴ�. 
/// </summary>

public class Stat : MonoBehaviour
{
    #region �⺻ ���� (���� ����, �÷��̾� ���)
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

    public GameObject Fielditem; // Monster ��� ��, �ʵ忡 ����Ǵ� ������ ��������Ʈ

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
    ///  ������(������)�� ���� �޴� �������� ����Ͽ� ���� ü���� ����ϴ�.
    /// <param name="attacker">ù ��° ���� : �������� ���� </param>
    /// </summary>
    public virtual void OnAttacked(Stat attacker) 

    {
        // (�������� ���ݷ� - ���� ����)
        int total_damage = Random.Range((int)((attacker.Attack - Defense) * 0.8), (int)((attacker.Attack - Defense) * 1.1)); // �ɷ�ġ�� 80% ~ 110%       

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
    ///  ��� ���� ó���� �ϴ� �޼��� �Դϴ�.
    /// <param name="attacker">ù ��° ���� : �������� ���� </param>
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
                dropitem.transform.position = transform.position; //��������� ��ġ
                dropitem.transform.position += new Vector3(0, 0.4f, 0); //2D ��������Ʈ �߸�����

         
                for (int i = 0; i < QuestDatabase.instance.QuestDB.Count; i++)
                {
                    if (QuestDatabase.instance.QuestDB[i].Quest_ID == 1)
                    {
                        if (QuestDatabase.instance.QuestDB[i].is_complete == false)
                        {
                            QuestDatabase.instance.QuestDB[i].monster_counter++;
                            Player_Quest.Instance.onChangequest.Invoke(); // ī���� ���� ��� �ݿ�
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
                dropitem.transform.position = transform.position; //��������� ��ġ
                dropitem.transform.position += new Vector3(0, 0.4f, 0); //2D ��������Ʈ �߸�����

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
 
    

   
