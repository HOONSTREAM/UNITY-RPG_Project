
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Base_Monster_Controller : MonoBehaviour
{
    [SerializeField]
    protected Vector3 _DesPos; //목적지 포지션
    [SerializeField]
    protected Define.Monster_State _state = Define.Monster_State.Idle;
    [SerializeField]
    protected GameObject LockTarget; //타겟팅 락온 된 몬스터 변수

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    public virtual Define.Monster_State State
    {
        get { return _state; }
        set
        {
            _state = value;
            Animator anim = GetComponent<Animator>();

            switch (_state)
            {
                case Define.Monster_State.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    break;
                case Define.Monster_State.Moving:
                    anim.CrossFade("RUN", 0.1f);
                    break;
                case Define.Monster_State.Skill:
                    anim.CrossFade("ATTACK", 0.1f, -1, 0);
                    break;
                case Define.Monster_State.Die:
                    anim.CrossFade("Die", 0.1f);
                    break;
                case Define.Monster_State.King_Slime_Skill:
                    anim.CrossFade("King_Slime_Skill", 0.1f, -1, 0);
                    break;

            }
        }
    }
    private void Start()
    {
        Init();
    }
    void Update()
    {

        switch (State)
        {
            case Define.Monster_State.Die:
                UpdateDie();
                break;
            case Define.Monster_State.Moving:
                UpdateMoving();
                break;
            case Define.Monster_State.Idle:
                UpdateIdle();
                break;
            case Define.Monster_State.Skill:
                UpdateSkill();
                break;
            case Define.Monster_State.King_Slime_Skill:
                Update_King_Slime_Skill();
                break;

        }


    }

    public abstract void Init();
    protected virtual void UpdateDie()
    {

    }
    protected virtual void UpdateMoving()
    {

    }
    protected virtual void UpdateIdle()
    {

    }
    protected virtual void UpdateSkill()
    {

    }

    protected virtual void Update_King_Slime_Skill()
    {

    }


}


