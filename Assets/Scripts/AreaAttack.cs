using System;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    [Header("Damage")]
    public int damage = 5;

    [Header("Attack Timer")]
    public float activeTime = 0.15f;

    public float distanceFromPlayer = 0.5f;

    private Collider2D hitCollider;
    private HashSet<HealthSystem> alreadyHit = new HashSet<HealthSystem>();

    void Awake()
    {
        hitCollider = GetComponent<Collider2D>();
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
