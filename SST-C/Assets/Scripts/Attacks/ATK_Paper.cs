using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATK_Paper : MonoBehaviour
{

    [SerializeField] private float lifetime = 5f;
    private Vector3 position;
    public float moveSpeed = 2f;
    private int hitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        position.x -= moveSpeed * Time.deltaTime;

        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ENEMY_Rock")
        {
            //Check how many times the paper has been hit by rocks, if 2 or more destroy it, otherwise add to the count
            if (hitCount >= 1)
                Destroy(gameObject);
            else
                hitCount++;

            collision.GetComponent<ENEMY_Rock>().Bounce();
        }
        else if (collision.gameObject.tag == "ENEMY_Sword")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "ENEMY_Paper")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "BOSS_Rock")
        {
            Destroy(gameObject);
        }
    }
}
