using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rolling : MonoBehaviour
{
    private const float rollDistance = 3.5f; // 구르기 거리
    private const float rollDuration = 0.2f; // 구르기 시간
    private bool isRolling = false;
    private Vector3 rollDirection;
    private float rollTimer;

    private Animator animator;
    private CharacterController characterController;
    private PlayerController playerController;

    public bool IsRolling => isRolling; // 구르기 상태를 외부에서 접근할 수 있도록 공개

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isRolling)
        {
            StartRoll();
        }

        if (isRolling)
        {
            Roll();
        }
    }

    void StartRoll()
    {

        isRolling = true;
        rollDirection = transform.forward;
        rollTimer = rollDuration;

        playerController.State = Define.State.Idle; // 상태를 Idle로 설정

        // Roll 메서드를 호출하여 즉시 이동 시작
        Roll();
    }

    void Roll()
    {
        if (!isRolling) return;

        rollTimer -= Time.deltaTime;
        float rollSpeed = rollDistance / rollDuration;

        Vector3 move = rollDirection * rollSpeed * Time.deltaTime;
        characterController.Move(move);

        if (rollTimer <= 0)
        {
            isRolling = false;
            playerController.State = Define.State.Idle; // 구르기 완료 후 상태를 Idle로 변경
        }
    }


}
