using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour//, IDamageable
{
    [Header("Public Variables")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Events")]
    public UnityEvent<int> onHealthChanged;
    public UnityEvent onDeath;

    [Header("Invincibility")]
    public float invincibleTime = 1f;
    private float invincibleTimer = 0f;
    public bool isInvincible => invincibleTimer > 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        onHealthChanged?.Invoke(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
