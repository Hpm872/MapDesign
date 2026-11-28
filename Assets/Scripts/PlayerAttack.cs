using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public AreaAttack attack;
    public Camera gameCamera;

    private InputSystem_Actions inputActions;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();

        inputActions = new InputSystem_Actions();

        if (gameCamera == null) gameCamera = Camera.main;
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Attack.performed += OnAttackPerformed;
    }

    void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttackPerformed;
        inputActions.Player.Disable();
    }

    void OnAttackPerformed(InputAction.CallbackContext ctx)
    {
        if (attack == null || gameCamera == null) return;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = gameCamera.ScreenToWorldPoint(mouseScreenPos);

        Vector2 direction = (Vector2)mouseWorldPos - (Vector2)transform.position;
        attack.Aim(direction);

        Debug.Log("Atacando! Animator  " + animator);
        if (animator != null)
        {
            Vector2 dir = direction.normalized;
            animator.SetFloat("MoveX", dir.x);
            animator.SetFloat("MoveY", dir.y);
            animator.SetTrigger("Attacking");
        }
    }
}
