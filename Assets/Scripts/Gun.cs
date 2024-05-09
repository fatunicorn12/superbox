using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Pistol,
    Shotgun,
    Katana
}

public class Gun : Weapon
{
    public GameObject projectilePrefab;
    public Transform shootingPointRight;
    public Transform shootingPointLeft;
    public float projectileSpeed = 10f;
    public string gunName;

    public AudioClip pistolShotSound;
    public AudioClip shotgunShotSound;
    private AudioSource audioSource;

    private SpriteRenderer playerSpriteRenderer;
    private bool canShoot = true;  // shooting based on reload
    private float reloadDelay = 1.0f;  // reload delay 

    private Vector3 rightSidePosition = new Vector3(0.7f, 0, 0);
    private Vector3 leftSidePosition = new Vector3(-0.7f, 0, 0);
    public int pelletsCount = 5;
    public float spreadAngle = 25f;

    void Start()
    {
        playerSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        UpdateGunPositionAndScale();
    }

    private void UpdateGunPositionAndScale()
    {
        transform.localPosition = playerSpriteRenderer.flipX ? leftSidePosition : rightSidePosition;

        float scaleX = playerSpriteRenderer.flipX ? -1 : 1;
        if (gunName == "Pistol")
        {
            transform.localScale = new Vector3(scaleX * 0.05f, 0.05f, 1);
        }
        else if (gunName == "Shotgun")
        {
            transform.localScale = new Vector3(scaleX * 0.3f, 0.3f, 1);
        }
    }

    public void UpdateGunDirection(bool isFacingLeft)
{
    transform.localPosition = isFacingLeft ? leftSidePosition : rightSidePosition;

    float scaleX = isFacingLeft ? -1 : 1;
    if (gunName == "Pistol")
    {
        transform.localScale = new Vector3(scaleX * 0.05f, 0.05f, 1);
    }
    else if (gunName == "Shotgun")
    {
        transform.localScale = new Vector3(scaleX * 0.3f, 0.3f, 1);
    }
}


    public void Shoot()
{
    if (!canShoot) return;

    Transform shootingPoint = playerSpriteRenderer.flipX ? shootingPointLeft : shootingPointRight;

    if (gunName == "Shotgun")
    {
        for (int i = 0; i < pelletsCount; i++)
        {
            float angle = spreadAngle / (pelletsCount - 1) * i - spreadAngle / 2;
            Quaternion rotation = Quaternion.Euler(0, 0, playerSpriteRenderer.flipX ? 180 + angle : angle);
            GameObject pellet = Instantiate(projectilePrefab, shootingPoint.position, rotation);
            Rigidbody2D rb = pellet.GetComponent<Rigidbody2D>();
            Vector2 shootDirection = rotation * Vector2.right;
            rb.velocity = shootDirection * projectileSpeed;
        }
        canShoot = false;
        Invoke("Reload", reloadDelay);
        PlaySoundEffect(shotgunShotSound);
    }
    else if (gunName == "Pistol")
    {
        GameObject projectile = Instantiate(projectilePrefab, shootingPoint.position, Quaternion.Euler(0, 0, playerSpriteRenderer.flipX ? 180 : 0));
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        Vector2 shootDirection = playerSpriteRenderer.flipX ? Vector2.left : Vector2.right;
        rb.velocity = shootDirection * projectileSpeed;
        PlaySoundEffect(pistolShotSound);
    }
}

private void PlaySoundEffect(AudioClip clip)
{
    if(audioSource != null && clip != null)
    {
        audioSource.PlayOneShot(clip);
    }
}


    private void Reload()
    {
        canShoot = true;  
    }
}
