using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goon : MonoBehaviour
{
    public float moveSpeed = 10f;  
    public float superGoonSpeed = 20f; 
    public LayerMask wallLayer;
    public int hitCount = 2;
    public float deathForceMagnitude = 9999f;

    private bool movingRight = true;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public bool IsSuper { get; private set; } = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        rb.velocity = new Vector2((movingRight ? 1 : -1) * moveSpeed, 0);

        if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y)
        {
            //implement later
        }
    }

    public void SetSuperGoon()
    {
        if (!IsSuper)
        {
            moveSpeed = superGoonSpeed;  
            spriteRenderer.color = Color.red;  
            IsSuper = true;
        }
    }

    public void SetMovingDirection(bool isMovingRight)
    {
        movingRight = isMovingRight;

        Vector3 theScale = transform.localScale;
        theScale.x = Mathf.Abs(theScale.x) * (isMovingRight ? 1 : -1);
        transform.localScale = theScale;
    }

   void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.CompareTag("MysteryBox") || collision.gameObject.CompareTag("Player"))
    {
        Flip();
    }
}



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            hitCount--;
            if (hitCount <= 0)
            {
                Die();
            }
            Destroy(collision.gameObject);
        }
    }

    void Flip()
    {
        movingRight = !movingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(new Vector2(Random.Range(-1f, 1f), 1).normalized * deathForceMagnitude);
        rb.AddTorque(Random.Range(-deathForceMagnitude, deathForceMagnitude));
        Destroy(gameObject, 1f);
    }
}



