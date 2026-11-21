using UnityEngine;

public class WaiterEnemy : MonoBehaviour
{
    [Header("Waypoints")]
    public Transform waypointA;
    public Transform waypointB;

    [Header("Movement")]
    public float moveSpeed = 4f;
    public float arrivalRadius = 0.15f;

    [Header("Pause")]
    public float waitTime = 1.5f;

    [Header("Damage")]
    public int damageAmount = 1;

    private bool goingToB = true;
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
        if (waypointA == null || waypointB == null) return;
       
        if (isWaiting)
        {
            rb.linearVelocity = Vector2.zero; 
            waitTimer -= Time.fixedDeltaTime;

            if (waitTimer <= 0f)
            {
                isWaiting = false;
                goingToB = !goingToB; 
            }
            
            return;
        }

        Transform destination = goingToB ? waypointB : waypointA;
        Vector2 dir = ((Vector2)destination.position - (Vector2)transform.position).normalized;
        rb.linearVelocity = dir * moveSpeed;

        if (animator != null) animator.SetBool("IsMoving", false);
        if (animator != null)
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("MoveX", Mathf.Abs(dir.x));
            animator.SetFloat("MoveY", dir.y);
        }
        
        if (dir.x > 0.01f) sr.flipX = false;
        else if (dir.x < -0.01f) sr.flipX = true;

        float dist = Vector2.Distance(transform.position, destination.position);
        if (dist < arrivalRadius)
        {
            isWaiting = true;
            waitTimer = waitTime;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        var dmg = other.gameObject.GetComponent<HealthSystem>();
        if (dmg != null) dmg.TakeDamage(damageAmount);
    }
}