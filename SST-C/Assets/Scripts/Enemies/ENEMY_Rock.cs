using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_Rock: MonoBehaviour
{

    public float moveSpeed = 5f;
    private Vector3 position;
    [SerializeField] private int maxHealth = 2;
    [SerializeField] private int curHealth;

    // Start is called before the first frame update
    void Start()
    {
        //Check if any bosses have been defeated and change the enemy health accordingly
        if (GameManager.instance.pBossDefeated == true | GameManager.instance.sBossDefeated == true)
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
            collision.GetComponent<ENEMY_Rock>().curHealth = curHealth - 2;
            Destroy(gameObject);
        }
    }

    private void Boundary()
    {
        if (position.x < -15)
            position.x = 15;
    }

    public void Bounce()
    {
        GetComponent<Collider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        moveSpeed = -moveSpeed;
    }
}
