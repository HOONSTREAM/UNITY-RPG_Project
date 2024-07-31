using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : BaseController
{


    Stat _stat;
    [SerializeField]
    float _scanRange = 60.0f; // 플레이어 스캔범위
    [SerializeField]
    float attackRange = 2.0f;

    public GameObject Hit_Particle; // 몬스터가 플레이어를 타격 할 때 나타나는 이펙트 

    [SerializeField]
    float _maxChaseDistance = 60.0f; // 최대 추적 거리 설정


    public DamageNumber DamageText;

    public override void Init() //가상함수
    {

        WorldObjectType = Define.WorldObject.Monster;

        _stat = gameObject.GetComponent<Stat>();

        if (gameObject.GetComponentInChildren<King_Slime_HP>() == null)
        {
            Managers.UI.ShowSceneUI<King_Slime_HP>("King_Slime_HP");

            _stat.MOVESPEED = 1.25f; //몬스터의 이동속도 초기세팅 

        }

        Hit_Particle = Managers.Resources.Load<GameObject>("PreFabs/Monster_Hit_Effect");

    }

    protected override void UpdateIdle()
    {
        GameObject player = Managers.Game.GetPlayer();
        if (player == null)
            return;

        float distance = (player.transform.position - gameObject.transform.position).magnitude;
        if (distance <= _scanRange)
        {
            LockTarget = player;
            State = Define.State.Moving;
            return;
        }

    }
    protected override void UpdateDie()
    {
        Debug.Log("Monster Die");

    }
    protected override void UpdateMoving()
    {
        //공격범위
        if (LockTarget != null)
        {
            _DesPos = LockTarget.transform.position; //타겟에서 내 포지션을 빼면 가야할 방향벡터가 나옴.
            float distance = (_DesPos - transform.position).magnitude;

            //타겟과의 거리가 공격범위 이내면 공격상태로 전환
            if (distance <= attackRange)
            {

                NavMeshAgent nma = gameObject.GetComponentInChildren<NavMeshAgent>();
                nma.SetDestination(transform.position); //이동중지
                State = Define.State.Skill; // 공격상태로 변경

                return;

            }

            else if (distance > _maxChaseDistance) // 추적거리 초과시 추적 중단
            {
                State = Define.State.Idle;
                NavMeshAgent nma = gameObject.GetComponentInChildren<NavMeshAgent>();
                nma.SetDestination(transform.position); //이동중지

                return;
            }
        }

        #region 이동 로직
        Vector3 dir = _DesPos - transform.position;

        if (dir.magnitude < 0.5f) // 방향의 스칼라값이 0에 수렴하면 (목적지에 도착했으면)
        {
            State = Define.State.Idle;
        }

        else
        {
            NavMeshAgent nma = gameObject.GetAddComponent<NavMeshAgent>(); //네비메시 에이전트 활용

            nma.SetDestination(_DesPos);
            nma.speed = _stat.MOVESPEED;


            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

        }

        #endregion

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


    /// <summary>
    /// 각 몬스터의 공격속도(쿨타임)에 따라 공격하도록 하는 메서드입니다.
    /// 쿨타임설정은 쿨타임매니저에서 관리합니다.
    /// </summary>
    /// <returns></returns>
    IEnumerator wait_attack()
    {
        yield return new WaitForSeconds(Managers.CoolTime.monster_attack_cooltime(this.gameObject.name));
        State = Define.State.Skill;
    }


    /// <summary>
    /// 애니메이션 클립 Event에 붙여 공격 동작 타이밍에 실행되는 메서드 입니다.
    /// </summary>
    private void OnHitEvent()
    {

        if (LockTarget != null)
        {
            Stat targetStat = LockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat); // 타겟 스텟의 OnAttacked 이므로, 상대방의 체력을 깎는 것임.


            if (targetStat.Hp > 0)
            {

                Print_Damage_Text(targetStat);
                Monster_Hit_Effect();

                float distance = (LockTarget.transform.position - transform.position).magnitude;
                if (distance <= attackRange)
                {
                    StartCoroutine(wait_attack());
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
    /// <summary>
    /// 몬스터 히트 이펙트를 Instantiate 하고, 1.5f 후, Destroy 합니다.
    /// </summary>
    private void Monster_Hit_Effect()
    {

        Vector3 particlePosition = LockTarget.transform.position + new Vector3(0.0f, 1.0f, 1.0f);
        Quaternion particleRotation = Quaternion.LookRotation(LockTarget.transform.forward);


        GameObject hit_particles = Instantiate(Hit_Particle, particlePosition, particleRotation);
        hit_particles.SetActive(true);
        Destroy(hit_particles, 1.5f);

    }


    /// <summary>
    /// 몬스터가 플레이어에게 타격하는 데미지를 출력합니다.
    /// </summary>
    /// <param name="targetStat"></param>
    private void Print_Damage_Text(Stat targetStat)
    {
        if (targetStat.Hp >= 0)
        {
            int damage_amount = Random.Range((int)((_stat.ATTACK - targetStat.DEFENSE) * 0.8), (int)((_stat.ATTACK - targetStat.DEFENSE) * 1.1)); // 능력치의 80% ~ 110%


            if (damage_amount <= 0)
            {
                damage_amount = 0;
            }

            DamageNumber damageNumber = DamageText.Spawn(LockTarget.transform.position, damage_amount);
        }
    }


    /// <summary>
    /// 몬스터 히트 사운드를 선정합니다.
    /// </summary>
    private void HitSounds()
    {
        Managers.Sound.Play("hit22", Define.Sound.Effect);
    }


}
