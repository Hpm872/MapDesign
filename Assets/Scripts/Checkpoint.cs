using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector3 lastCheckpointPosition;
    public static bool hasCheckpoint = false;

    [Header("Visual")]
    public SpriteRenderer spritei;
    public Color activatedColor = Color.white;

    private bool activated = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;
            lastCheckpointPosition = transform.position;
            hasCheckpoint = true;

            if (spritei != null)
            {
                spritei.color = activatedColor;
                Debug.Log("Checkpoint guardado: " + transform.position);
            }
        }
    }
}
