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
       // AttackParticle = Managers.Resources.Load<GameObject>("PreFabs/Holy hit");
        

    }

   
    public virtual void OnAttacked(Stat attacker)
    
    {
        int damage = Mathf.Max(0, attacker.Attack - Defense);        
        Hp -= damage;
        
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);

        }
        
    }


    

    protected virtual void OnDead(Stat attacker)
    {
       PlayerStat playerstat = attacker as PlayerStat;
       if(playerstat != null)
        {
            playerstat.EXP += 10;
            GameObject dropitem = ItemDataBase.instance.DropFieldItem();
            dropitem.transform.position = transform.position; //드랍아이템 위치
            dropitem.transform.position += new Vector3(0, 0.4f , 0); //2D 스프라이트 잘림방지                                       
        }
       
     
        Managers.Game.DeSpawn(gameObject);
    }
}
