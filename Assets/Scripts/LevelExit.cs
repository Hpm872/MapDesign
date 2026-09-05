using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || triggered) return;

        triggered = true;

        var rb = other.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        var controller = other.GetComponent<PlayerController>();
        if (controller != null) controller.enabled = false;

        var attack = other.GetComponent<PlayerAttack>();
        if (attack != null) attack.enabled = false;

        if (UIManager.Instance != null) UIManager.Instance.ShowVictory();
    }
}
