using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RockBossVerticalRock : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;

        if (Mathf.Sign(position.y) == -1)
        {
            moveSpeed = -moveSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy Movement across screen
        position = transform.position;
        position.y -= moveSpeed * Time.deltaTime;
        Boundary();
        transform.position = position;
    }

    private void Boundary()
    {
        if (position.y <= -5)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(3);
            Destroy(gameObject);
        }
    }
}

