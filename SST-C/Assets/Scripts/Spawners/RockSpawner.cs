using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rockEnemy;
    [SerializeField] private bool canSpawn = true;
    private float[] spawnDelay = { 2f, 2f, 1.5f, 1.5f, 1.5f, 1.32f, 1.32f, 1f, .5f };
    [SerializeField] private float currentSpawnDelay;
    private int[] maxAlive = { 1, 2, 4, 4, 4, 6, 6, 6, 8, 8 };
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
        if (canSpawn && GameManager.instance.rocksAlive < maxAlive[currentDifficulty])
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
        Instantiate(rockEnemy, spawnPosition, transform.rotation);
        GameManager.instance.rocksAlive += 1;
    }

    private void RandomiseSpawn()
    {
        spawnPosition.y = UnityEngine.Random.Range(-4.8f, 4.8f);
    }
    
    private void UpdateDifficulty()
    {
        currentDifficulty = GameManager.instance.difficultyLevel;
        if (currentDifficulty >= maxAlive.Length)
        {
            currentDifficulty = maxAlive.Length - 1;
        }
    }
}
