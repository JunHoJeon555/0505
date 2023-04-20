using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// �ȴ� �ӵ�
    /// </summary>
    public float walkSpeed = 3.0f;

    /// <summary>
    /// �ٴ� �ӵ�
    /// </summary>
    public float runSpeed = 5.0f;

    /// <summary>
    /// ���� �ӵ�(�Ȱų� �ٴ� �� �� �ϳ�)
    /// </summary>
    float currentSpeed = 5.0f;

    /// <summary>
    /// �̵� ���� ǥ�ÿ� enum
    /// </summary>
    enum MoveMode
    {
        Walk = 0,
        Run
    }

    /// <summary>
    /// ���� �̵� ����
    /// </summary>
    MoveMode moveMode = MoveMode.Run;
    MoveMode Move_Mode
    {
        get => moveMode;
        set
        {
           moveMode = value;
            switch(moveMode)
            {
                case MoveMode.Walk:
                    currentSpeed = walkSpeed;               //�ӵ� ����
                    animator.SetFloat("Speed", 0.3f);
                    if(inputDir != Vector3.zero)            //�̵� �߿� ����Ǿ�����
                    {
                        animator.SetFloat("Speed", 0.3f);   // �ִϸ��̼� �Ķ���͵� �����ؼ� �ִ�
                    }
                    break;
                case MoveMode.Run:
                    currentSpeed = runSpeed;
                    if (inputDir != Vector3.zero)
                    {
                        animator.SetFloat("Speed", 1f);
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// �Էµ� �̵� ����
    /// </summary>
    Vector3 inputDir = Vector3.zero;

    /// <summary>
    /// ȸ���ӵ�
    /// </summary>
    public float turnSpedd = 10.0f;
    
    /// <summary>
    /// ���������� �ٶ� ȸ��
    /// </summary>
    Quaternion targetRotation = Quaternion.identity;

    /// <summary>
    /// ��ǲ �׼� �ν��Ͻ�
    /// </summary>
    PlayerInputActions inputActions;

    // ������Ʈ��
    Animator animator;
    CharacterController controller;

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
    }

    private void OnDisable()
    {
        inputActions.Player.MoveModeChange.performed -= OnMoveModeChange;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        controller.Move(Time.deltaTime * currentSpeed * inputDir);  // �� �� �������� �����̴� ����
        //controller.SimpleMove(currentSpeed * inputDir);   // �����ϰ� �����̱�(�߷°��� �͵� �˾Ƽ� ó��)

        //transform.rotation���� targetRotation���� �ʴ� 1/turnSpeed�� ȸ�� 
        transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,Time.deltaTime *turnSpedd);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        inputDir.x = input.x;
        inputDir.y = 0.0f;
        inputDir.z = input.y;

        if (!context.canceled)
        {
            // �̵� �Է��� ���Դ�.

            //ī�޶��� y�� ȸ���� ���� �̾Ƴ���
            Quaternion cameraYRotation = Quaternion.Euler(0,Camera.main.transform.rotation.eulerAngles.y,0);
            inputDir = cameraYRotation * inputDir;     //ī�޶� y ȸ���� �Է¹��⿡ ���ؼ� ���� ȸ����Ű��
            
            targetRotation = Quaternion.LookRotation(inputDir); //ȸ�� �� inputDir �������� ����ȸ�� �����


            animator.SetFloat("Speed", 1.0f);

            switch (Move_Mode)  //Move_Mode�� ��
            {
                case MoveMode.Walk:
                    animator.SetFloat("Speed" ,0.3f);
                    break;
                case MoveMode.Run:
                    animator.SetFloat("Speed", 1f);
                    break;
                default:
                    animator.SetFloat("Speed", 0f);
                    break;

            }
            inputDir.y = -2;

        }
        else
        {
            // �̵� �Է��� ������.
            animator.SetFloat("Speed", 0.0f);
        }
       

    }

    private void OnMoveModeChange(InputAction.CallbackContext context)
    {
        //Shift�� �������� �̵���� ����
        if(Move_Mode == MoveMode.Walk)
        {
            Move_Mode = MoveMode.Run;
        }
        else
        {
            Move_Mode = MoveMode.Walk;
            
        }
        
      
        
        

    }


    //1. �̵������� �ٶ󺸰� �����
    //2. ����Ƽ Ű�� ������ �̵���尡 ����ǰ� �����

}
