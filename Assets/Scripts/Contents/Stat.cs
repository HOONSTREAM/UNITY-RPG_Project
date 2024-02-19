using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Stat : MonoBehaviour
{
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
   
    private PlayerStat _stat;
    private PlayerController controller;
    public GameObject Fielditem;
   
    
    public int Level { get { return _level; } set {  _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed { get { return _movespeed; } set { _movespeed = value; } }
    

    private void Start()
    {
 
        _level = 1;
        _hp = 100;
        _maxHp = 100;
        _attack = 7;
        _defense = 5;
        _movespeed = 1.0f;
        
        
        controller = GetComponent<PlayerController>();
        Fielditem = Managers.Resources.Load<GameObject>("PreFabs/UI/SubItem/FieldItem");
  
    }

   
    public virtual void OnAttacked(Stat attacker) //���ڷ� ������ ���ݷ��� �޴´�.
    
    {
        int total_damage = Random.Range((int)((attacker.Attack - Defense)*0.8), (int)((attacker.Attack - Defense)*1.1)); // �ɷ�ġ�� 80% ~ 110%       
        
        if(total_damage < 0)
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


    

    protected virtual void OnDead(Stat attacker) //TODO : ����Ȯ��
    {
        if(gameObject.name=="Slime")
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
                
               // TODO : ����Ʈ �ϷῩ�� Ȯ�� �� ���� ī��Ʈ ����

               for(int i = 0; i<QuestDatabase.instance.QuestDB.Count; i++)
                {
                    if(QuestDatabase.instance.QuestDB[i].Quest_ID == 1)
                    {
                        if(QuestDatabase.instance.QuestDB[i].is_complete == true)
                        {
                            return;
                        }

                        QuestDatabase.instance.QuestDB[i].monster_counter++;
                        Player_Quest.Instance.onChangequest.Invoke(); // ī���� ���� ��� �ݿ�
                        
                       
                        break;
                    }
                }
               
            }

            StartCoroutine("MonsterDead");

        }
        else //TODO : �ٸ����� Ȯ�� 
        {

        }
      
    }

   IEnumerator MonsterDead() // ���� ������Ʈ ������ �������Ѽ� ���� ü�¿� ���� �������� �ʰ��Ǿ ��������Ʈ�� �ߵ��� ��
    {
        yield return new WaitForSeconds(0.25f);     
        Managers.Game.DeSpawn(gameObject);
    }
}
