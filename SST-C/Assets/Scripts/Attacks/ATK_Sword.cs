using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATK_Sword : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float lifetime = 3f;
    private Vector3 position;

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
        if (collision.gameObject.tag == "ENEMY_Paper")
        {
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.Log("Collided with an object other than paper enemy");
        }
    }
}
