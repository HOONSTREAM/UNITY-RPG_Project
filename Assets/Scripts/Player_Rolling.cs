using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rolling : MonoBehaviour
{
    private const float rollDistance = 3.5f; // 구르기 거리
    private const float rollDuration = 0.2f; // 구르기 시간
    private const float rollCooldown = 3.0f; // 구르기 쿨타임
    private bool isRolling = false;
    private Vector3 rollDirection;
    private float rollTimer;
    private float cooldownTimer;

    private Animator animator;
    private CharacterController characterController;
    private PlayerController playerController;

    
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.C) && !isRolling && cooldownTimer <= 0)
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
        rollDirection = GetMouseDirection();
        rollTimer = rollDuration;
        cooldownTimer = rollCooldown; // 쿨타임 시작

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

    Vector3 GetMouseDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPosition = hit.point;
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0; // Y축 방향 제거
            return direction;
        }
        return transform.forward; // 만약 Raycast 실패 시 기본 방향 유지
    }

}
