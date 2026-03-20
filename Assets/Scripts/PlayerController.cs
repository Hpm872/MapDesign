using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    Rigidbody2D rb;
    Vector2 moveInput;

    private InputAction moveAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
     
        moveAction = InputSystem.actions["Move"];
    }

    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>().normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}