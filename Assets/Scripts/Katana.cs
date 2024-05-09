using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float slashDuration = 0.2f;  
    public Collider2D hitBox;
    private bool canSlash = true;
    private float slashCooldown = 1.0f;  
    private Quaternion originalRotation;  

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitBox.enabled = false;  
        originalRotation = transform.localRotation;  
    }

    void Update()
    {
        if (spriteRenderer.flipX)
        {
            transform.localPosition = new Vector3(-0.5f, 0, 0);  
        }
        else
        {
            transform.localPosition = new Vector3(0.5f, 0, 0);  
        }
    }

    public void Slash()
    {
        if (!canSlash) return;

        transform.localRotation = Quaternion.Euler(0, 0, spriteRenderer.flipX ? -90 : 90);
        hitBox.enabled = true;
        Invoke("EndSlash", slashDuration);
        canSlash = false;
        Invoke("ResetSlash", slashCooldown);
    }

    private void EndSlash()
    {
        hitBox.enabled = false;
        transform.localRotation = originalRotation;  
    }

    private void ResetSlash()
    {
        canSlash = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))  
        {
            //implement later
            Debug.Log("katana hit!!");
        }
    }
}
