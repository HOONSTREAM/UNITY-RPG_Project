using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rolling : MonoBehaviour
{
    private const float rollDistance = 3.5f; // ������ �Ÿ�
    private const float rollDuration = 0.2f; // ������ �ð�
    private bool isRolling = false;
    private Vector3 rollDirection;
    private float rollTimer;

    private Animator animator;
    private CharacterController characterController;
    private PlayerController playerController;

    public bool IsRolling => isRolling; // ������ ���¸� �ܺο��� ������ �� �ֵ��� ����

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


}
