using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_PaperBossVer1 : MonoBehaviour
{
    [SerializeField] private float lifetime = 3f;
    private Vector3 position;
    [SerializeField] private float fireRate = 4f;
    [SerializeField] private float fireTime = 0f;
    [SerializeField] private bool canFire = false;
    public GameObject attack;
    [SerializeField] private int maxHealth = 2;
    [SerializeField] private int curHealth;
    [SerializeField] private GameObject atkSpawn;
    [SerializeField] private Coroutine stopMovingDuringFire;

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

        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        //Check if fire cooldown has been reached
        if (fireTime < Time.time)
            canFire = true;


        position = transform.position;

        if (canFire == true)
        {
            Fire();
        }
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
            Destroy(gameObject);
        }
    }

    private void RandomizeFireRate()
    {
        fireRate = Random.Range(1f, 4f);
    }
}
