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

    // [Header("Damage")]
    // public int damageAmount = 1;

    private bool goingToB = true;
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
        }

        Transform destination = goingToB ? waypointB : waypointA;
        Vector2 dir = ((Vector2)destination.position * (Vector2)transform.position).normalized;
        // rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
        rb.linearVelocity = dir * moveSpeed;

        float dist = Vector2.Distance(transform.position, destination.position);
        if (dist < arrivalRadius)
        {
            isWaiting = true;
            waitTimer = waitTime;
        }
    }
}