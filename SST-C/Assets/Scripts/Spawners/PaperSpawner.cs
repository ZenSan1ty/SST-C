using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperSpawner : MonoBehaviour
{
    [SerializeField] private GameObject paperEnemy;
    [SerializeField] private bool canSpawn = true;
    [SerializeField] private float spawnDelay = 4f;
    [SerializeField] private float currentSpawnDelay;
    [SerializeField] private int maxAlive = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && GameManager.instance.papersAlive < maxAlive)
        {
            SpawnEnemy();
            canSpawn = false;
        }
        if (!canSpawn)
        {
            currentSpawnDelay += Time.deltaTime;
            if (currentSpawnDelay >= spawnDelay)
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
}
