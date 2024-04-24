using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public float maxSpawnDelay = 4f; 

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            bool movingRight = (Random.value > 0.5f);
            if (!movingRight)
            {
                Vector3 theScale = enemy.transform.localScale;
                theScale.x *= -1;
                enemy.transform.localScale = theScale;
            }

            Goon goonScript = enemy.GetComponent<Goon>();
            if (goonScript != null)
            {
                goonScript.SetMovingDirection(movingRight);
            }

            nextSpawnTime = Time.time + Random.Range(0f, maxSpawnDelay);
        }
    }
}



