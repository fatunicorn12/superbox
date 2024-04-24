using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Pistol,
    Shotgun
}

public class Gun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootingPointRight;
    public Transform shootingPointLeft;
    public float projectileSpeed = 10f;
    public string gunName;

    private SpriteRenderer playerSpriteRenderer;
    private Vector3 originalLocalScale;

    private Vector3 rightSidePosition = new Vector3(0.7f, 0, 0); 
    private Vector3 leftSidePosition = new Vector3(-0.7f, 0, 0); 

    void Start()
    {
        playerSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    void Update()
{
    if (playerSpriteRenderer.flipX)
    {
        transform.localPosition = leftSidePosition;
        transform.localScale = new Vector3(-0.05f, 0.05f, 1); 
    }
    else
    {
        transform.localPosition = rightSidePosition;
        transform.localScale = new Vector3(0.05f, 0.05f, 1); 
    }
}


    public void Shoot()
    {
        Transform shootingPoint = playerSpriteRenderer.flipX ? shootingPointLeft : shootingPointRight;
        GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.Euler(0, 0, playerSpriteRenderer.flipX ? 180 : 0));
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector2 shootDirection = playerSpriteRenderer.flipX ? Vector2.left : Vector2.right;
        rb.velocity = shootDirection * projectileSpeed;
    }
}






