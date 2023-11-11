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

//���� : https://geojun.tistory.com/64

public class PlayerController : BaseController
{


    private int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster); //���̾��ũ
    bool skill_is_stop = false; // ��ų��� ���� ����
    PlayerStat _stat; // �÷��̾��� ����
    float attackRange = 2.0f;
    public GameObject DamageText;


  

    public override void Init()
    {
       

        WorldObjectType = Define.WorldObject.Player;

        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent; //Inputmanager���� Ű�׼��� �߻��ϸ� OnMouseClicked �Լ��� ������ ���� ��û.
        Managers.Input.MouseAction += OnMouseEvent;
        
        /* �÷��̾� ü�¹� ���� / ���� ��ü �������̽��� ��ü */ 
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
               
                State = Define.State.Skill;
                return;
            }
        }

        Vector3 dir = _DesPos - transform.position; // ������ �����ǿ��� �÷��̾��� �������� ���� ������ ���⺤�Ͱ� ���´�.
        dir.y = 0;

        if (dir.magnitude < 0.1f) // ������ ��Į���� 0�� �����ϸ� (�������� ����������)
        {
            State = Define.State.Idle;
           
        }

        else
        {
           
            /*========================================TODO==============================================================================*/
            float MoveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude); //�Ÿ�(�ð�*�ӷ�),�ּڰ�,�ִ�
            transform.position += dir.normalized * MoveDist; // P = Po + vt(�Ÿ�)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Building")))
            {
               
                transform.position += new Vector3(0, 0, -0.5f); //�ڷιо�� 
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
  
    void player_OnHitEvent() //�ִϸ��̼� Hit event�� ��ϵǾ� �ִ� �Լ� 
    {
        if (LockTarget != null)
        {
            
            
            Stat targetStat = LockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat); //���� ������ ���ڷ� �־ ������ ü���� ��´�.;

            Debug.Log($"������ :{_stat.Attack - targetStat.Defense} ");
            int damagenumber = _stat.Attack - targetStat.Defense;

            

            TextMesh text = DamageText.gameObject.GetComponent<TextMesh>();
            text.text = damagenumber.ToString();

            Instantiate(DamageText, LockTarget.transform.position, Quaternion.identity, LockTarget.transform);


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
        Managers.Sound.Play("Fight Hits 24", Define.Sound.Effect);
        
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
                        _DesPos = hit.point;

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


