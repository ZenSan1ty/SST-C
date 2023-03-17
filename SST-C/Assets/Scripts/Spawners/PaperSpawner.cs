using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSpawner : MonoBehaviour
{
    [SerializeField] private GameObject paperEnemy;
    [SerializeField] private bool canSpawn = true;
    private float[] spawnDelay = { 3.67f, 3.67f, 3.2f, 3.2f, 2.4f, 2.4f, 2.4f, 2.4f, 2.4f };
    [SerializeField] private float currentSpawnDelay;
    //[SerializeField] private int maxAlive = 3;
    private int[] maxAlive = { 1, 1, 2, 3, 3, 3, 4, 5, 6 };
    private int currentDifficulty = GameManager.instance.difficultyLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDifficulty();
        if (canSpawn && GameManager.instance.papersAlive < maxAlive[currentDifficulty])
        {
            SpawnEnemy();
            canSpawn = false;
        }
        if (!canSpawn)
        {
            currentSpawnDelay += Time.deltaTime;
            if (currentSpawnDelay >= spawnDelay[currentDifficulty])
            {
                canSpawn = true;
                currentSpawnDelay = 0f  ;
            }
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(paperEnemy);
        GameManager.instance.papersAlive += 1;
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
