using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockBoss : MonoBehaviour
{
    public bool isStage1 = true;
    public bool isStage2 = false;
    public bool isStage3 = false;

    WaitForSeconds shortWait = new WaitForSeconds(0.2f);
    WaitForSeconds longWait = new WaitForSeconds(2f);

    [SerializeField] private int maxHealth = 60;
    [SerializeField] private int curHealth;

    private Vector3 topSpawnPos;
    private Vector3 bottomSpawnPos;
    private Vector3 position;

    [SerializeField] private float moveSpeedVert = 5f;
    [SerializeField] private float initMoveSpeedVert = 5f;
    [SerializeField] private float moveSpeedHori = 0f;
    [SerializeField] private float initMoveSpeedHori = 30f;

    [SerializeField] private GameObject rockEnemy;
    [SerializeField] private GameObject rockEnemyVert;
    private GameObject player;

    private Coroutine currentAttack;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Start is called before the first frame update
    void Start()
    {
        curHealth = 20;
        UpdateSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        UpdateSpawnPos();

        //Check to contain enemy in game bounds
        if (position.y >= 4 || position.y <= -4)
        {
            moveSpeedVert = -moveSpeedVert;
        }

        position.y += moveSpeedVert * Time.deltaTime;
        position.x -= moveSpeedHori * Time.deltaTime;


        if (currentAttack == null && isStage1 == true)
        {
            currentAttack = StartCoroutine(Stage1Attack());
        }
        else if (currentAttack == null && isStage2 == true)
        {
            currentAttack = StartCoroutine(Stage2Attack());
        }
        else if (currentAttack == null && isStage3 == true)
        {
            currentAttack = StartCoroutine(Stage3Attack());
        }

        if (position.x < -15)
            position.x = 15;

        transform.position = position;

    }

    // The main attack for stage 1 of the boss
    IEnumerator Stage1Attack()
    {
        for (int i = 0; i <= 10;  i++)
        {
            Instantiate(rockEnemy, gameObject.transform.position, gameObject.transform.rotation);
            yield return shortWait;
        }

        yield return longWait;

        Instantiate(rockEnemyVert, topSpawnPos, transform.rotation);
        Instantiate(rockEnemyVert, bottomSpawnPos, transform.rotation);

        yield return longWait;

        currentAttack = null;
    }

    IEnumerator Stage2Attack(bool usedStage3 = false)
    {

        Instantiate(rockEnemyVert, topSpawnPos, transform.rotation);
        Instantiate(rockEnemyVert, bottomSpawnPos, transform.rotation);

        for (int i = 0; i <= 10; i++)
        {
            
            Instantiate(rockEnemy, gameObject.transform.position, gameObject.transform.rotation);
            yield return shortWait;
        }

        yield return longWait;

        if (!usedStage3)
        {
            currentAttack = null;
        }
    }

    IEnumerator Stage3Attack()
    {
        StartCoroutine(Stage2Attack(true));

        yield return new WaitForSeconds(3f);

        moveSpeedVert = 0f;
        transform.position = GameManager.instance.BossSpawn.transform.position;
        moveSpeedHori = initMoveSpeedHori;
        yield return new WaitForSeconds(2f);
        moveSpeedHori = 0f;
        transform.position = GameManager.instance.BossSpawn.transform.position;
        moveSpeedVert = initMoveSpeedVert;

        yield return longWait;

        currentAttack = null;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(5);
        }
    }

    //Updates the position of enemy spawn points above and beneath the player
    private void UpdateSpawnPos()
    {
        topSpawnPos = player.transform.position + new Vector3(0, 5, 0);
        bottomSpawnPos = player.transform.position - new Vector3(0, 5, 0);

    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        CheckStage();
        if (curHealth <= 0)
        {
            Destroy(gameObject);
            StopAllCoroutines();
            currentAttack = null;
            GameManager.instance.rBossDefeated = true;
            GameManager.instance.rBossAlive = false;
            GameManager.instance.ActivateSpawners();
            player.GetComponent<Player>().currentHealth = maxHealth;
        }
    }

    public void CheckStage()
    {
        if (20f < curHealth && curHealth < 41f)
        {
            isStage1 = false;
            isStage2 = true;
            isStage3 = false;
        }
        else if (0f < curHealth && curHealth < 21f)
        {
            isStage1 = false;
            isStage2 = false;
            isStage3 = true;
        }
    }
}
