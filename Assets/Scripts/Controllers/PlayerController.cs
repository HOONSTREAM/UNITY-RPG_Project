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

    private int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster); //레이어마스크
    bool skill_is_stop = false; // 스킬사용을 멈추었는지 판정하기위한 플래그 변수
    private PlayerStat _stat; // 플레이어의 스텟
    float attackRange = 2.0f;

    public DamageNumber DamageText;
    public GameObject clickMarker;
    private GameObject clickMarker_global_variable;
    public GameObject hit_particle;

    public override void Init()
    {

        WorldObjectType = Define.WorldObject.Player;

        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent; //Inputmanager에게 키액션이 발생하면 OnMouseClicked 함수를 실행할 것을 요청.
        Managers.Input.MouseAction += OnMouseEvent;

    }

    protected override void UpdateMoving()
    {
        
        if (LockTarget != null)
        {

            float distance = (_DesPos - transform.position).magnitude;

            if (distance <= attackRange)
            {
                Destroy(clickMarker_global_variable); //전투중에는 마우스Marker가 뜨지않도록 Destroy 

                if (LockTarget.gameObject.GetComponent<Stat>().Hp <= 0) //락타겟이 있어도 Hp가 0이하이면 공격애니메이션 재생X
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

        Vector3 dir = _DesPos - transform.position; // 목적지 포지션에서 플레이어의 포지션을 빼면 가야할 방향벡터가 나온다.
        dir.y = 0;

        if (dir.magnitude < 0.1f) // 방향의 스칼라값이 0에 수렴하면 (목적지에 도착했으면)
        {         
            State = Define.State.Idle;
            Destroy(clickMarker_global_variable); //도착 시 마우스Marker 오브젝트 파괴
        }

        else
        {
           
            float MoveDist = Mathf.Clamp(_stat.MOVESPEED * Time.deltaTime, 0, dir.magnitude); //거리(시간*속력),최솟값,최댓값
            transform.position += dir.normalized * MoveDist; // P = Po + vt(거리)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);


            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Building")))
            {               
                transform.position -= dir * 0.1f; //뒤로밀어내기 
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
    /// 플레이어의 애니메이션 클립 이벤트에 등록되어, 타격 타이밍에 실행되는 메서드 입니다.
    /// </summary>
    void player_OnHitEvent() 
    {
        if (LockTarget != null)
        {
           
            Stat targetStat = LockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat); //나의 스텟을 인자로 넣어서 상대방의 체력을 깎는다.;

            Print_Damage_Text(targetStat);           
            Player_Hit_Effect();
          
            Ability_Script Ability_script = GameObject.Find("Ability_Slot_CANVAS").gameObject.GetComponent<Ability_Script>();
            Managers.Monster_Info.Set_Monster_Info(LockTarget);
            Managers.Monster_Info.OnMonsterUpdated += (LockTarget) => { Debug.Log($"몬스터가 세팅되었습니다.{Managers.Monster_Info.Get_Monster_Info().ToString()}"); };
            Ability_script.Accumulate_Ability_Func();         
            _stat.onchangestat.Invoke(); // 유저 스텟 인터페이스 업데이트
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
    /// 플레이어의 히트 이펙트를 관리하는 메서드입니다.
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
    /// 플레이어가 몬스터를 타격할 때 출력되는 데미지 텍스트를 관리하는 메서드 입니다.
    /// <param name="stat">첫 번째 인자 : 타겟(타격의대상)의 스텟 </param>
    /// </summary>
    /// <param name="targetStat"></param>
    /// 
    private void Print_Damage_Text(Stat targetStat)
    {
        if (targetStat.Hp >= 0)
        {
            int damage_amount = Random.Range((int)((_stat.ATTACK - targetStat.DEFENSE) * 0.8), (int)((_stat.ATTACK - targetStat.DEFENSE) * 1.1)); // 능력치의 80% ~ 110%             
            DamageNumber damageNumber = DamageText.Spawn(LockTarget.transform.position,damage_amount);
        }
    }

    /// <summary>
    /// 플레이어가 마우스로 이동 시, 마우스 클릭커 이펙트를 제어하는 메서드입니다.
    /// </summary>
    /// <param name="vector3"></param>
    private void Mouse_Click_Effect(Vector3 vector3)
    {

        // 기존의 클릭 마커 오브젝트가 존재한다면 삭제한다.
        if (clickMarker_global_variable != null)
        {
            Destroy(clickMarker_global_variable);
        }

        GameObject go = Instantiate(clickMarker, _DesPos + vector3, Quaternion.identity); //목적지 마우스Marker
        clickMarker_global_variable = go;
        go.SetActive(true);

        Destroy(go,3.0f);

        State = Define.State.Moving;
    }
}


