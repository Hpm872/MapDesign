using NUnit.Framework;
using UnityEngine;

public class ChaserEnemy : MonoBehaviour
{
    // ------ Variables Publicas ---------
    [Header("Detection")]
    public float detectionRadius = 5f;
    public float loseRadius = 8f;
    public LayerMask playerLayer;

    [Header("Movement")]
    public float chaseSpeed = 4f;
    public float returnSpeed = 2f;

    [Header("Damage")]
    public int damageAmount = 1;

    // ------ Variables Privadas ---------
    private Vector2 originPosition;
    private Transform playerTarget;
    private bool isChasing = false;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        originPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DetectPlayer();

        if (isChasing) ChasePlayer(); else ReturnToOrigin();
    }

    private void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        if (hit != null)
        {
            playerTarget = hit.transform;
            isChasing = true;
        } 
        else if (isChasing) 
        {
            float d = Vector2.Distance(transform.position, playerTarget.position);
            if (d > loseRadius)
            {
                playerTarget = null;
                isChasing = false;
            }
        }
    }

    private void ChasePlayer()
    {
        if (playerTarget == null) return;

        Vector2 dir = ((Vector2)playerTarget.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * chaseSpeed;

        if (dir.x > 0.01f) sr.flipX = false;
        if (dir.x < -0.01f) sr.flipX = true;
    }

    private void ReturnToOrigin()
    {
        float dist = Vector2.Distance(transform.position, originPosition);
        if (dist < 0.01f)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 dir = (originPosition - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * returnSpeed;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var dmg = other.GetComponent<IDamageable>();
        if (dmg != null) dmg.TakeDamage(damageAmount);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, loseRadius);
    }
}
