using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

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
    private Weapon currentWeapon;

    public AudioClip jumpSound;
    private AudioSource audioSource;
    public AudioClip gameOverSound;
    public GameObject gameOverCanvas;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
        audioSource = gameObject.AddComponent<AudioSource>();

        gameOverCanvas.SetActive(false);
    }

    void Update()
    {
        isMoving = Mathf.Abs(movementInput) > Mathf.Epsilon;
        if (animator)
            animator.SetBool("isMoving", isMoving);

        Move(movementInput);
    }

    public void EquipWeapon(Weapon weaponPrefab)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon.gameObject);

        Weapon newWeapon = Instantiate(weaponPrefab, transform);
        newWeapon.transform.localPosition = new Vector3(0.7f, 0, 0);
        newWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon = newWeapon;
    }

    public void ActivateWeapon()
    {
        if (currentWeapon != null)
            currentWeapon.ActivateWeapon();
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
            rb.velocity = new Vector2(0, rb.velocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckGroundCollision(collision);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameOver();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckGroundCollision(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TeleportTrigger"))
        {
            GameOver();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    private void CheckGroundCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void GameOver()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (MysteryBox.score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", MysteryBox.score);
            highScore = MysteryBox.score;
        }

        if (finalScoreText != null)
            finalScoreText.text = "Final Score: " + MysteryBox.score.ToString();
        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore.ToString();

        gameOverCanvas.SetActive(true);

        if (gameOverSound != null && audioSource != null)
    {
        audioSource.PlayOneShot(gameOverSound);
    }

    }

    public void RestartGame()
    {
        MysteryBox.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
