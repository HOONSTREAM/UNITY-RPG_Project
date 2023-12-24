using Data;
using System.Collections;
using System.Collections.Generic;
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

//참고 : https://geojun.tistory.com/64

public class PlayerController : BaseController
{

    private int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster); //레이어마스크
    bool skill_is_stop = false; // 스킬사용 멈춤 변수
    PlayerStat _stat; // 플레이어의 스텟
    float attackRange = 2.0f;
    public GameObject DamageText;
    public GameObject clickMarker;
    private GameObject clickMarker_global_variable;
    

   
    public override void Init()
    {
              
        WorldObjectType = Define.WorldObject.Player;

        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent; //Inputmanager에게 키액션이 발생하면 OnMouseClicked 함수를 실행할 것을 요청.
        Managers.Input.MouseAction += OnMouseEvent;

        

        /* 플레이어 체력바 보류 / 게임 전체 인터페이스로 대체 */
        //if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
        //{
        //    Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
        //}
    }
 
    protected override void UpdateMoving()
    {
        
        if (LockTarget != null)
        {

            float distance = (_DesPos - transform.position).magnitude;
            if (distance <= attackRange)
            {
                Destroy(clickMarker_global_variable); //전투중에는 마우스Marker가 뜨지않도록 Destroy 
                State = Define.State.Skill;
                return;
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
           
            /*========================================TODO==============================================================================*/
            float MoveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude); //거리(시간*속력),최솟값,최댓값
            transform.position += dir.normalized * MoveDist; // P = Po + vt(거리)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Building")))
            {               
                transform.position -= dir * 0.2f; //뒤로밀어내기 
                State = Define.State.Idle;
                return;
            }
            /*========================================================================================================================*/       
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
  
    void player_OnHitEvent() //애니메이션 Hit event로 등록되어 있는 함수 
    {
        if (LockTarget != null)
        {

            
            Stat targetStat = LockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat); //나의 스텟을 인자로 넣어서 상대방의 체력을 깎는다.;
       
            int damagenumber = _stat.Attack - targetStat.Defense;

            TextMesh text = DamageText.gameObject.GetComponent<TextMesh>();
            text.text = damagenumber.ToString();

            Instantiate(DamageText, LockTarget.transform.position, Quaternion.identity, LockTarget.transform);


            //TODO : 어빌 업데이트 
            Abillity_Script abillity_script = FindObjectOfType<Abillity_Script>();
            abillity_script.Accumulate_abillity_Func();           
            Debug.Log($"데미지{_stat.Attack}");
            _stat.SetStat(_stat.Level); // 무기 어빌리티가 즉시 적용되도록 스크립트 실행
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


    void player_HitSounds(Define.MouseEvent evt)
    {
        
        Managers.Sound.Play("sword-unsheathe", Define.Sound.Effect);
    
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
                        
                        GameObject go = Instantiate(clickMarker,_DesPos + vector3, Quaternion.identity); //목적지 마우스Marker
                        clickMarker_global_variable = go;
                        go.SetActive(true);
                        Destroy(go, 1.5f);
                        State = Define.State.Moving;
                                                                    
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

  
}


