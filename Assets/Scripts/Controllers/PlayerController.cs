using DamageNumbersPro;
using Data;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngineInternal;
using static UnityEditor.Progress;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : BaseController
{

    private int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster); //���̾��ũ
    bool skill_is_stop = false; // ��ų����� ���߾����� �����ϱ����� �÷��� ����
    private PlayerStat _stat; // �÷��̾��� ����
    float attackRange = 2.0f;

    public DamageNumber DamageText;
    public GameObject clickMarker;
    private GameObject clickMarker_global_variable;
    public GameObject hit_particle;

    public override void Init()
    {

        WorldObjectType = Define.WorldObject.Player;

        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent; //Inputmanager���� Ű�׼��� �߻��ϸ� OnMouseClicked �Լ��� ������ ���� ��û.
        Managers.Input.MouseAction += OnMouseEvent;

    }

    protected override void UpdateMoving()
    {
        
        if (LockTarget != null)
        {

            float distance = (_DesPos - transform.position).magnitude;

            if (distance <= attackRange)
            {
                Destroy(clickMarker_global_variable); //�����߿��� ���콺Marker�� �����ʵ��� Destroy 

                if (LockTarget.gameObject.GetComponent<Stat>().Hp <= 0) //��Ÿ���� �־ Hp�� 0�����̸� ���ݾִϸ��̼� ���X
                {
                    skill_is_stop = true;
                    State = Define.State.Idle;
                    LockTarget = null;
                    
                    return;

                }
                else
                {
                    State = Define.State.Skill;
                    return;
                }
               
            }
        }

        Vector3 dir = _DesPos - transform.position; // ������ �����ǿ��� �÷��̾��� �������� ���� ������ ���⺤�Ͱ� ���´�.
        dir.y = 0;

        if (dir.magnitude < 0.1f) // ������ ��Į���� 0�� �����ϸ� (�������� ����������)
        {         
            State = Define.State.Idle;
            Destroy(clickMarker_global_variable); //���� �� ���콺Marker ������Ʈ �ı�
        }

        else
        {
           
            float MoveDist = Mathf.Clamp(_stat.MOVESPEED * Time.deltaTime, 0, dir.magnitude); //�Ÿ�(�ð�*�ӷ�),�ּڰ�,�ִ�
            transform.position += dir.normalized * MoveDist; // P = Po + vt(�Ÿ�)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);


            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Building")))
            {               
                transform.position -= dir * 0.1f; //�ڷιо�� 
                State = Define.State.Idle;
                return;
            }

        }
    }
  
    protected override void UpdateSkill()
   {
        
        if (LockTarget!=null)
        {
            Vector3 dir = LockTarget.transform.position-transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
  
        }

    }
  
    /// <summary>
    /// �÷��̾��� �ִϸ��̼� Ŭ�� �̺�Ʈ�� ��ϵǾ�, Ÿ�� Ÿ�ֿ̹� ����Ǵ� �޼��� �Դϴ�.
    /// </summary>
    void player_OnHitEvent() 
    {
        if (LockTarget != null)
        {
           
            Stat targetStat = LockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat); //���� ������ ���ڷ� �־ ������ ü���� ��´�.;

            Print_Damage_Text(targetStat);           
            Player_Hit_Effect();
          
            Ability_Script Ability_script = GameObject.Find("Ability_Slot_CANVAS").gameObject.GetComponent<Ability_Script>();
            Managers.Monster_Info.Set_Monster_Info(LockTarget);
            Managers.Monster_Info.OnMonsterUpdated += (LockTarget) => { Debug.Log($"���Ͱ� ���õǾ����ϴ�.{Managers.Monster_Info.Get_Monster_Info().ToString()}"); };
            Ability_script.Accumulate_Ability_Func();         
            _stat.onchangestat.Invoke(); // ���� ���� �������̽� ������Ʈ
        }

        if (skill_is_stop)
        {          
            State = Define.State.Idle;           
        }
        else if (skill_is_stop == false)
        {           
            State = Define.State.Skill;         
        }

    }
    void OnMouseEvent(Define.MouseEvent evt)
    {
        
        switch (State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRUN(evt);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRUN(evt);
                break;
            case Define.State.Skill:
                {
                    if(evt == Define.MouseEvent.PointerUp || evt == Define.MouseEvent.Click)
                    {                      
                        skill_is_stop = true;
                    }
                   
                }
                break;

        }


     }

    void player_HitSounds()
    {            
        Managers.Sound.Play("crack10.mp3", Define.Sound.Effect);
    
        return;
    }
    void OnMouseEvent_IdleRUN(Define.MouseEvent evt)
    {
       
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    
        
        bool raycasthit = Physics.Raycast(ray, out hit, 100.0f, _mask);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);


        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                   
                    if (raycasthit)
                    {
                         Vector3 vector3 =new Vector3(0f, 0.7f, 0f);
                        _DesPos = hit.point;                      
                        Mouse_Click_Effect(vector3);                    
                        skill_is_stop = true;
                        
  
                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                        {
                            LockTarget = hit.collider.gameObject;
                            
                        }

                        else
                        {
                            LockTarget = null;                           
                        }

                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                   
                    if (LockTarget==null && raycasthit)
                        {
                            _DesPos = hit.point;
                        }
                }
                break;

            case Define.MouseEvent.PointerUp:
                {
                    
                    skill_is_stop = true;
                }
                
                break;

            case Define.MouseEvent.Click:
                {
                   
                    skill_is_stop = true;
                }
                break;


        }

    }

    /// <summary>
    /// �÷��̾��� ��Ʈ ����Ʈ�� �����ϴ� �޼����Դϴ�.
    /// </summary>
    private void Player_Hit_Effect()
    {
        Vector3 particlePosition = LockTarget.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        Quaternion particleRotation = Quaternion.LookRotation(LockTarget.transform.forward);


        GameObject hit_particles = Instantiate(hit_particle, particlePosition, particleRotation);
        hit_particles.SetActive(true);
        Destroy(hit_particles, 1.5f);
    }


    /// <summary>
    /// �÷��̾ ���͸� Ÿ���� �� ��µǴ� ������ �ؽ�Ʈ�� �����ϴ� �޼��� �Դϴ�.
    /// <param name="stat">ù ��° ���� : Ÿ��(Ÿ���Ǵ��)�� ���� </param>
    /// </summary>
    /// <param name="targetStat"></param>
    /// 
    private void Print_Damage_Text(Stat targetStat)
    {
        if (targetStat.Hp >= 0)
        {
            int damage_amount = Random.Range((int)((_stat.ATTACK - targetStat.DEFENSE) * 0.8), (int)((_stat.ATTACK - targetStat.DEFENSE) * 1.1)); // �ɷ�ġ�� 80% ~ 110%             
            DamageNumber damageNumber = DamageText.Spawn(LockTarget.transform.position,damage_amount);
        }
    }

    /// <summary>
    /// �÷��̾ ���콺�� �̵� ��, ���콺 Ŭ��Ŀ ����Ʈ�� �����ϴ� �޼����Դϴ�.
    /// </summary>
    /// <param name="vector3"></param>
    private void Mouse_Click_Effect(Vector3 vector3)
    {

        // ������ Ŭ�� ��Ŀ ������Ʈ�� �����Ѵٸ� �����Ѵ�.
        if (clickMarker_global_variable != null)
        {
            Destroy(clickMarker_global_variable);
        }

        GameObject go = Instantiate(clickMarker, _DesPos + vector3, Quaternion.identity); //������ ���콺Marker
        clickMarker_global_variable = go;
        go.SetActive(true);

        Destroy(go,3.0f);

        State = Define.State.Moving;
    }
}


