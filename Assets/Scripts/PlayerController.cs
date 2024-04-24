using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 15f;
    public float jumpForce = 27f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private Animator animator; 
    private float movementInput = 0f; 
    public bool isMoving;
    private Gun currentGun;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
        rb.freezeRotation = true;
    }

    void Update()
{
    isMoving = Mathf.Abs(movementInput) > Mathf.Epsilon;
    
    if (animator)
    {
        animator.SetBool("isMoving", isMoving);
    }

    Move(movementInput);
    
    //potential jumping logic
}

    public void EquipGun(Gun gunPrefab)
{
    if (currentGun != null)
    {
        Destroy(currentGun.gameObject);
    }

    Gun newGun = Instantiate(gunPrefab, transform);
    newGun.transform.localPosition = new Vector3(0.7f, 0, 0); 
    newGun.transform.localRotation = Quaternion.identity;

    currentGun = newGun;
}


    public void SetMovementInput(float input)
{
    movementInput = input; 
}

    public void Move(float input)
{

    if (Mathf.Abs(input) > 0.01f) 
    {
        rb.velocity = new Vector2(input * movementSpeed, rb.velocity.y);
        spriteRenderer.flipX = input < 0;
    }
    else
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
}



    public void Jump()
{
    if (isGrounded)
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        isGrounded = false; 
    }
}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckGroundCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckGroundCollision(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void CheckGroundCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}



