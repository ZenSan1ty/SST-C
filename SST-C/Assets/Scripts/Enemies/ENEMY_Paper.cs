using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ENEMY_Paper : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float initMoveSpeed;
    private Vector3 position;
    [SerializeField] private float fireRate = 4f;
    [SerializeField] private float fireTime = 0f;
    [SerializeField] private bool canFire = false;
    public GameObject attack;
    [SerializeField] private int maxHealth = 2;
    [SerializeField] private int curHealth;
    [SerializeField] private GameObject atkSpawn;
    public float isNegative;
    private Coroutine stopMovingDuringFire;
    WaitForSeconds moveDelay = new WaitForSeconds(0.2f);

    private void Awake()
    {
        RandomizeFireRate();   
    }

    // Start is called before the first frame update
    void Start()
    {
        //Check if any bosses have been defeated and change the enemy health accordingly
        if (GameManager.instance.rBossDefeated == true || GameManager.instance.sBossDefeated == true)
        {
            maxHealth = 4;
        }
        else if (GameManager.instance.rBossDefeated == true && GameManager.instance.sBossDefeated == true)
        {
            maxHealth = 6;
        }

        curHealth = maxHealth;

        
    }

    // Update is called once per frame
    void Update()
    {
        //Check if fire cooldown has been reached
        if (fireTime < Time.time)
            canFire = true;


        position = transform.position;

        //Check if enemy is at the boundary
        if (position.y >= 5.2 || position.y <= -5.2)
        {
            moveSpeed = -moveSpeed;
        }
      
        position.y += moveSpeed * Time.deltaTime;
        transform.position = position;



        if (canFire == true && stopMovingDuringFire == null)
        {
            //Debug.Log("Started Coroutine");
            stopMovingDuringFire = StartCoroutine(FireTest());
            
        }
    }

    IEnumerator FireTest()
    {

        initMoveSpeed = moveSpeed;

        moveSpeed = 0;

        yield return moveDelay;

        while (canFire)
            Fire();

        yield return moveDelay;

        moveSpeed = initMoveSpeed;

        stopMovingDuringFire = null;
    }
    public void Fire()
    {
        Instantiate(attack, atkSpawn.transform.position, transform.rotation);

        canFire = false;
        RandomizeFireRate();
        fireTime = Time.time + fireRate;
    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            GameManager.instance.papersKilled++;
            GameManager.instance.papersAlive--;
            Destroy(gameObject);
            GameManager.instance.CheckBossSpawn();
        }
    }

    private void RandomizeFireRate()
    {
        fireRate = Random.Range(1f, 4f);
    }

}
