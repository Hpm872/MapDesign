using System;
using UnityEngine;

public class RoadEnemy : MonoBehaviour
{
    // ------ Variables Publicas ---------
    [Header("Route")]
    public Transform [] waypoints;
    public Transform spriteTransform;
    public float rotationOffset = 90f;

    [Header("Movement")]
    public float moveSpeed = 4f;
    public float arrivalRadius = 0.15f;
    public float waitTime = 1.5f;

    [Header("Damage")]
    public int damageAmount = 1;

    // ------ Variables Privadas ---------
    private int currentIndex = 0;
    private float waitTimer = 0f;
    private bool isWaiting = false;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        if (isWaiting)
        {
            rb.linearVelocity = Vector2.zero;

            if (animator != null) animator.SetBool("IsMoving", false);

            waitTimer -= Time.fixedDeltaTime;

            if (waitTimer <= 0f) isWaiting = false;
            return;
        }

        Transform destination = waypoints[currentIndex];

        if (destination == null) return;

        Vector2 dir = ((Vector2)destination.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;

        if (animator != null)
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("MoveX", dir.x);
            animator.SetFloat("MoveY", dir.y);
        }

        sr.flipX = dir.x < 0f;

        // if (spriteTransform != null && dir.sqrMagnitude > 0.01f)
        // {
        //     float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + rotationOffset;
        //     spriteTransform.rotation = Quaternion.Euler(0f, 0f, angle);
        // }

        float dist = Vector2.Distance(transform.position, destination.position);
        if (dist < arrivalRadius)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
            if (waitTime > 0f)
            {
                isWaiting = true;
                waitTimer = waitTime;

                // rb.linearVelocity = Vector2.zero;
                // if (animator != null) animator.SetBool("IsMoving", false);
            }
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        var dmg = other.gameObject.GetComponent<HealthSystem>();
        if (dmg != null) dmg.TakeDamage(damageAmount);
    }
}