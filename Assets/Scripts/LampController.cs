using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LampController : MonoBehaviour {
    public Light2D playerLight;

    [Header("Parpadeo")]
    public float baseIntensity = 1f;
    public float flickerAmount = 0.12f;
    public float flickerSpeed = 3f;

    void Update()
    {
        if (playerLight == null) return;
        
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);
        playerLight.intensity = baseIntensity + (noise - 0.5f) * flickerAmount;
    }
}