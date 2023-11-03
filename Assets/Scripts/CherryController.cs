using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject cherryPrefab;
    private GameObject currentCherry;
    private float spawnTimer = 10f;
    private Vector2 endPosition;
    private float lerpTime = 1000f;
    private float currentLerpTime;
    private Vector2 levelCenter;

    private float frameWidth = 25f;
    private float frameHeight = 25f;

    public ScoreManager scoreManager;

    void Start()
    {
        InvokeRepeating("SpawnCherry", 0, spawnTimer);
        levelCenter = new Vector2(0, 0);

        GameObject scoreManagerObj = GameObject.Find("ScoreManager");

        scoreManager = scoreManagerObj.GetComponent<ScoreManager>();
    }

    void SpawnCherry()
    {
        if (currentCherry != null)
        {
            return;
        }
        int side = Random.Range(0, 4);
        Vector2 spawnPosition = Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch (side)
        {
            case 0: // Left
                spawnPosition = new Vector2(-frameWidth / 2, Random.Range(-frameHeight / 2, frameHeight / 2));
                direction = (levelCenter - spawnPosition).normalized;
                break;
            case 1: // Right
                spawnPosition = new Vector2(frameWidth / 2, Random.Range(-frameHeight / 2, frameHeight / 2));
                direction = (levelCenter - spawnPosition).normalized;
                break;
            case 2: // Top
                spawnPosition = new Vector2(Random.Range(-frameWidth / 2, frameWidth / 2), frameHeight / 2);
                direction = (levelCenter - spawnPosition).normalized;
                break;
            case 3: // Bottom
                spawnPosition = new Vector2(Random.Range(-frameWidth / 2, frameWidth / 2), -frameHeight / 2);
                direction = (levelCenter - spawnPosition).normalized;
                break;
        }
        endPosition = levelCenter + direction * Mathf.Max(frameWidth, frameHeight);
        currentCherry = Instantiate(cherryPrefab, spawnPosition, Quaternion.identity);
        currentLerpTime = 0;
    }

    void Update()
    {
        if (currentCherry != null)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float perc = currentLerpTime / lerpTime;

            Vector2 startPos = (Vector2)currentCherry.transform.position;
            Vector2 endPos = endPosition;
            Vector2 newPosition = Vector2.Lerp(startPos, endPos, perc);

            currentCherry.transform.position = newPosition;

            if (currentLerpTime >= spawnTimer)
            {
                Destroy(currentCherry);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManager.IncrementScore(100);

            Destroy(gameObject);
        }
    }
}
