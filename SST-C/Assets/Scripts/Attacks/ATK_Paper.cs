using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATK_Paper : MonoBehaviour
{

    [SerializeField] private float lifetime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ENEMY_Rock")
        {
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
    }
}
