using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 걷는 속도
    /// </summary>
    public float walkSpeed = 3.0f;

    /// <summary>
    /// 뛰는 속도
    /// </summary>
    public float runSpeed = 5.0f;

    /// <summary>
    /// 현재 속도(걷거나 뛰는 것 중 하나)
    /// </summary>
    float currentSpeed = 5.0f;

    /// <summary>
    /// 이동 상태 표시용 enum
    /// </summary>
    enum MoveMode
    {
        Walk = 0,
        Run
    }

    /// <summary>
    /// 현재 이동 상태
    /// </summary>
    MoveMode moveMode = MoveMode.Run;
    MoveMode Move_Mode
    {
        get => moveMode;
        set
        {
            moveMode = value;
            switch (moveMode)
            {
                case MoveMode.Walk:
                    currentSpeed = walkSpeed;               // 속도 변경
                    if (inputDir != Vector3.zero)          // 이동 중에 변경되었으면
                    {
                        animator.SetFloat("Speed", 0.3f);   // 애니메이션 파라메터도 변경해서 애니메이션 변경
                    }
                    break;
                case MoveMode.Run:
                    currentSpeed = runSpeed;
                    if (inputDir != Vector3.zero)
                    {
                        animator.SetFloat("Speed", 1.0f);
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// 입력된 이동 방향
    /// </summary>
    Vector3 inputDir = Vector3.zero;

    /// <summary>
    /// 회전 속도
    /// </summary>
    public float turnSpeed = 10.0f;

    /// <summary>
    /// 최종적으로 바라볼 회전
    /// </summary>
    Quaternion targetRotation = Quaternion.identity;

    /// <summary>
    /// 인풋 액션 인스턴스
    /// </summary>
    PlayerInputActions inputActions;

    // 컴포넌트들
    Animator animator;
    CharacterController controller;

    readonly int AttackHash = Animator.StringToHash("Attack");
    readonly int SpeedHash = Animator.StringToHash("Speed");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.MoveModeChange.performed += OnMoveModeChange;
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.MoveModeChange.performed -= OnMoveModeChange;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        controller.Move(Time.deltaTime * currentSpeed * inputDir);  // 좀 더 수동으로 움직이는 느낌
        //controller.SimpleMove(currentSpeed * inputDir);   // 간단하게 움직이기(중력같은 것도 알아서 처리)

        // transform.rotation에서 targetRotation까지 초당 1/turnSpeed씩 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        inputDir.x = input.x;
        inputDir.y = 0.0f;
        inputDir.z = input.y;

        if (!context.canceled)
        {
            // 이동 입력이 들어왔다.

            // 카메라의 Y축 회전만 따로 뽑아내기
            Quaternion cameraYRotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
            inputDir = cameraYRotation * inputDir;  // 카메라 Y 회전을 입력방향에 곱해서 같이 회전 시키기

            targetRotation = Quaternion.LookRotation(inputDir); // 회전 된 inputDir 기준으로 최종 회전 만들기

            switch (Move_Mode)  // MoveMode에 따라 애니메이션 변경
            {
                case MoveMode.Walk:
                    animator.SetFloat(SpeedHash, 0.3f);
                    break;
                case MoveMode.Run:
                    animator.SetFloat(SpeedHash, 1.0f);
                    break;
                default:
                    animator.SetFloat(SpeedHash, 0.0f);
                    break;
            }

            inputDir.y = -2.0f; // 강제로 바닥으로 내리기
        }
        else
        {
            // 이동 입력이 끝났다.
            animator.SetFloat(SpeedHash, 0.0f);
        }
    }

    private void OnMoveModeChange(InputAction.CallbackContext _)
    {
        // Shift가 눌러지면 이동 모드 변경하기
        if (Move_Mode == MoveMode.Walk)
        {
            Move_Mode = MoveMode.Run;
        }
        else
        {
            Move_Mode = MoveMode.Walk;
        }
    }

    private void OnAttack(InputAction.CallbackContext _)
    {
        animator.SetTrigger(AttackHash);
    }

}
