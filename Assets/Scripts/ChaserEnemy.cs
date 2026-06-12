using UnityEngine;

public class ChaserEnemy : MonoBehaviour
{
    // ------ Variables Publicas ---------
    // Detección
    public float detectionRadius = 5f;
    public float loseRadius = 8f;
    public LayerMask playerLayer;

    // Movimiento
    public float chaseSpeed = 4f;
    public float returnSpeed = 2f;

    // Daño
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
    void Update()
    {
        
    }
}
