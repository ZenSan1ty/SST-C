using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_PaperATK : MonoBehaviour
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
        position.x -= moveSpeed * Time.deltaTime;
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.Log("Collided with an object other than player");
        }
    }
}
