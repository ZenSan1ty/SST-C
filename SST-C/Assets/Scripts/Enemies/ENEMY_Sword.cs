using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_Sword : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 12f;
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
        if (GameManager.instance.pBossDefeated == true || GameManager.instance.rBossDefeated == true)
        {
            maxHealth = 4;
        }
        else if (GameManager.instance.pBossDefeated == true && GameManager.instance.rBossDefeated == true)
        {
            maxHealth = 6;
        }

        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy movement
        position = transform.position;
        CheckStun();
        position.x -= moveSpeed * Time.deltaTime;
        //Check if enemy has reached off-screen boundary and move enemy back on screen
        Boundary();
        transform.position = position;

        if (isStunned == false)
        {
            transform.Rotate(0, 0, 720 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(2);
            Damage(curHealth);
        }
        else
        {
            //Debug.Log("Collided with an object other than player");
        }
    }

    private void Boundary()
    {
        if (position.x < -15)
            position.x = 15;
    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            GameManager.instance.swordsKilled++;
            GameManager.instance.swordsAlive--;
            Destroy(gameObject);
            GameManager.instance.CheckBossSpawn();
        }
    }

    public void Stun()
    {
        moveSpeed = 3f;
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
                moveSpeed = 12f;
            }
        }
    }
}
