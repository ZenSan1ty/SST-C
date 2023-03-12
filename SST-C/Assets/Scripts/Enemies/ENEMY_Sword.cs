using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_Sword : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float lifetime = 3f;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy movement
        position = transform.position;
        position.x -= moveSpeed * Time.deltaTime;
        //Check if enemy has reached off-screen boundary and move enemy back on screen
        Boundary();
        transform.position = position;

        transform.Rotate(0, 0, 720 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(1);
            Destroy(gameObject);
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
}
