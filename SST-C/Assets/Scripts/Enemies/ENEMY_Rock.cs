using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_Rock: MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy Movement across screen
        position = transform.position;
        position.x -= moveSpeed * Time.deltaTime;
        Boundary();
        transform.position = position;
    }

    private void Boundary()
    {
        if (position.x < -15)
            position.x = 15;
    }
}
