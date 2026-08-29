using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    [Header("Damage")]
    public int damage = 5;

    [Header("Attack Timer")]
    public float activeTime = 0.15f;

    public float distanceFromPlayer = 0.5f;

    private CircleCollider2D hitCollider;
    private HashSet<HealthSystem> alreadyHit = new HashSet<HealthSystem>();

    void Awake()
    {
        hitCollider = GetComponent<CircleCollider2D>();
        hitCollider.enabled = false;
    }

    public void Aim(Vector2 direction)
    {
        direction.Normalize();

        transform.localPosition = direction * distanceFromPlayer;
        float angle = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        Swing();
    }

    public void Swing()
    {
        StopAllCoroutines();
        StartCoroutine(SwingRoutine());
    }

    private IEnumerator SwingRoutine() 
    {
        alreadyHit.Clear();
        hitCollider.enabled = true;

        yield return new WaitForSeconds(activeTime);

        hitCollider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        HealthSystem enemyHealth = other.GetComponent<HealthSystem>();

        if (enemyHealth == null || alreadyHit.Contains(enemyHealth)) return;
        alreadyHit.Add(enemyHealth);

        enemyHealth.TakeDamage(damage);
        Debug.Log("Enemigo ha sido golpeado");
    }

    void OnDrawGizmosSelected()
    {
        if (hitCollider == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, hitCollider.radius);
    }
}
