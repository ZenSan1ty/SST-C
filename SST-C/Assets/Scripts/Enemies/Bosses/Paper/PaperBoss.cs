using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBoss : MonoBehaviour
{
    public bool isStage1 = true;
    public bool isStage2 = false;
    public bool isStage3 = false;

    WaitForSeconds shortWait = new WaitForSeconds(0.2f);
    WaitForSeconds longWait = new WaitForSeconds(2f);

    [SerializeField] public int maxHealth = 60;
    [SerializeField] public int curHealth;
    [SerializeField] public int tempHealth;

    private Vector3 randomSpawnPos;
    private Vector3 position;

    [SerializeField] private float moveSpeedVert = 5f;
    [SerializeField] private float initMoveSpeedVert = 5f;
    [SerializeField] private float moveSpeedHori = 0f;
    [SerializeField] private float initMoveSpeedHori = 5f;

    [SerializeField] private GameObject paperEnemy;
    [SerializeField] private GameObject paperAttack;
    [SerializeField] private GameObject shield;
    private GameObject player;

    [SerializeField] private HealthBar healthBar;

    private Coroutine currentAttack;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;


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

        //transform.Rotate(0, 0, 360 * Time.deltaTime);


    }

    // The main attack for stage 1 of the boss
    IEnumerator Stage1Attack()
    {
        for (int i = 0; i < 20; i++)
        {
            Instantiate(paperAttack, transform.position, transform.rotation);
            yield return shortWait;
        }

        yield return new WaitForSeconds(3.5f);


        currentAttack = null;
    }

    IEnumerator Stage2Attack()
    {
        var forcefield = Instantiate(shield, position, transform.rotation);
        forcefield.transform.parent = transform;
        for (int i = 0; i < 8; i++)
        {
            UpdateSpawnPos();
            Instantiate(paperEnemy, randomSpawnPos, transform.rotation);
        }

        yield return new WaitForSeconds(4f);

        currentAttack = null;
    }

    IEnumerator Stage3Attack()
    {
        for (int i = 0; i < 8; i++)
        {
            UpdateSpawnPos();
            Instantiate(paperEnemy, randomSpawnPos, transform.rotation);
        }

        for (int i = 0; i < 20; i++)
        {
            Instantiate(paperAttack, transform.position, transform.rotation);
            yield return shortWait;
        }

        yield return new WaitForSeconds(2f);

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
        randomSpawnPos = transform.position + new Vector3(0f, Random.Range(-4f, 4f), 0f);

    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        healthBar.UpdateHealthBar();
        CheckStage();

        if (curHealth <= 0)
        {
            Destroy(gameObject);
            StopAllCoroutines();
            currentAttack = null;
            GameManager.instance.pBossDefeated = true;
            GameManager.instance.pBossAlive = false;
            GameManager.instance.papersKilled += 10;
            GameManager.instance.ActivateSpawners();
            player.GetComponent<Player>().currentHealth = maxHealth;
            player.GetComponent<Player>().TakeDamage(0);
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

