using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPaperAttack : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float lifetime = 5f;
    private Vector3 position;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifetime);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        //transform.LookAt(player.transform.position);
        Move();
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(1);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "ATK_Rock")
        {
            Destroy(collision.gameObject);
            Debug.Log("Destroyed the rock attack");
        }
        if (collision.gameObject.tag == "ATK_Sword")
        {
            Destroy(gameObject);
            //Destroy(collision.gameObject);
        }
    }

    private void Move()
    {
        if (position.x > player.transform.position.x)
        {
            position.x -= moveSpeed * Time.deltaTime;
        }
        else if (position.x < player.transform.position.x)
        {
            position.x += moveSpeed * Time.deltaTime; ;
        }
        if (position.y > player.transform.position.y)
        {
            position.y -= moveSpeed * Time.deltaTime;
        }
        else if (position.y < player.transform.position.y)
        {
            position.y += moveSpeed * Time.deltaTime;
        }
    }
}
