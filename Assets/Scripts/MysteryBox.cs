using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MysteryBox : MonoBehaviour
{
    public Weapon[] availableWeapons;  
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Collider2D myCollider;

    public GameObject mysteryBoxPrefab;
    public static int score = 0;
    public TextMeshProUGUI scoreText;
    public AudioClip collectionSound;  
    private AudioSource audioSource;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {

        Weapon newWeaponPrefab = availableWeapons[Random.Range(0, availableWeapons.Length)];
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.EquipWeapon(newWeaponPrefab);  
        }
        DisplayWeapon(newWeaponPrefab.weaponName);
        score++;
        UpdateScoreUI();
        SpawnNewBox();
        PlayCollectionSound();  
        DestroyBox();
    }
    if(other.CompareTag("TeleportTrigger")){
        SpawnNewBox();
        DestroyBox();
    }
}

private void PlayCollectionSound()
{
    if (audioSource != null && collectionSound != null)
    {
        audioSource.PlayOneShot(collectionSound);
    }
}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            myCollider.isTrigger = true;
        }
    }

    private void SpawnNewBox()
    {
        Camera mainCamera = Camera.main;
        float verticalWidthSeen = mainCamera.orthographicSize * 2f * Screen.width / Screen.height;
        float randomX = Random.Range(mainCamera.transform.position.x - verticalWidthSeen / 2, mainCamera.transform.position.x + verticalWidthSeen / 2);
        float safeSpawnHeight = 4f;

        GameObject newBox = Instantiate(mysteryBoxPrefab, new Vector3(randomX, safeSpawnHeight, 0), Quaternion.identity);
        Rigidbody2D rbNewBox = newBox.GetComponent<Rigidbody2D>();
        Collider2D colliderNewBox = newBox.GetComponent<Collider2D>();

        if (rbNewBox != null)
        {
            rbNewBox.isKinematic = false;
            rbNewBox.gravityScale = 1;
            rbNewBox.freezeRotation = true;
        }
        if (colliderNewBox != null)
        {
            colliderNewBox.isTrigger = false;
        }

        MysteryBox boxScript = newBox.GetComponent<MysteryBox>();
        if (boxScript != null)
        {
            boxScript.scoreText = scoreText;
        }
    }

    private void DestroyBox()
    {
        Destroy(gameObject);
    }

    private void DisplayWeapon(string weaponName)
    {
        Debug.Log("New Weapon: " + weaponName);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
