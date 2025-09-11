using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigidbody2;
    PlayerAction playerAction;

    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();

        playerAction = new PlayerAction();
        playerAction.Player.Enable();
    }

    void Update()
    {
        Vector2 moveDir = playerAction.Player.Move.ReadValue<Vector2>(); // πÊ«‚ Vector
        
        rigidbody2.linearVelocity = moveDir * moveSpeed;
    }
}
