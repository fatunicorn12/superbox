using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    public Sprite openSprite;
    public Gun[] availableGuns;  
    private SpriteRenderer spriteRenderer;
    private bool isOpened = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player") && !isOpened)
    {
        isOpened = true;
        spriteRenderer.sprite = openSprite;

        Gun newGunPrefab = availableGuns[Random.Range(0, availableGuns.Length)];
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.EquipGun(newGunPrefab);
        }
        DisplayGun(newGunPrefab.gunName);  
    }
}


    private void DisplayGun(string gunName)
    {
        Debug.Log("New Gun: " + gunName);
        //update the UI here
    }
}



