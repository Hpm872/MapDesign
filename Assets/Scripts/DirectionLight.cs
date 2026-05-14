using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;

public class DirectionLight : MonoBehaviour
{
    public Light2D spotlight;
    public Transform playerTransform;
    private Vector2 lastDirection = Vector2.right;
    private InputSystem_Actions inputActions;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (spotlight == null || playerTransform == null) return;

        Vector2 move = inputActions.Player.Move.ReadValue<Vector2>();

        if (move.sqrMagnitude > 0.01f) lastDirection = move.normalized;
        
        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
        spotlight.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }
}
