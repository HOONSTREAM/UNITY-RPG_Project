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

//���� : https://geojun.tistory.com/64

public class PlayerController : BaseController
{

    private int _mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster); //���̾��ũ
    bool skill_is_stop = false; // ��ų��� ���� ����
    PlayerStat _stat; // �÷��̾��� ����
    float attackRange = 2.0f;
    public GameObject DamageText;
    public GameObject clickMarker;
    private GameObject clickMarker_global_variable;
    

   
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
                Destroy(clickMarker_global_variable); //�����߿��� ���콺Marker�� �����ʵ��� Destroy 
                State = Define.State.Skill;
                return;
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
           
            /*========================================TODO==============================================================================*/
            float MoveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude); //�Ÿ�(�ð�*�ӷ�),�ּڰ�,�ִ�
            transform.position += dir.normalized * MoveDist; // P = Po + vt(�Ÿ�)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Building")))
            {               
                transform.position -= dir * 0.2f; //�ڷιо�� 
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
       
            int damagenumber = _stat.Attack - targetStat.Defense;

            TextMesh text = DamageText.gameObject.GetComponent<TextMesh>();
            text.text = damagenumber.ToString();

            Instantiate(DamageText, LockTarget.transform.position, Quaternion.identity, LockTarget.transform);


            //TODO : ��� ������Ʈ 
            Abillity_Script abillity_script = FindObjectOfType<Abillity_Script>();
            abillity_script.Accumulate_abillity_Func();           
            Debug.Log($"������{_stat.Attack}");
            _stat.SetStat(_stat.Level); // ���� �����Ƽ�� ��� ����ǵ��� ��ũ��Ʈ ����
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
                        
                        GameObject go = Instantiate(clickMarker,_DesPos + vector3, Quaternion.identity); //������ ���콺Marker
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


