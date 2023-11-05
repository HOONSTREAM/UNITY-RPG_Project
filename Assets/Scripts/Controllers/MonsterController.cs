using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;
    [SerializeField]
    float _scanRange = 10;
    [SerializeField]
    float attackRange = 2;

    public override void Init()
    {
       

        WorldObjectType = Define.WorldObject.Monster;

        _stat = gameObject.GetComponent<Stat>();
        if(gameObject.GetComponentInChildren<UI_HPBar>() == null)
        {
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
            _stat.MoveSpeed = 2.0f; //������ �̵��ӵ� �ʱ⼼�� 

        }

    }

    protected override void UpdateIdle()
    {
        // GameObject player = GameObject.FindGameObjectWithTag("Player"); Withtag �Լ��� �ʹ� ���� 
        GameObject player = Managers.Game.GetPlayer();
        if (player == null)
            return;

        float distance = (player.transform.position - gameObject.transform.position).magnitude;
        if(distance <= _scanRange)
        {
            LockTarget = player;
            State = Define.State.Moving;
            return;

        }

       


    }
    /*UpdateDie �Լ����� ����� ����*/
    
    /*=============================*/
    protected override void UpdateDie() //TODO
    {
        //Debug.Log("Monster Die");
        //Managers.Sound.Play("Slime_Die2",Define.Sound.Effect);
        
        //nowTime += Time.deltaTime;

        //if(nowTime >= DieTime)
        //{
        //    Managers.Game.DeSpawn(gameObject);
        //}
    }
    protected override void UpdateMoving()
    {
        //���ݹ���
        if (LockTarget != null)
        {
            _DesPos = LockTarget.transform.position;
            float distance = (_DesPos - transform.position).magnitude;
            if (distance <= attackRange)
            {

                NavMeshAgent nma = gameObject.GetComponentInChildren<NavMeshAgent>();
                nma.SetDestination(transform.position);
                State = Define.State.Skill;
                return;

            }
        }

        //�̵�
        Vector3 dir = _DesPos - transform.position; // ������ �����ǿ��� �÷��̾��� �������� ���� ������ ���⺤�Ͱ� ���´�.

        if (dir.magnitude < 0.5f) // ������ ��Į���� 0�� �����ϸ� (�������� ����������)
        {
            State = Define.State.Idle;
        }

        else
        {
            NavMeshAgent nma = gameObject.GetAddComponent<NavMeshAgent>(); //�׺�޽� ������Ʈ Ȱ��

            nma.SetDestination(_DesPos);
            nma.speed = _stat.MoveSpeed;


            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

        }
    }
        

       
    
    protected override void UpdateSkill()
    {
       

        if (LockTarget != null)
        {
            Vector3 dir = LockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);

        }
    }

    /* �������� �Դ� �͵��� ������ �Դ� ��󿡴ٰ� �ǰ��Լ��� �����ϴ� ���� ����. ���� ������ų ���� �͵� ����ϱⰡ ������ */
    void OnHitEvent()
    {
       

        if(LockTarget != null)
        {
            Stat targetStat = LockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat); //���� ������ ���ڷ� �־ ������ ü���� ��´�.

            if(targetStat.Hp > 0)
            {
                float distance = (LockTarget.transform.position - transform.position).magnitude;
                if(distance <= attackRange)
                {
                    State = Define.State.Skill;
                }
                else
                {
                    State = Define.State.Moving;
                }
            }
            else
            {
                State = Define.State.Idle;
            }
        }
        else
        {
            State = Define.State.Idle;

        }
    }

    void HitSounds(Define.MouseEvent evt)
    {
        Managers.Sound.Play("Fight Hits 15", Define.Sound.Effect);

    }
}
