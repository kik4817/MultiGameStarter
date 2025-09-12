using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMove : NetworkBehaviour
{
    Rigidbody2D rigidbody2;
    PlayerAction playerAction;
    Animator animator;

    public const string ChangeHasName = "IsChange";

    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAction = new PlayerAction();
        playerAction.Player.Enable();
    }

    void Update()
    {
        // 내가 소유건을 가지고 있을때만 움직여라
        if (!IsOwner) { return; }
        //if(IsServer) { }
        //if(IsHost) { }

        Move();
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        if (Keyboard.current.rKey.isPressed)
        {
            animator.SetBool(ChangeHasName, true);
        }
        else if(Keyboard.current.tKey.isPressed)
        {
            animator.SetBool(ChangeHasName, false);
        }
    }

    private void Move()
    {
        Vector2 moveDir = playerAction.Player.Move.ReadValue<Vector2>(); // 방향 Vector

        rigidbody2.linearVelocity = moveDir * moveSpeed;
    }
}
