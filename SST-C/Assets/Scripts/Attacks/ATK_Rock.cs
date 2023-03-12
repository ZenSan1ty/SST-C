using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ATK_Rock : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float lifetime = .2f;
    private Vector3 position;


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
        if (collision.gameObject.tag == "ENEMY_Sword")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "ENEMY_Paper")
        {
            Destroy(gameObject);
            Debug.Log("Destroyed by the paper attack");
        }
        else
        {
            Debug.Log("Collided with something unknown");
        }
    }
}
