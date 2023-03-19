using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBoss : MonoBehaviour
{
    public bool isStage1 = true;
    private Coroutine currentAttack;
    public bool isStage2 = false;
    public bool isStage3 = false;
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int curHealth;
    [SerializeField] private GameObject rockEnemy;
    private Vector3 topSpawnPos;
    private Vector3 bottomSpawnPos;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        UpdateSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawnPos();

        if (currentAttack == null && isStage1 == true)
        {
            currentAttack = StartCoroutine(Stage1Attack());
        }

    }

    IEnumerator Stage1Attack()
    {
        for (int i = 0; i <= 10;  i++)
        {
            Instantiate(rockEnemy, gameObject.transform.position, gameObject.transform.rotation);
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(5f);

        currentAttack = null;
    }

    private void UpdateSpawnPos()
    {
        topSpawnPos = player.transform.position + new Vector3(0, 5, 0);
        bottomSpawnPos = player.transform.position - new Vector3(0, 5, 0);

    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            GameManager.instance.rocksKilled++;
            GameManager.instance.rocksAlive--;
            Destroy(gameObject);
            GameManager.instance.CheckBossSpawn();
        }
    }

    public void CheckStage()
    {
        if (10f < curHealth && curHealth < 21f)
        {
            isStage1 = false;
            isStage2 = true;
            isStage3 = false;
        }
        else if (0f < curHealth && curHealth < 11f)
        {
            isStage1 = false;
            isStage2 = false;
            isStage3 = true;
        }
    }
}
