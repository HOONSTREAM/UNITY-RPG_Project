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

    public GameObject Fielditem; // Monster ��� ��, �ʵ忡 ����Ǵ� ������ ��������Ʈ

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
    ///  ������(������)�� ���� �޴� �������� ����Ͽ� ���� ü���� ����ϴ�.
    /// <param name="attacker">ù ��° ���� : �������� ���� </param>
    /// </summary>
    public virtual void OnAttacked(Stat attacker) 

    {
        // (�������� ���ݷ� - ���� ����)
        int total_damage = Random.Range((int)((attacker.ATTACK - DEFENSE) * 0.8), (int)((attacker.ATTACK - DEFENSE) * 1.1)); // �ɷ�ġ�� 80% ~ 110%       

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
    ///  ��� ���� ó���� �ϴ� �޼��� �Դϴ�. ��������� ����Ʈ�� �ִٸ�, ���� ó���մϴ�.
    /// <param name="attacker">ù ��° ���� : �������� ���� </param>
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
                dropitem.transform.position = transform.position; //��������� ��ġ
                dropitem.transform.position += new Vector3(0, 0.4f, 0); //2D ��������Ʈ �߸�����

                QuestDatabase.instance.Kill_Slime_For_Main_Quest();
            }

            StartCoroutine("MonsterDead");
        
        }

        else if (gameObject.name == "Punch_man")
        {
            PlayerStat playerstat = attacker as PlayerStat; // ĳ����

            if (playerstat != null)
            {
                playerstat.EXP += Managers.StatFactory.GetExperiencePoints(gameObject);
                playerstat.onchangestat.Invoke();
                GameObject dropitem = Fielditem.GetComponent<FieldItem>().PunchmanDropFieldItem();
                dropitem.transform.position = transform.position; //��������� ��ġ
                dropitem.transform.position += new Vector3(0, 0.4f, 0); //2D ��������Ʈ �߸�����

                if (QuestDatabase.instance.QuestDB[4].is_complete == true)
                {
                    QuestDatabase.instance.Kill_Punch_man_For_Main_Quest();
                }
                
            }

            StartCoroutine("MonsterDead");

        }

        else if (gameObject.name == "Turtle_Slime")
        {
            PlayerStat playerstat = attacker as PlayerStat; // ĳ����

            if (playerstat != null)
            {
                playerstat.EXP += Managers.StatFactory.GetExperiencePoints(gameObject);
                playerstat.onchangestat.Invoke();
                GameObject dropitem = Fielditem.GetComponent<FieldItem>().Turtle_Slime_DropFieldItem();
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
 
    

   
