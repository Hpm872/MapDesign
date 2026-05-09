using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DirectionLight : MonoBehaviour
{
    public Light2D spotlight;
    public Transform playerTransform;
    private float lastDirectionX = 1f;
    private float lastDirectionY = 1f;

    // Update is called once per frame
    void Update()
    {
        if (spotlight == null || playerTransform == null) return;

        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0.01f) lastDirectionX = 1f;
        if (h < -0.01f) lastDirectionX = -1f;

        float v = Input.GetAxisRaw("Vertical");
        if (v > 0.01f) lastDirectionY = 1f;
        if (v < -0.01f) lastDirectionY = -1f;

        float targetAngle = (lastDirectionX > 0f)? -90f:90f;
        spotlight.transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
    }
}
