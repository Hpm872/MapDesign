using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public float destroyDelay = 0f;

    private HealthSystem healthSystem;
    private bool isDead = false;

    void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    void OnEnable()
    {
        if (healthSystem != null) healthSystem.onDeath.AddListener(HandleDeath);
    }

    void OnDisable()
    {
        if (healthSystem != null) healthSystem.onDeath.RemoveListener(HandleDeath);
    }

    void HandleDeath()
    {
        if (isDead) return;

        isDead = true;
        Destroy(gameObject, destroyDelay);
    }
}
