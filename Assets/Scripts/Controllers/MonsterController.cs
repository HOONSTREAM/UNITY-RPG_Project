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
            _stat.MoveSpeed = 2.0f; //몬스터의 이동속도 초기세팅 

        }

    }

    protected override void UpdateIdle()
    {
        // GameObject player = GameObject.FindGameObjectWithTag("Player"); Withtag 함수는 너무 느림 
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
    /*UpdateDie 함수에서 사용할 변수*/
    
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
        //공격범위
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

        //이동
        Vector3 dir = _DesPos - transform.position; // 목적지 포지션에서 플레이어의 포지션을 빼면 가야할 방향벡터가 나온다.

        if (dir.magnitude < 0.5f) // 방향의 스칼라값이 0에 수렴하면 (목적지에 도착했으면)
        {
            State = Define.State.Idle;
        }

        else
        {
            NavMeshAgent nma = gameObject.GetAddComponent<NavMeshAgent>(); //네비메시 에이전트 활용

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

    /* 데미지를 입는 것들은 데미지 입는 대상에다가 피격함수를 구현하는 것이 좋다. 향후 버프스킬 같은 것들 계산하기가 편리해짐 */
    void OnHitEvent()
    {
       

        if(LockTarget != null)
        {
            Stat targetStat = LockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat); //나의 스텟을 인자로 넣어서 상대방의 체력을 깎는다.

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
