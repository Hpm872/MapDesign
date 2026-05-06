using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class LampController : MonoBehaviour {
    public Light2D playerLight;

    [Header("Parpadeo")]
    public float baseIntensity = 1f;
    public float flickerAmount = 0.12f;
    public float flickerSpeed = 3f;
    private InputSystem_Actions inputActions;
    public bool lampOn = true;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Flashlight.performed += OnToggle;
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Flashlight.performed -= OnToggle;
    }

    void OnToggle(InputAction.CallbackContext ctx)
    {
        lampOn = !lampOn;
        if (playerLight != null)
        {
            playerLight.enabled = lampOn;
        }
    }

    void Update()
    {
        if (playerLight == null || !lampOn) return;
        
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);
        playerLight.intensity = baseIntensity + (noise - 0.5f) * flickerAmount;
    }
}