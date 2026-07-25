using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    private enum State {Patrol, Wait, Chase, Return}
    private State currentState = State.Wait;

    [Header("Route")]
    public Transform [] waypoints;
    private int currentIndex = 0;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float chaseSpeed = 4.5f;
    public float returnSpeed = 2.5f;
    public float arrivalRadius = 0.15f;

    [Header("Wait")]
    public float waitTime = 2f;
    private float waitTimer = 0f;

    [Header("Detection")]
    public float detectionRadius = 6f;
    public float loseRadius = 10f;
    public LayerMask playerLayer;

    [Header("Damage")]
    public int damageAmount = 1;

    [Header("Wall Detection")]
    public float detectionDistance = 0.5f;
    public LayerMask wallLayer;

    [Header("Internal Components")]
    private Vector2 originPosition;
    private Transform playerTarget;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        originPosition = transform.position;
    }

    void FixedUpdate()
    {
        CheckForPlayer();

        switch (currentState)
        {
            case State.Patrol: TickPatrol(); break;
            case State.Wait: TickWait(); break;
            case State.Chase: TickChase(); break;
            case State.Return: TickReturn(); break;
        }
    }

    void CheckForPlayer()
    {
        if (currentState == State.Chase) return;
        
        Collider2D hit = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        if (hit != null)
        {
            playerTarget = hit.transform;
            currentState = State.Chase;
        }
    }

    void TickPatrol()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        MoveTowards(waypoints[currentIndex].position, moveSpeed);

        float dist = Vector2.Distance(transform.position, waypoints[currentIndex].position);
        if (dist < arrivalRadius)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
            rb.linearVelocity = Vector2.zero;
            waitTimer = waitTime;
            currentState = State.Wait;
        }
    }

    void TickWait()
    {
        rb.linearVelocity = Vector2.zero;

        waitTimer -= Time.fixedDeltaTime;
        if (waitTimer <= 0f) currentState = State.Patrol;
    }

    void TickChase()
    {
        if (playerTarget == null) currentState = State.Return;

        float dist = Vector2.Distance(transform.position, playerTarget.position);
        if (dist > loseRadius)
        {
            playerTarget = null;
            currentState = State.Return;
            return;
        }
        
        MoveTowards(playerTarget.position, chaseSpeed);
    }

    void TickReturn()
    {
        float dist = Vector2.Distance(transform.position, originPosition);
        if (dist < 0.1f)
        {
            rb.linearVelocity = Vector2.zero;
            currentState = State.Wait;
            return;
        }

        MoveTowards(originPosition, returnSpeed);
    }
    
    void MoveTowards(Vector2 target, float speed)
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * speed;

        if (dir.x > 0.01f) sr.flipX = false;
        if (dir.x < -0.01f) sr.flipX = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var dmg = other.GetComponent<IDamageable>(); // Cambiar IDamageable por PlayerHealth luego
        if (dmg != null) dmg.TakeDamage(damageAmount);
    }

    bool IsWallNear()
    {
        Vector2 facingDirection = sr.flipX ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDirection, detectionDistance, wallLayer);

        Debug.DrawRay(transform.position, facingDirection * detectionDistance, hit.collider != null ? Color.green : Color.red);

        return hit.collider != null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, loseRadius);
    }
}