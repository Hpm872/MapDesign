using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour, IDamageable
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
        if (invincibleTimer > 0f) invincibleTimer -= Time.deltaTime;
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);

        invincibleTimer = invincibleTime;
        onHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0) onDeath?.Invoke();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(maxHealth, currentHealth);

        onHealthChanged?.Invoke(currentHealth);
    }
}
