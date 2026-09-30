using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    
    public Rigidbody2D rigidbody2D;
    public Animator animator;

    private float moveSpeed = 5f;
    private Vector2 movement;
    private bool right = true;
    public int life = 3;
    public int coins = 0;
    private Collider2D whatIsCollider;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        controlMovement();
        controlAnimation();
        controlFlip();
        controlCollision();
    }

    void controlMovement()
    {
        rigidbody2D.linearVelocity = new Vector2(movement.x*moveSpeed, movement.y*moveSpeed);
    }

    void controlAnimation()
    {
        animator.SetFloat("xVelocity", rigidbody2D.linearVelocityX);
        animator.SetFloat("yVelocity", rigidbody2D.linearVelocityY);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void controlFlip()
    {
        if (movement.x > 0 && !right)
        {
            Flip();
        } else if (movement.x < 0 && right)
        {
            Flip();
        }
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        right = !right;
    }

    void controlCollision()
    {
        if (whatIsCollider == null)
            return;

        if (whatIsCollider.CompareTag("Coin"))
        {
            CollectCoin();
        }

        whatIsCollider = null;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        whatIsCollider = collision.collider;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        whatIsCollider = other;
    }

    void CollectCoin()
    {
        coins = coins + 1;
        Destroy(whatIsCollider.gameObject);
    }

    public void TakeDemage()
    {
        life = life - 1;
        if (life < 1)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(rigidbody2D.gameObject);
    }

}
