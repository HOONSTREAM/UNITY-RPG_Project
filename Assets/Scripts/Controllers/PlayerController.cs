using System.Collections;
using System.Collections.Generic;
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
    bool _stopSkill = false; // ��ų��� ���� ����
    PlayerStat _stat; // �÷��̾��� ����
    float attackRange = 2.0f;
    




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
                Debug.Log("���̻� ������ �� ����.");
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
    void OnHitEvent() //�ִϸ��̼� Hit event�� ��ϵǾ� �ִ� �Լ� 
    {
        if (LockTarget != null)
        {
            Stat targetStat = LockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat); //���� ������ ���ڷ� �־ ������ ü���� ��´�.;

        }

        if (_stopSkill)
        {
            State = Define.State.Idle;
        }
        else
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
                    if(evt == Define.MouseEvent.PointerUp)
                    {
                        _stopSkill = true;

                    }
                }
                break;

        }

     }
    void HitSounds(Define.MouseEvent evt)
    {
        Managers.Sound.Play("Fight Hits 24", Define.Sound.Effect);
        //Managers.Sound.Play("univ0001", Define.Sound.Effect);

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
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                        {
                            LockTarget = hit.collider.gameObject;
                            Debug.Log("���� Ŭ��");
                        }                     

                        else
                        {
                            LockTarget = null;
                            Debug.Log("�׶��� Ŭ��");
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
                    _stopSkill = true;
                }
                
                break;

        }

    }

  
}


