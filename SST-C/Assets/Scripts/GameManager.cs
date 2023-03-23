using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject parent;
    [SerializeField] private int score = 0;
    [SerializeField] public int Score {  
        get { return  score; }
        private set { score = value; }
    }
    public Text scoreText;
    private float timeSinceStart = 0f;

    public GameObject deathScreen;

    public float xBoundary = 98f;
    public float yBoundary = 38f;

    public GameObject player;
    public bool playerDead = false;

    public bool rBossAlive = false;
    public bool pBossAlive = false;
    public bool sBossAlive = false;
    public bool rBossDefeated = false;
    public bool pBossDefeated = false;
    public bool sBossDefeated = false;

    public GameObject RockBoss;
    public GameObject PaperBoss;
    public GameObject SwordBoss;
    public GameObject BossSpawn;
    public GameObject bg;

    public int rocksKilled = 0;
    public int papersKilled = 0;
    public int swordsKilled = 0;
    public int killsRequired = 10;

    public int swordsAlive = 0;
    public int rocksAlive = 0;
    public int papersAlive = 0;

    public int difficultyLevel = 0;

    public RockSpawner rockSpawner;
    public PaperSpawner paperSpawner;
    public SwordSpawner swordSpawner;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        //parent = GameObject.Find("Canvas/Death Screen/Score Text");
        //scoreText = parent.GetComponent<Text>();

        UpdateScore();
    }

    private void Update()
    {
        if (playerDead == false)
        {
            CalculateScore();

        }

        difficultyLevel = Score / 500;
    }

    public void PlayerDied()
    {
        deathScreen.SetActive(true);
        bg.GetComponent<ScrollingBackground>().speed = 0;
        CalculateScore();
        DeactivateSpawners();
        playerDead = true;
        //Destroy(player);
        //Time.timeScale = 0f;
    }

    private void CalculateScore()
    {
        timeSinceStart += Time.deltaTime;
        Score = (int)(((rocksKilled + papersKilled + swordsKilled) * 50) + (Mathf.Round(timeSinceStart) * 10));
            if (rBossDefeated)
                Score += 500;
            if (sBossDefeated)
                Score += 500;
            if (pBossDefeated)
                Score += 500;
        UpdateScore();
    }
    
    private void UpdateScore()
    {
        scoreText.text = "Score: " + Score;
    }

    public void CheckBossSpawn()
    {
        if (rocksKilled >= killsRequired && !rBossDefeated)
        {
            DeactivateSpawners();
            Instantiate(RockBoss, BossSpawn.transform.position, transform.rotation);
            rBossAlive = true;
        }
        else if (swordsKilled >= killsRequired && !sBossDefeated)
        {
            DeactivateSpawners();
            Instantiate(SwordBoss, BossSpawn.transform.position, transform.rotation);
            sBossAlive = true;
        }
        else if (papersKilled >= killsRequired && !pBossDefeated)
        {
            DeactivateSpawners();
            Instantiate(PaperBoss, BossSpawn.transform.position, transform.rotation);
        }
        else
            Debug.Log("Not enough kills to spawn boss");
    }

    private void DeactivateSpawners()
    {
        rockSpawner.gameObject.SetActive(false);
        paperSpawner.gameObject.SetActive(false);
        swordSpawner.gameObject.SetActive(false);
        rocksAlive = 0;
        papersAlive = 0;
        swordsAlive = 0;
        DestroyEnemies();
        
    }

    public void ActivateSpawners()
    {
        rockSpawner.gameObject.SetActive(true);
        paperSpawner.gameObject.SetActive(true);
        swordSpawner.gameObject.SetActive(true);
    }

    private void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("ENEMY_Paper");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);
        enemies = GameObject.FindGameObjectsWithTag("ENEMY_Rock");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);
        enemies = GameObject.FindGameObjectsWithTag("ENEMY_Sword");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);
    }
}
