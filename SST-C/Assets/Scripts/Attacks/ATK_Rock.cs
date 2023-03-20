using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ATK_Rock : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float lifetime = .2f;
    private Vector3 position;
    [SerializeField] private int atkDamage = 2;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        position.x += moveSpeed * Time.deltaTime;
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Damage the sword enemy
        if (collision.gameObject.tag == "ENEMY_Sword")
        {
            collision.GetComponent<ENEMY_Sword>().Damage(atkDamage);
        }
        //Destroy the rock attack
        else if (collision.gameObject.tag == "ENEMY_Paper")
        {
            Destroy(gameObject);
        }
        //Stun the rock enemy and destroy the rock attack
        else if (collision.gameObject.tag == "ENEMY_Rock")
        {
            collision.GetComponent<ENEMY_Rock>().Stun();
            //Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "BOSS_Rock")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag =="BOSS_Sword")
        {
            collision.GetComponent<SwordBoss>().Damage(atkDamage);
        }
    }
}
