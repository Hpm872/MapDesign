using UnityEngine;

public class RoadEnemy : MonoBehaviour
{
    // ------ Variables Publicas ---------
    // Ruta
    public Transform [] waypoints;

    // Movimiento
    public float moveSpeed = 4f;
    public float arrivalRadius = 0.15f;
    public float waitTime = 1.5f;

    // Daño
    public int damageAmount = 1;

    // ------ Variables Privadas ---------
    private int currentIndex = 0;
    private float waitTimer = 0f;
    private bool isWaiting = false;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        if (isWaiting)
        {
            rb.linearVelocity = Vector2.zero;
            waitTimer -= Time.fixedDeltaTime;

            if (waitTimer <= 0f) isWaiting = false;
            return;
        }

        Transform destination = waypoints[currentIndex];
        Vector2 dir = ((Vector2)destination.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;

        float dist = Vector2.Distance(transform.position, destination.position);
        if (dist < arrivalRadius)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
            if (waitTime > 0f)
            {
                isWaiting = true;
                waitTimer = waitTime;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var dmg = other.GetComponent<IDamageable>();
        if (dmg != null) dmg.TakeDamage(damageAmount);
    }
}
