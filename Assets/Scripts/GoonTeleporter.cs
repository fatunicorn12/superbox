using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoonTeleporter : MonoBehaviour
{
    public Transform respawnPoint; 
    public float superGoonSpeed = 20f; 
    public Color superGoonColor = Color.red; 
    private Goon goonScript; 
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        goonScript = GetComponent<Goon>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("TeleportTrigger"))
        {
            if (!goonScript.IsSuper)  
            {
                TransformGoon();
            }
            else
            {
                RespawnSuperGoon(); 
            }
        }
    }

    void TransformGoon()
    {
        goonScript.SetSuperGoon();
        spriteRenderer.color = superGoonColor; 
        RespawnSuperGoon(); 
    }

    void RespawnSuperGoon()
    {
        transform.position = respawnPoint.position;
    }
}





