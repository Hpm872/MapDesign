using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int damageAmount = 1;
    public string targetTag = "Player";

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            HealthSystem health = collision.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
                Debug.Log("Target successfully took damage: " + damageAmount);
            }
        }
    }
}
