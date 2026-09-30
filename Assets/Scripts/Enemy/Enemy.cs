using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public Player player;

    public float moveSpeed = 3f;
    public float attackCooldown = 1f;

    private Rigidbody2D rigidbody2D;
    private float cooldownTimer;
    private bool touchingPlayer;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (touchingPlayer && cooldownTimer <= 0)
        {
            Attack();
        }
    }

    void FixedUpdate()
    {
        controlMovement();
    }

    void controlMovement()
    {
        // Se estiver tocando no Player, fica parado
        if (touchingPlayer || cooldownTimer > 0)
        {
            rigidbody2D.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction =
            (player.transform.position - transform.position).normalized;

        rigidbody2D.linearVelocity = direction * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;

            if (cooldownTimer <= 0)
            {
                Attack();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
        }
    }

    void Attack()
    {
        Debug.Log("INIMIGO ATACOU!");

        player.TakeDemage();

        cooldownTimer = attackCooldown;
    }
}