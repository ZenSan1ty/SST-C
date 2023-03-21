using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATK_Sword : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float lifetime = 3f;
    private Vector3 position;
    [SerializeField] private int atkDamage = 2;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        position.x += moveSpeed * Time.deltaTime;
        transform.position = position;

        transform.Rotate(0, 0, -720 * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Damage the paper enemy
        if (collision.gameObject.tag == "ENEMY_Paper")
        {
            collision.GetComponent<ENEMY_Paper>().Damage(atkDamage);
        }
        //Destroy the sword attack;
        else if (collision.gameObject.tag == "ENEMY_Rock") 
        {
            Destroy(gameObject);
        }
        //Stun the sword enemy and destroy the sword attack
        else if (collision.gameObject.tag == "ENEMY_Sword")
        {
            collision.GetComponent<ENEMY_Sword>().Stun();
        }
        else if (collision.gameObject.tag == "BOSS_Rock")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "BOSS_Paper")
        {
            collision.GetComponent<PaperBoss>().Damage(atkDamage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Shield")
        {
            Destroy(gameObject);
        }
    }
}
