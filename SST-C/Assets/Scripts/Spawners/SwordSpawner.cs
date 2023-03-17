using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpawner : MonoBehaviour
{

    [SerializeField] private GameObject swordEnemy;
    [SerializeField] private bool canSpawn = true;
    [SerializeField] private float currentSpawnDelay;
    private float[] spawnDelay = { 2.61f, 2.5f, 2f, 2f, 1.81f, 1.81f, 1.5f, 1.5f, 1f };
    private int[] maxAlive = { 1, 2, 2, 3, 3, 4, 4, 5, 6 };
    private int currentDifficulty = 0;
    private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDifficulty();
        if (canSpawn && GameManager.instance.swordsAlive < maxAlive[currentDifficulty])
        {
            RandomiseSpawn();
            SpawnEnemy();
            canSpawn = false;
        }
        if (!canSpawn)
        {
            currentSpawnDelay += Time.deltaTime;
            if (currentSpawnDelay >= spawnDelay[currentDifficulty])
            {
                canSpawn = true;
                currentSpawnDelay = 0f;
            }
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(swordEnemy, spawnPosition, transform.rotation);
        GameManager.instance.swordsAlive += 1;
    }

    private void RandomiseSpawn()
    {
        spawnPosition.y = Random.Range(-3.5f, 3.5f);
    }

    private void UpdateDifficulty()
    {
        currentDifficulty = GameManager.instance.difficultyLevel;
        if (currentDifficulty > maxAlive.Length - 1)
        {
            currentDifficulty = maxAlive.Length - 1;
        }
    }
}
