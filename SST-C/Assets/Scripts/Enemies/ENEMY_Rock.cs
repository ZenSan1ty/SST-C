using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_Rock: MonoBehaviour
{

    public float moveSpeed = 5f;
    private Vector3 position;
    [SerializeField] private int maxHealth = 2;
    [SerializeField] private int curHealth;
    [SerializeField] private bool isStunned = false;
    [SerializeField] private float stunTimer = 2f;
    [SerializeField] private float stunnedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Check if any bosses have been defeated and change the enemy health accordingly
        if (GameManager.instance.pBossDefeated == true || GameManager.instance.sBossDefeated == true)
        {
            maxHealth = 4;
        }
        else if (GameManager.instance.pBossDefeated == true && GameManager.instance.sBossDefeated == true)
        {
            maxHealth = 6;
        }
        
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy Movement across screen
        position = transform.position;
        CheckStun();
        position.x -= moveSpeed * Time.deltaTime;
        Boundary();
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(3);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "ENEMY_Rock" && moveSpeed == -5f)
        {
            collision.GetComponent<ENEMY_Rock>().Damage(2);
            Damage(curHealth);
        }
    }

    private void Boundary()
    {
        if (position.x < -15)
            position.x = 15;

        if (position.x > 15.5)
            Damage(curHealth);
    }

    public void Bounce()
    {
        GetComponent<Collider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        moveSpeed = -5f;
    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            GameManager.instance.rocksKilled++;
            GameManager.instance.rocksAlive--;
            Destroy(gameObject);
        }
    }

    public void Stun()
    {
        moveSpeed = 0;
        isStunned = true;
    }

    private void CheckStun()
    {
        if (isStunned)
        {
            stunnedTime += Time.deltaTime;
            if (stunnedTime >= stunTimer)
            {
                isStunned = false;
                stunnedTime = 0;
                moveSpeed = 5f;
            }
        }
    }
}
