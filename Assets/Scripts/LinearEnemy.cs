using UnityEngine;

public class LinearEnemy : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform waypointA;
    public Transform waypointB;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float arrivalRadius = 0.15f;

    [Header("Damage")]
    public int damageAmount = 1;

    private bool goingToB = true;
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (waypointA == null || waypointB == null) return;

        Transform destination = goingToB ? waypointB : waypointA;
        Vector2 dir = ((Vector2)destination.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;

        if (dir.x > 0.01f)  sr.flipX = false;
        if (dir.x < -0.01f) sr.flipX = true;

        float dist = Vector2.Distance(transform.position, destination.position);
        if (dist < arrivalRadius) goingToB = !goingToB;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var dmg = other.GetComponent<IDamageable>();
        if (dmg != null) dmg.TakeDamage(damageAmount);
    }
}