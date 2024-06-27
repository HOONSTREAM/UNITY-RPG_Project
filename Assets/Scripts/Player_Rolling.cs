using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rolling : MonoBehaviour
{
    private const float rollDistance = 3.5f; // ������ �Ÿ�
    private const float rollDuration = 0.2f; // ������ �ð�
    private const float rollCooldown = 3.0f; // ������ ��Ÿ��
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
        cooldownTimer = rollCooldown; // ��Ÿ�� ����

        playerController.State = Define.State.Idle; // ���¸� Idle�� ����

        // Roll �޼��带 ȣ���Ͽ� ��� �̵� ����
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
            playerController.State = Define.State.Idle; // ������ �Ϸ� �� ���¸� Idle�� ����
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
            direction.y = 0; // Y�� ���� ����
            return direction;
        }
        return transform.forward; // ���� Raycast ���� �� �⺻ ���� ����
    }

}
