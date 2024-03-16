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

    public GameObject Hit_Particle;

    public override void Init() //�����Լ�
    {
       
        WorldObjectType = Define.WorldObject.Monster;

        _stat = gameObject.GetComponent<Stat>();
        if(gameObject.GetComponentInChildren<UI_HPBar>() == null)
        {
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
            
            _stat.MoveSpeed = 1.25f; //������ �̵��ӵ� �ʱ⼼�� 

        }

        Hit_Particle = Managers.Resources.Load<GameObject>("PreFabs/Monster_Hit_Effect");

    }

    protected override void UpdateIdle()
    {
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
    protected override void UpdateDie() //TODO
    {
        Debug.Log("Monster Die");
     
    }
    protected override void UpdateMoving()
    {
        //���ݹ���
        if (LockTarget != null)
        {
            _DesPos = LockTarget.transform.position; //Ÿ�ٿ��� �� �������� ���� ������ ���⺤�Ͱ� ���´�.
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
        
        if (LockTarget != null)
        {
            Stat targetStat = LockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat); //���� ������ ���ڷ� �־ ������ ü���� ��´�.

            if (targetStat.Hp > 0)
            {
               
                #region ���� ��Ʈ ����Ʈ


                Vector3 particlePosition = LockTarget.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
                Quaternion particleRotation = Quaternion.LookRotation(LockTarget.transform.forward);


                GameObject hit_particles = Instantiate(Hit_Particle, particlePosition, particleRotation);
                hit_particles.SetActive(true);
                Destroy(hit_particles, 1.5f);
                #endregion

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

    void HitSounds(Define.MouseEvent evt)
    {
        Managers.Sound.Play("hit22", Define.Sound.Effect);
    }

   
    IEnumerator wait_attack() // ���ݼӵ� ���� 
    {
        yield return new WaitForSeconds(Managers.CoolTime.monster_attack_cooltime(this.gameObject.name));
        State = Define.State.Skill;
    }
}
