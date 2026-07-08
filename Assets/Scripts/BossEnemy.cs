using System;
using UnityEngine;
using UnityEngine.Rendering;

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

    // Update is called once per frame
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
}
