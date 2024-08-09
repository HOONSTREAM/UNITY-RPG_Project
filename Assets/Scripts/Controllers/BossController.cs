using DamageNumbersPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : Base_Monster_Controller
{


    Stat _stat;
    [SerializeField]
    float _scanRange = 60.0f; // �÷��̾� ��ĵ����
    [SerializeField]
    float attackRange = 2.0f;
    [SerializeField]
    float king_slime_skill_Range = 8.0f;

    public GameObject Hit_Particle; // ���Ͱ� �÷��̾ Ÿ�� �� �� ��Ÿ���� ����Ʈ 
    public GameObject King_Slime_skill_Hit_Particle; // ŷ������ ��ų ����Ʈ 

    [SerializeField]
    float _maxChaseDistance = 60.0f; // �ִ� ���� �Ÿ� ����


    public DamageNumber DamageText;

    public override void Init() //�����Լ�
    {

        WorldObjectType = Define.WorldObject.Monster;

        _stat = gameObject.GetComponent<Stat>();

        if (gameObject.GetComponentInChildren<King_Slime_HP>() == null)
        {
            Managers.UI.ShowSceneUI<King_Slime_HP>("King_Slime_HP");

            _stat.MOVESPEED = 1.25f; //������ �̵��ӵ� �ʱ⼼�� 

        }

        Hit_Particle = Managers.Resources.Load<GameObject>("PreFabs/Hit_Effect/Monster_Hit_Effect");
        King_Slime_skill_Hit_Particle = Managers.Resources.Load<GameObject>("PreFabs/Hit_Effect/King_Slime_Hit_Effect");

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
            State = Define.Monster_State.Moving;
            return;
        }

    }
    protected override void UpdateDie()
    {
        Debug.Log("Monster Die");

    }
    protected override void UpdateMoving()
    {
        //���ݹ���
        if (LockTarget != null)
        {
            _DesPos = LockTarget.transform.position; //Ÿ�ٿ��� �� �������� ���� ������ ���⺤�Ͱ� ����.
            float distance = (_DesPos - transform.position).magnitude;

            //Ÿ�ٰ��� �Ÿ��� ���ݹ��� �̳��� ���ݻ��·� ��ȯ

            if (distance <= king_slime_skill_Range)
            {
                
                NavMeshAgent nma = gameObject.GetComponentInChildren<NavMeshAgent>();
                nma.SetDestination(transform.position); // �̵� ����

                // �������� ���� ����� ����
                int randomAttack = Random.Range(0, 2); // 0 �Ǵ� 1 �߿��� ������ ����

                if (randomAttack == 0)
                {
                    Debug.Log("ŷ������ ��ų ���� �ǽ�");
                    State = Define.Monster_State.King_Slime_Skill; // ŷ������ ��ų ���·� ����
                }
                else
                {
                    
                    Debug.Log("�Ϲ� ���� �ǽ�");
                    State = Define.Monster_State.Skill; // �Ϲ� ��ų ���·� ����
      
                }

                return;
            }


            else if (distance > _maxChaseDistance) // �����Ÿ� �ʰ��� ���� �ߴ�
            {
                State = Define.Monster_State.Idle;
                NavMeshAgent nma = gameObject.GetComponentInChildren<NavMeshAgent>();
                nma.SetDestination(transform.position); //�̵�����

                return;
            }
        }

        #region �̵� ����
        Vector3 dir = _DesPos - transform.position;

        if (dir.magnitude < 0.5f) // ������ ��Į���� 0�� �����ϸ� (�������� ����������)
        {
            State = Define.Monster_State.Idle;
        }

        else
        {
            NavMeshAgent nma = gameObject.GetAddComponent<NavMeshAgent>(); //�׺�޽� ������Ʈ Ȱ��

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

    protected override void Update_King_Slime_Skill()
    {
        if (LockTarget != null)
        {
            Vector3 dir = LockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);

        }
    }


    /// <summary>
    /// �� ������ ���ݼӵ�(��Ÿ��)�� ���� �����ϵ��� �ϴ� �޼����Դϴ�.
    /// ��Ÿ�Ӽ����� ��Ÿ�ӸŴ������� �����մϴ�.
    /// </summary>
    /// <returns></returns>
    IEnumerator wait_attack()
    {
        yield return new WaitForSeconds(Managers.CoolTime.monster_attack_cooltime(this.gameObject.name));
        State = Define.Monster_State.Skill;
    }


    /// <summary>
    /// �ִϸ��̼� Ŭ�� Event�� �ٿ� ���� ���� Ÿ�ֿ̹� ����Ǵ� �޼��� �Դϴ�.
    /// </summary>
    private void OnHitEvent()
    {

        if (LockTarget != null)
        {
            Stat targetStat = LockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat); // Ÿ�� ������ OnAttacked �̹Ƿ�, ������ ü���� ��� ����.


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
                    State = Define.Monster_State.Moving;
                }
            }
            else
            {
                State = Define.Monster_State.Idle;
            }
        }
        else
        {
            State = Define.Monster_State.Idle;
        }

    }

    private void OnHit_KingSlime_Skill_Event()
    {

        if (LockTarget != null && IsPlayerInHitBox())
        {
            Stat targetStat = LockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat); // Ÿ�� ������ OnAttacked �̹Ƿ�, ������ ü���� ��� ����.


            if (targetStat.Hp > 0)
            {

                Print_Damage_Text(targetStat);
                KingSlime_Skill_Hit_Effect();

                float distance = (LockTarget.transform.position - transform.position).magnitude;
                if (distance <= king_slime_skill_Range)
                {
                    StartCoroutine(wait_attack());
                }
                else
                {
                    State = Define.Monster_State.Moving;
                }
            }
            else
            {
                State = Define.Monster_State.Idle;
            }
        }
        else
        {
            Debug.Log("�÷��̾ ��Ʈ�ڽ� ���� ���� �����ϴ�. ������ ȸ���߽��ϴ�.");
            Monster_Miss_Hit_Effect();
            Print_Miss_Damage();
            State = Define.Monster_State.Idle;
        }

       
    }

    private void Monster_Hit_Effect()
    {

        Vector3 particlePosition = LockTarget.transform.position + new Vector3(0.0f, 1.0f, 1.0f);
        Quaternion particleRotation = Quaternion.LookRotation(LockTarget.transform.forward);


        GameObject hit_particles = Instantiate(Hit_Particle, particlePosition, particleRotation);
        hit_particles.SetActive(true);
        Destroy(hit_particles, 1.5f);

    }

    private void Monster_Miss_Hit_Effect()
    {

        Vector3 particlePosition = gameObject.transform.position + new Vector3(5.0f, 1.0f, 0.0f);
        Quaternion particleRotation = Quaternion.LookRotation(gameObject.transform.forward);


        GameObject hit_particles = Instantiate(Hit_Particle, particlePosition, particleRotation);
        hit_particles.SetActive(true);
        Destroy(hit_particles, 1.5f);

    }

    /// <summary>
    /// ŷ�������� ��ų ��Ʈ ����Ʈ�Դϴ�.
    /// </summary>
    private void KingSlime_Skill_Hit_Effect()
    {

        Vector3 particlePosition = LockTarget.transform.position + new Vector3(0.0f, 1.0f, 1.0f);
        Quaternion particleRotation = Quaternion.LookRotation(LockTarget.transform.forward);


        GameObject hit_particles = Instantiate(King_Slime_skill_Hit_Particle, particlePosition, particleRotation);
        hit_particles.SetActive(true);
        Destroy(hit_particles, 1.5f);

    }
    /// <summary>
    /// �÷��̾ ��Ʈ �ڽ� ������ �ִ��� Ȯ���մϴ�.
    /// </summary>
    private bool IsPlayerInHitBox()
    {
        Vector3 boxCenter = transform.position + transform.forward * king_slime_skill_Range / 2;
        Vector3 boxSize = new Vector3(6.0f, 6.0f, king_slime_skill_Range); // �ڽ��� ũ�⸦ �����ϼ���.

        Collider[] hitColliders = Physics.OverlapBox(boxCenter, boxSize / 2, transform.rotation);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == LockTarget)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// ���Ͱ� �÷��̾�� Ÿ���ϴ� �������� ����մϴ�.
    /// </summary>
    /// <param name="targetStat"></param>
    private void Print_Damage_Text(Stat targetStat)
    {
        if (targetStat.Hp >= 0)
        {
            int damage_amount = Random.Range((int)((_stat.ATTACK - targetStat.DEFENSE) * 0.8), (int)((_stat.ATTACK - targetStat.DEFENSE) * 1.1)); // �ɷ�ġ�� 80% ~ 110%


            if (damage_amount <= 0)
            {
                damage_amount = 0;
            }

            DamageNumber damageNumber = DamageText.Spawn(LockTarget.transform.position, damage_amount);
        }
    }

    private void Print_Miss_Damage()
    {

      DamageNumber damageNumber = DamageText.Spawn(LockTarget.transform.position, 0);
      

       return;
    }


    /// <summary>
    /// ���� ��Ʈ ���带 �����մϴ�.
    /// </summary>
    private void HitSounds()
    {
        Managers.Sound.Play("hit22", Define.Sound.Effect);
    }

    /// <summary>
    /// ŷ�������� ��ų ����Ʈ �����Դϴ�.
    /// </summary>
    private void King_Slime_Skill_Hit_Sound()
    {
        Managers.Sound.Play("king_slime_attack", Define.Sound.Effect);
    }








    /// <summary>
    /// Gizmos�� ����Ͽ� ��Ʈ �ڽ��� �׸��ϴ�.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // ��Ʈ�ڽ��� ���������� ǥ��
        Vector3 boxCenter = transform.position + transform.forward * king_slime_skill_Range / 2;
        Vector3 boxSize = new Vector3(6.0f, 6.0f, king_slime_skill_Range); // �ڽ��� ũ�⸦ �����ϼ���.

        Gizmos.matrix = Matrix4x4.TRS(boxCenter, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxSize); // �ڽ��� �߽ɰ� ũ��� ��Ʈ�ڽ� �׸���
    }
}
